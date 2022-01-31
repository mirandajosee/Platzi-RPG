using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 120.0f;

    private bool walking = false;
    public Vector2 lastMovement = Vector2.zero;

    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";
    private const string walkingState = "Walking";
    private const string attackingState = "Attacking";

    private Animator animator;
    private Rigidbody2D playerRigidBody;

    public static bool playerCreated;

    public string nextPlaceName;

    private bool attacking = false;
    public float attackTime;
    private float attackTimeCounter;

    public bool playerTalking;

    private SFXManager sfxManager;

    // Start is called before the first frame update
    void Start()
    {
        sfxManager = FindObjectOfType<SFXManager>();
        animator = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        if (!playerCreated)
        {
            playerCreated = true;
            DontDestroyOnLoad(this.transform.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        playerTalking = false;
        lastMovement = new Vector2(0, -1);
    }


    // Update is called once per frame
    void Update()
    {
        walking = false;

        if (playerTalking)
        {
            playerRigidBody.velocity = Vector2.zero;
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            attacking = true;
            attackTimeCounter = attackTime;
            playerRigidBody.velocity = Vector2.zero;
            animator.SetBool(attackingState, true);

            sfxManager.playerAttack.Play();
        }

        if (attacking)
        {
            attackTimeCounter -= Time.deltaTime;
            if (attackTimeCounter < 0)
            {
                attacking = false;
                animator.SetBool(attackingState, false);
            }
        }
        else
        {

            if(Mathf.Abs(Input.GetAxisRaw(horizontal))>0.5f || Mathf.Abs(Input.GetAxisRaw(vertical)) > 0.5f)
            {
                walking = true;
                lastMovement = new Vector2(Input.GetAxisRaw(horizontal), Input.GetAxisRaw(vertical));
                playerRigidBody.velocity = lastMovement.normalized * speed * Time.deltaTime;
                
            }

            /*if (Mathf.Abs(Input.GetAxisRaw(horizontal)) > 0.5f)
            {
                *this.transform.Translate(new Vector3(Input.GetAxisRaw(horizontal) * speed * Time.deltaTime,0,0));*
                playerRigidBody.velocity = new Vector2(Input.GetAxisRaw(horizontal) * currentSpeed * Time.deltaTime, playerRigidBody.velocity.y);
                walking = true;
                lastMovement = new Vector2(Input.GetAxisRaw(horizontal), 0);
            }
            else
            {
                playerRigidBody.velocity = new Vector2(0f, playerRigidBody.velocity.y);
            }


            if (Mathf.Abs(Input.GetAxisRaw(vertical)) > 0.5f)
            {
                *this.transform.Translate(new Vector3(0,Input.GetAxisRaw(vertical) * speed * Time.deltaTime, 0));*
                playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, Input.GetAxisRaw(vertical) * currentSpeed * Time.deltaTime);
                walking = true;
                lastMovement = new Vector2(0, Input.GetAxisRaw(vertical));
            }
            else
            {
                playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 0f);
            }

            if (Mathf.Abs(Input.GetAxisRaw(horizontal)) > 0.5f && Mathf.Abs(Input.GetAxisRaw(vertical)) > 0.5f)
            {
                currentSpeed = speed / Mathf.Sqrt(2);
            }
            else
            {
                currentSpeed = speed;
            }*/
        }
        if (!walking)
        {
            playerRigidBody.velocity = Vector2.zero;
        }

        animator.SetFloat(horizontal, Input.GetAxisRaw(horizontal));
        animator.SetFloat(vertical, Input.GetAxisRaw(vertical));
        animator.SetBool(walkingState, walking);
        animator.SetFloat(lastHorizontal, lastMovement.x);
        animator.SetFloat(lastVertical, lastMovement.y);
    }
}
