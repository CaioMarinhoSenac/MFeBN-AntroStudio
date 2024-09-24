using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParallaxMouseEffect : MonoBehaviour
{
    [Tooltip("Starts from the furthest object to the nearest")]
    [SerializeField] private GameObject[] parallaxObjects;
    [SerializeField] private float mouseSpeedX = 1f, mouseSpeedY = 0.2f;
    private Vector3[] OriginalPositions;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;

        OriginalPositions = new Vector3[parallaxObjects.Length];
        for(int i = 0; i < parallaxObjects.Length; i++)
        {
            OriginalPositions[i] = parallaxObjects[i].transform.position;
        }
    }

    void FixedUpdate()
    {
        float x, y;

        x = (Input.mousePosition.x - (Screen.width / 2)) * mouseSpeedX / Screen.width;
        y = (Input.mousePosition.y - (Screen.height / 2)) * mouseSpeedY / Screen.height;

        for (int i = 1; i < parallaxObjects.Length + 1; i++)
        {
            parallaxObjects[i - 1].transform.position = OriginalPositions[i - 1] + (new Vector3(x, y, 0f) * i * 
                ((i - 1) - (parallaxObjects.Length / 2)));
        }
    }
}
