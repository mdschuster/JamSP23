using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grinder : MonoBehaviour
{

    public Animator anim;
    public RuntimeAnimatorController bloodyController;
    private bool isBloody;
    // Start is called before the first frame update
    void Start()
    {
        isBloody = false;
        anim = GetComponent<Animator>();
    }

    public void updateBloody()
    {
        if (!isBloody)
        {
            isBloody = true;
            anim.runtimeAnimatorController = bloodyController;
        }
    }
}
