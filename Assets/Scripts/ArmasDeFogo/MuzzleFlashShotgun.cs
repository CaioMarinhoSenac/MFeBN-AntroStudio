using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlashShotgun : MuzzleFlashNormal
{
    protected override void Shoot()
    {
        MuzzleAnimator.SetTrigger("ShootShotgun");
    }
}
