/*
Copyright 2023 Micah Schuster

Redistribution and use in source and binary forms, with or without modification,
are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this
list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice,
this list of conditions and the following disclaimer in the documentation and/or
other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE
OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
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
    public float maxFallTime;

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



    private Entity entity;
    private EntityAbility entityAbility;
    public System.Action DirectionAction;


    // Start is called before the first frame update
    void Start()
    {

        entity = GetComponent<Entity>();
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
        Vector3 leftDir = LeftLower.position - LeftOrigin.position;
        Vector3 rightDir =  RightLower.position - RightOrigin.position;
        rayhit1 = Physics2D.Raycast(RightOrigin.position, rightDir, rightDir.magnitude, rayLayerMask);
        rayhit2 = Physics2D.Raycast(LeftOrigin.position, leftDir, leftDir.magnitude, rayLayerMask);

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
        if (entityAbility.getCurrentAbility().movementSpeedModifier==0) return false;
        if (!isGrounded()) return false;

        //change direction
        float raycastDistanceHoriz = 0.075f;

        rayhit3 = Physics2D.Raycast(RightOrigin.position, Vector3.right, raycastDistanceHoriz, rayLayerMask);
        rayhit4 = Physics2D.Raycast(LeftOrigin.position, Vector3.left, raycastDistanceHoriz, rayLayerMask);
        if (drawRaycasts)
        {
            Debug.DrawRay(LeftOrigin.position, Vector3.right * raycastDistanceHoriz, Color.red, 0.01f);
            Debug.DrawRay(RightOrigin.position, Vector3.left * raycastDistanceHoriz, Color.red, 0.01f);
        }

        

        if(rayhit3.collider!=null && rayhit3.collider.tag == "ground")
        {
            DirectionAction?.Invoke();
            return true;
        }
        else if (rayhit4.collider != null && rayhit4.collider.tag == "ground")
        {
            DirectionAction?.Invoke();
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

    public bool getGrounded()
    {
        return grounded;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "lava")
        {
            entity.death();
        }
        if(collision.gameObject.tag == "grinder")
        {
            //Not a good place to do this stuff, but it works
            GameManager.instance().score();
            collision.gameObject.GetComponentInParent<Grinder>().updateBloody();
            entity.death();
        }
    }
}
