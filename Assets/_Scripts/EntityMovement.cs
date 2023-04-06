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

    //physics variables
    private Rigidbody2D rb;

    //Raycast related variables
    [Header("Raycast Variables")]
    [SerializeField] private LayerMask rayLayerMask;
    private RaycastHit2D rayhit1;
    private RaycastHit2D rayhit2;
    private Vector2 temp;
    private bool grounded;

    [Header("Debug Variables")]
    [SerializeField] private bool drawRaycasts;




    // Start is called before the first frame update
    void Start()
    {
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
        float raycastDistance=0.52f;
        Vector3 leftOrigin = this.transform.position - new Vector3(0.5f, 0f, 0f);
        Vector3 rightOrigin = this.transform.position + new Vector3(0.5f, 0f, 0f);
        Vector2 leftDir = (this.transform.position + new Vector3(-0.55f, -raycastDistance, 0f)) - leftOrigin;
        Vector2 rightDir = (this.transform.position + new Vector3(0.55f,-raycastDistance, 0f)) - rightOrigin;
        rayhit1 = Physics2D.Raycast(rightOrigin, rightDir, raycastDistance, rayLayerMask);
        rayhit2 = Physics2D.Raycast(leftOrigin, leftDir, raycastDistance, rayLayerMask);

        if (drawRaycasts)
        {
            Debug.DrawRay(leftOrigin, leftDir, Color.red, 0.01f);
            Debug.DrawRay(rightOrigin, rightDir, Color.red, 0.01f);

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


    private void fallMove()
    {
        //move only in y at fall speed;
        temp.x = 0;
        temp.y = -fallSpeed;
        rb.velocity = temp;
    }

    private void move()
    {
        temp.x = movementDirection * walkSpeed;
        temp.y = 0;
        rb.velocity = temp;
    }
}
