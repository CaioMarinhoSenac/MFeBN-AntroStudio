using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparenciaDeCenario : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(true);
    }
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
