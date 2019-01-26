
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


	void Start()
	{
        _cc = gameObject.GetComponent<CharacterController>();
	}


	void Update()
	{
        Vector3 away = GetAwayVector();
        Vector3 right = GetRightVector();

        Vector3 desiredDirection = Actions.Rotate.Y * away + Actions.Rotate.X * right;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.Normalize(desiredDirection));

        // Rotate our transform a step closer to the target's.
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, desiredDirection.magnitude * rotationSpeed * Time.deltaTime);

        float finalMoveSpeed = desiredDirection.magnitude * moveSpeed;
        _cc.Move(transform.forward * finalMoveSpeed * Time.deltaTime);

        anim.SetFloat("MoveSpeed", finalMoveSpeed/2.0f);
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

