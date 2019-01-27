using UnityEngine;

public class Tile
{
    private float cleanlinessValue = 0.02f;

    public Tile(int x, int y)
    {
        XLocation = x;
        YLocation = y;
    }
    public bool IsClean { get {
            return this.MessReference == null;
        }
    }
    public bool HasPlayer { get
        {
            return this.PlayerReference != null;
        }
    }

    public bool IsEmpty { get
        {
            return this.IsClean && !this.HasPlayer && this.ToolReference == null;
        }
    }

    public Mess MessReference { get; set; }
    public Player PlayerReference { get; set; }
    public Tool ToolReference { get; set; }
    public int XLocation { get; set; }
    public int YLocation { get; set; }
    public Vector3 TopLeft { get; set; }

    public Vector3 GetCenter(float tileSize)
    {
        Vector3 offset = (new Vector3(1, 0, -1)) * tileSize * 0.5f;
        return TopLeft + offset;
    }

    public void AddMess(Mess mess)
    {
        GameManager.Instance.ChangeCleanliness(-cleanlinessValue);
        AudioManager.Instance.PlayMessFx(mess.Messtype);
        AudioManager.Instance?.ChangePitchBendMusic(0.02f);
        MessReference = mess;
    }

    public void CleanMess()
    {
        MessReference = null;
        GameManager.Instance.ChangeCleanliness(cleanlinessValue);
        AudioManager.Instance?.ChangePitchBendMusic(-0.01f);
    }

    public void AddTool(Tool t)
    {
        ToolReference = t;
    }

    public void RemoveTool()
    {
        ToolReference = null;
    }
}
