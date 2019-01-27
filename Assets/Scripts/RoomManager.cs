using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;

    public float TileSize;
    public Transform TopLeft;
    public Transform BottomRight;

    public List<GameObject> EasyMessPrefabs;
    public List<GameObject> HardMessPrefabs;

    //Game Difficulty tuning
    public AnimationCurve DifficultyCurve;
    public float MaxSpawnInterval;
    public float MinSpawnInterval;
    public float MinutesToMinInterval;
    public float HardThreshold;
    
    private Tile[,] _tiles;

    //Game state variables
    private float _startTime;

    private void Awake()
    {
        if(instance == null){
            instance = this;
        }
    }

    private void Start()
    {
        AudioManager.Instance.PlayMainMusic();
        _tiles = MapBuilder.BuildTileMap(TopLeft.position, BottomRight.position, TileSize);
    }

    /*private void Update()
    {
        for (int i = 0; i < _tiles.GetLength(0); i++)
        {
            for (int j = 0; j < _tiles.GetLength(1); j++)
            {
                Tile t = _tiles[i, j];

                /*Debug.DrawLine(t.TopLeft, t.TopLeft + (XBasis * TileSize), Color.red);
                Debug.DrawLine(t.TopLeft, t.TopLeft + (YBasis * TileSize), Color.red);#1#

                if (t.HasPlayer)
                {
                    Vector3 center = t.GetCenter(TileSize);
                    Debug.DrawRay(center, Vector3.up * 100.0f);
                }

                if (!t.IsClean)
                {
                    Vector3 center = t.GetCenter(TileSize);
                    Debug.DrawRay(center, Vector3.up * 100.0f, Color.green);
                }
            }
        }
    }*/

    public void StartGame()
    {
        this._startTime = Time.time;
        StartCoroutine(MessSpawner());
    }

    public float GetCleanlinessLevel()
    {
        float cleanLevel = 0;
        for (int i = 0; i < _tiles.GetLength(0); i++)
        {
            for (int j = 0; j < _tiles.GetLength(1); j++)
            {
                if (_tiles[i,j].IsClean) 
                {
                    cleanLevel++;
                }
            }
        }

        return cleanLevel;
    }


    private IEnumerator MessSpawner()
    {
        while (true)
        {

            float timeElapsed = Time.time - this._startTime;
            float timeScaled = timeElapsed / (MinutesToMinInterval * 60.0f);

            TryToSpawnMess(timeScaled > HardThreshold);

            float difficultyScale = DifficultyCurve.Evaluate(timeScaled);
            float spawnInterval = MinSpawnInterval + (MaxSpawnInterval - MinSpawnInterval) * difficultyScale;

            AudioManager.Instance.ChangePitchBendMusic(0.02f);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Might fail if no spare slot is found in time, returns false then
    bool TryToSpawnMess(bool hard)
    {
        int MAX_SPAWN_ATTEMPTS = 5;

        for(int spawnAttempt = 0; spawnAttempt < MAX_SPAWN_ATTEMPTS; spawnAttempt++)
        {
            int xIdx = UnityEngine.Random.Range(0, this._tiles.GetLength(0));
            int yIdx = UnityEngine.Random.Range(0, this._tiles.GetLength(1));

            Tile t = _tiles[xIdx, yIdx];
            bool canSpawn = t.IsClean && !t.HasPlayer;

            if (canSpawn)
            {
                List<GameObject> messPrefabs = new List<GameObject>(EasyMessPrefabs);

                //weight mess based on difficulty level
                if (hard)
                {
                    Debug.Log("SPAWNING HARD MESS");
                    foreach( GameObject hardMess in HardMessPrefabs)
                    {
                        messPrefabs.Add(hardMess);
                    }                  
                }

                GameObject prefab = messPrefabs[UnityEngine.Random.Range(0, messPrefabs.Count)];
                
                Vector3 jitterVector = Vector3.Normalize(new Vector3(UnityEngine.Random.Range(-1.0f,1.0f), 0, UnityEngine.Random.Range(-1.0f, 1.0f))) * TileSize * 0.3f;
                Vector3 spawnPoint = t.GetCenter(TileSize) + jitterVector;

                GameObject inst = GameObject.Instantiate(prefab, spawnPoint, Quaternion.identity);
                Mess messInst = inst.GetComponent<Mess>();
                //call mess lifecycle method
                messInst.dropOn(t);

                return true;
            }

        }

        return false;
    }


    public Tile GetTileAt(Vector3 position)
    {
        Vector3 offset = position - TopLeft.position;
        int xIdx = Math.Max(Math.Min( (int) (Math.Abs(offset.x)/TileSize), _tiles.GetLength(0) - 1), 0);
        int yIdx = Math.Max(Math.Min( (int) (Math.Abs(offset.z)/TileSize), _tiles.GetLength(1) - 1), 0);

        return _tiles[xIdx, yIdx];
    }

    public void ChangeTileState(MessType state, PlayerBehavior playerBehavior)
    {
        switch (state)
        {    case MessType.Clean:
                // base case
                // trigger nice audio :D
                break;
            case MessType.Wet:
                this.HandleWetState(playerBehavior);
                break;
            case MessType.Dirt:
                this.HandleDirtState(playerBehavior);
                break;
            case MessType.Dishes:
                this.HandleDishState(playerBehavior);
                break;
            case MessType.Clothes:
                this.HandleClothesState(playerBehavior);
                break;
            case MessType.Trash:
                this.HandleTrashState(playerBehavior);
                break;
            case MessType.DogShit:
                this.HandleShitState(playerBehavior);
                break;
            case MessType.Mud:
                this.HandleMudState(playerBehavior);
                break;
            default:
                break;
        }
    }

    public MessType GetTileMess(Tile tile)
    {
        return tile.MessReference.Messtype;
    }

    private void HandleWetState(PlayerBehavior playerBehavior)
    {
        if (playerBehavior.GetTool().Tooltype == ToolType.Rag)
        {
            this.ChangeTileState(MessType.Clean, playerBehavior);
        }
    }

    private void HandleDirtState(PlayerBehavior playerBehavior)
    {
        if (playerBehavior.GetTool().Tooltype == ToolType.Broom)
        {
            this.ChangeTileState(MessType.Clean, playerBehavior);
        }
    }

    private void HandleDishState(PlayerBehavior playerBehavior)
    {
        if (playerBehavior.GetTool().Tooltype == ToolType.Hand)
        {
            this.ChangeTileState(MessType.Clean, playerBehavior);
        }
    }

    private void HandleClothesState(PlayerBehavior playerBehavior)
    {
        if (playerBehavior.GetTool().Tooltype == ToolType.Hand)
        {
            this.ChangeTileState(MessType.Clean, playerBehavior);
        }
    }

    private void HandleTrashState(PlayerBehavior playerBehavior)
    {
        if (playerBehavior.GetTool().Tooltype == ToolType.Hand)
        {
            this.ChangeTileState(MessType.Clean, playerBehavior);
        }
    }

    private void HandleShitState(PlayerBehavior playerBehavior)
    {
        if (playerBehavior.GetTool().Tooltype == ToolType.Hand)
        {
            this.ChangeTileState(MessType.Mud, playerBehavior);
            // mud audio from mud source
        }
    }

    private void HandleMudState(PlayerBehavior playerBehavior)
    {
        if (playerBehavior.GetTool().Tooltype == ToolType.Mop)
        {
            this.ChangeTileState(MessType.Wet, playerBehavior);
            // water audio from audio source
        }
    }   
}