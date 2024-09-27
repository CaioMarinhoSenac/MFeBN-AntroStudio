using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorrerSomScript : MonoBehaviour
{
    public AudioSource somMorrer;

    private void OnEnable()
    {
        somMorrer.Play();
    }
}
