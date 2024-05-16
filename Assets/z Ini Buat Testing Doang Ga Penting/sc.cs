using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        // Animator animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("a")) 
        {
            animator.SetTrigger("satu");
            // animator.Play("1");
        }
        if(Input.GetKeyDown("b")) animator.Play("2");
    }
}
