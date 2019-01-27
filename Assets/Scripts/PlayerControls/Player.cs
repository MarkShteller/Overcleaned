
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
    public FocusArrow focusArrow;

    private IInteractable _currFocusedInteractable;
    public IHoldable _currentHeld;

    public GameObject PlayerHand;


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

            Vector3 moveVector = transform.forward * finalMoveSpeed;

            if(transform.position.y > 0.0f)
            {
                moveVector += Vector3.up * -1.0f * 2.0f;
            }

            _cc.Move(moveVector * Time.deltaTime);


            this.UpdatePlayerTile();
        }
        anim.SetFloat("MoveSpeed", finalMoveSpeed/2.0f);

        UpdateFocusedInteractable();

        //Interact
        if (Actions.Green.WasPressed)
        {
            if(this._currFocusedInteractable != null)
            {
                this._currFocusedInteractable.interact(this);
            }
        }

        //Drop
        if (Actions.Red.WasPressed)
        {
            if (this._currentHeld != null)
            {
                this._currentHeld.OnTryDrop();
            }
        }

    }


    public void HoldGameObject(IHoldable obj)
    {
        GameObject gObj = obj.GetGameObject();
        gObj.transform.parent = PlayerHand.transform;
        gObj.transform.localPosition = Vector3.zero;
        gObj.transform.localRotation = Quaternion.identity;

        gObj.GetComponent<Collider>().enabled = false;
        this._currentHeld = obj;
    }


    public void DropGameObject(Vector3 position)
    {
        GameObject gObj = this._currentHeld.GetGameObject();
        gObj.transform.parent = null;
        gObj.transform.position = position;
        gObj.transform.rotation = Quaternion.identity;

        gObj.GetComponent<Collider>().enabled = true;
        this._currentHeld = null;
    }


    public bool IsHoldingObject()
    {
        return this._currentHeld != null;
    }

    public ToolType CurentlyHeldToolType()
    {
        if(!this.IsHoldingObject()) { return ToolType.None;  }

        Tool t = this._currentHeld.GetGameObject().GetComponent<Tool>();
        
        if(t == null) { return ToolType.None; }

        return t.type;

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
            focusArrow.Hide();
        }
        else
        {
            focusArrow.Show();

            focusArrow.transform.position = this._currFocusedInteractable.GetPosition() + Vector3.up * 1.5f;
            focusArrow.transform.rotation = Quaternion.identity;
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

