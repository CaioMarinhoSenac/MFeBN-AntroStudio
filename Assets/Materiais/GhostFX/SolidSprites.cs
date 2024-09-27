using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidSprites : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Shader material;
    public Color color;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = Shader.Find("GUI/Text Shader");
    }

    // Update is called once per frame
    void Update()
    {
        ColorSprite();
    }

    void ColorSprite()
    {
        spriteRenderer.material.shader = material;
        spriteRenderer.color = color;
    }

    void Finish()
    {
        gameObject.SetActive(false);
    }
}
