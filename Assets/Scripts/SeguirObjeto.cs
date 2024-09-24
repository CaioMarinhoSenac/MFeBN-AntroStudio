using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguirObjeto : MonoBehaviour
{
    public Transform objetoParaSeguir;
    RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(objetoParaSeguir != null)
        {
            rectTransform.anchoredPosition = objetoParaSeguir.localPosition;
        }
    }
}
