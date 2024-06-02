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
        transform.localPosition += new Vector3(0, 40f, 0);
    }

    public void MoveDown()
    {
        transform.localPosition -= new Vector3(0, 40f, 0);
    }

    public void Die()
    {
        animator.Play("Dead");
    }

    public void OnHit()
    {
        animator.Play("OnHit");
    }
}
