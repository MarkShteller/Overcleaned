
using UnityEngine;


// This is just a simple "player" script that rotates and colors a cube
// based on input read from the actions field.
//
// See comments in PlayerManager.cs for more details.
//
public class Player : MonoBehaviour
{

    public float moveSpeed;
    public float rotationSpeed;

	public PlayerActions Actions { get; set; }


    private CharacterController _cc;

    public Animator anim;

    private Tile _currTile;

    public PlayerFocusDetector FocusDetector;
    public MeshRenderer FocusArrow;
    private IInteractable _currFocusedInteractable;


	void Start()
	{
        _cc = gameObject.GetComponent<CharacterController>();
	}


	void Update()
	{
        Vector3 away = GetAwayVector();
        Vector3 right = GetRightVector();

        Vector3 desiredDirection = Actions.Rotate.Y * away + Actions.Rotate.X * right;
        float finalMoveSpeed = desiredDirection.magnitude * moveSpeed;

        if(desiredDirection.magnitude > 0.0f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.Normalize(desiredDirection));

            // Rotate our transform a step closer to the target's.
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, desiredDirection.magnitude * rotationSpeed * Time.deltaTime);

            _cc.Move(transform.forward * finalMoveSpeed * Time.deltaTime);


            this.UpdatePlayerTile();
        }
        anim.SetFloat("MoveSpeed", finalMoveSpeed/2.0f);

        UpdateFocusedInteractable();
    }


    void UpdatePlayerTile()
    {   
        //Clear previous tile
        if(this._currTile != null)
        {
            _currTile.PlayerReference = null;
        }


        this._currTile = RoomManager.instance.GetTileAt(this.transform.position);
        this._currTile.PlayerReference = this;

    }


    void UpdateFocusedInteractable()
    {
        this._currFocusedInteractable = FocusDetector.GetClosestInteractable();
        //Show focus arrow
        if (this._currFocusedInteractable == null)
        {
            FocusArrow.enabled = false;
        }
        else
        {
            FocusArrow.enabled = true;

            FocusArrow.transform.position = this._currFocusedInteractable.GetPosition() + Vector3.up * 2.0f;
        }
    }


    Vector3 GetAwayVector()
    {
        Vector3 away3d = Camera.main.transform.forward;

        //Project onto xz plane
        return Vector3.Normalize(new Vector3(away3d.x, 0, away3d.z));
    }

    Vector3 GetRightVector()
    {
        Vector3 right3d = Camera.main.transform.right;

        //Project onto xy plane
        return Vector3.Normalize(new Vector3(right3d.x, 0, right3d.z));
    }

}

