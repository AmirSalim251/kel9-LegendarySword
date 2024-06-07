using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VFX : MonoBehaviour
{
    public static VFX instance;

    public GameObject damagePopupPrefab;
    public GameObject vfxPrefab;

    private Animator VFXAnimator;

    public float yOffset = 1f;

    void Awake()
    {
        instance = this;
    }

    public void Create(Vector3 position, string text, string target)
    {
        position.y += yOffset;

        GameObject damagePopup = Instantiate(damagePopupPrefab, position, Quaternion.identity);
        var temp = damagePopup.GetComponentInChildren<TextMeshProUGUI>();
        temp.text = text;

        GameObject VFX = Instantiate(vfxPrefab, position, Quaternion.identity);
        VFXAnimator = VFX.GetComponentInChildren<Animator>();
        VFXAnimator.Play("Attack");

        if (target == "Alex" || target == "Freya" || target == "Magnus")
        {
            damagePopup.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            VFX.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        }

        Destroy(damagePopup, 1f);
        Destroy(VFX, 1f);
    }
}
