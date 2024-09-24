using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAnimations : MonoBehaviour
{
    public Animator CamAnimator;

    public void CamShake()
    {
        CamAnimator.SetTrigger("Shake");
    }
}
