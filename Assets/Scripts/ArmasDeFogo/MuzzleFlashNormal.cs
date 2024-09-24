using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MuzzleFlashNormal : MonoBehaviour
{
    [SerializeField] protected Animator MuzzleAnimator;
    [SerializeField] protected string ShootType;

    protected void OnEnable() 
    {
        Shoot();
    }

    protected virtual void Shoot()
    {
        MuzzleAnimator.SetTrigger(ShootType);
    }
}
