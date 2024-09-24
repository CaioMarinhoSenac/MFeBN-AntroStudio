using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisaoProjetil : MonoBehaviour
{
    [SerializeField] protected Animator CollisionAnimator;
    [SerializeField] protected string ColisionType;
    private void Start()
    {
        Destroy(gameObject, 0.25f);
    }
    

    protected void OnEnable()
    {
        Collide();
    }

    protected virtual void Collide()
    {
        CollisionAnimator.SetTrigger(ColisionType);
    }
}
