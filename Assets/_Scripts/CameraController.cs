using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed;
    private Vector3 moveAmount;

    // Start is called before the first frame update
    void Start()
    {
        moveAmount = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        moveAmount.x = Input.GetAxisRaw("Horizontal");
        moveAmount.y = Input.GetAxisRaw("Vertical");
        moveAmount.z = 0;

        Vector3 pos = this.transform.position;
        pos.x += moveAmount.x * speed * Time.deltaTime;
        pos.y += moveAmount.y * speed * Time.deltaTime;
        this.transform.position = pos;
    }
}
