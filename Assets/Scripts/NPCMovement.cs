using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public float speed = 1.5f;
    private Rigidbody2D npcRigidBody;

    public bool isWalking, isTalking;

    public float walkTime = 1.5f;
    private float walkCounter;

    public float waitTime = 3.0f;
    private float waitCounter;

    private Vector2[] walkingDirection =
    {
        new Vector2(1,0),
        new Vector2(-1,0),
        new Vector2(0,1),
        new Vector2(0,-1)
    };

    private Vector2 directionToMakeStep;

    private int currentDirection;

    public BoxCollider2D villagerZone;

    private DialogManager manager;

    // Start is called before the first frame update
    void Start()
    {
        npcRigidBody = GetComponent<Rigidbody2D>();
        manager = FindObjectOfType<DialogManager>();
        waitCounter = waitTime;
        walkCounter = walkTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!manager.dialogActive)
        {
            isTalking = false;
        }

        if (isTalking)
        {
            StopWalking();
            npcRigidBody.velocity = Vector2.zero;
            return;
        }

        if (isWalking)
        {
            directionToMakeStep = speed * walkingDirection[currentDirection];
            if (villagerZone != null)
            {
                if (this.transform.position.x < villagerZone.bounds.min.x ||
                    this.transform.position.x > villagerZone.bounds.max.x||
                    this.transform.position.y < villagerZone.bounds.min.y||
                    this.transform.position.y > villagerZone.bounds.max.y)
                {
                    directionToMakeStep = (new Vector2(Random.Range(villagerZone.bounds.min.x, villagerZone.bounds.max.x) - this.transform.position.x, Random.Range(villagerZone.bounds.min.y, villagerZone.bounds.max.y) - this.transform.position.y).normalized * speed);
                }
            }           
            npcRigidBody.velocity = directionToMakeStep;
            walkCounter -= Time.deltaTime;
            if (walkCounter < 0)
            {               
                StopWalking();
            }
        }else if (!isWalking)
        {
            npcRigidBody.velocity = Vector2.zero;
            waitCounter -= Time.deltaTime;
            if (waitCounter < 0)
            {
                StartWalking();
            }
        }
    }

    private void StartWalking()
    {
        isWalking = true;
        currentDirection = Random.Range(0, 4);
        walkCounter = walkTime;
    }

    private void StopWalking()
    {
        isWalking = false;
        waitCounter = waitTime;
        
    }

}
