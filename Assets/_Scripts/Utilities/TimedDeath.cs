using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDeath : MonoBehaviour
{
    [Header("Properties")]
    public float TimeToDie;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        time = TimeToDie;
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
