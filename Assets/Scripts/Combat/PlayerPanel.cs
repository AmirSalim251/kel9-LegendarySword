using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }    

    public void MoveUp()
    {
        transform.position += new Vector3(0, 25f, 0);
    }

    public void MoveDown()
    {
        transform.position -= new Vector3(0, 25f, 0);
    }

    public void Die()
    {
        animator.Play("Dead");
    }
}
