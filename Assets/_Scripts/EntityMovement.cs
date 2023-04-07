using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EntityMovement : MonoBehaviour
{
    [Header("Movement Properties")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float fallSpeed;
    private int movementDirection;
    private float changeDirCooldown = 0.2f;
    private float changeDirTime = 0f;

    //physics variables
    private Rigidbody2D rb;

    //Raycast related variables
    [Header("Raycast Variables")]
    [SerializeField] private LayerMask rayLayerMask;
    [SerializeField] private Transform RightOrigin;
    [SerializeField] private Transform LeftOrigin;
    [SerializeField] private Transform RightLower;
    [SerializeField] private Transform LeftLower;
    private RaycastHit2D rayhit1;
    private RaycastHit2D rayhit2;
    private RaycastHit2D rayhit3;
    private RaycastHit2D rayhit4;
    private Vector2 temp;
    private bool grounded;

    [Header("Debug Variables")]
    [SerializeField] private bool drawRaycasts;



    private EntityAbility entityAbility;


    // Start is called before the first frame update
    void Start()
    {
        entityAbility = GetComponent<EntityAbility>();
        rb = GetComponent<Rigidbody2D>();
        movementDirection = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded())
            grounded = true;
        else
            grounded = false;

        if (changeDirTime <= 0)
        {
            if (changeDirection())
            {
                movementDirection *= -1;
            }
            changeDirTime = changeDirCooldown;
        }
        else
        {
            changeDirTime -= Time.deltaTime;
        }

    }

    private void FixedUpdate()
    {
        if (grounded)
        {
            move();
        }
        else
        {
            fallMove();
        }
    }

    private bool isGrounded()
    {
        float raycastDistanceDown=0.52f;
        Vector3 leftDir = LeftLower.position - LeftOrigin.position;
        Vector3 rightDir =  RightLower.position - RightOrigin.position;
        rayhit1 = Physics2D.Raycast(RightOrigin.position, rightDir, raycastDistanceDown, rayLayerMask);
        rayhit2 = Physics2D.Raycast(LeftOrigin.position, leftDir, raycastDistanceDown, rayLayerMask);

        if (drawRaycasts)
        {
            Debug.DrawRay(LeftOrigin.position, leftDir, Color.red, 0.01f);
            Debug.DrawRay(RightOrigin.position, rightDir, Color.red, 0.01f);
        }
        if (rayhit1.collider == null && rayhit2.collider == null)
        {
            return false;
        }
        if ((rayhit1.collider!=null && rayhit1.collider.tag == "ground") || (rayhit2.collider!=null && rayhit2.collider.tag == "ground"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool changeDirection()
    {
        //change direction
        float raycastDistanceHoriz = 0.05f;

        rayhit3 = Physics2D.Raycast(RightOrigin.position, Vector3.right, raycastDistanceHoriz, rayLayerMask);
        rayhit4 = Physics2D.Raycast(LeftOrigin.position, Vector3.left, raycastDistanceHoriz, rayLayerMask);
        if (drawRaycasts)
        {
            Debug.DrawRay(LeftOrigin.position, Vector3.right * raycastDistanceHoriz, Color.red, 0.01f);
            Debug.DrawRay(RightOrigin.position, Vector3.left * raycastDistanceHoriz, Color.red, 0.01f);
        }

        if(rayhit3.collider!=null && rayhit3.collider.tag == "ground")
        {
            return true;
        }
        else if (rayhit4.collider != null && rayhit4.collider.tag == "ground")
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    private void fallMove()
    {
        //move only in y at fall speed;
        temp.x = 0;
        temp.y = -fallSpeed*entityAbility.getModifiedFallSpeed();
        rb.velocity = temp;
    }

    private void move()
    {
        temp.x = movementDirection * walkSpeed * entityAbility.getModifiedMovementSpeed();
        temp.y = 0;
        rb.velocity = temp;
    }
}
