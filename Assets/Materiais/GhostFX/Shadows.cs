using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadows : MonoBehaviour
{
    public static Shadows instance;
    public GameObject GhostFX;
    public List<GameObject> pool = new List<GameObject>();
    public float speed;
    public Color color;

    private float cronometro;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetShadows()
    {
        for (int i = 0; i < pool.Count; ++i)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                pool[i].transform.position = transform.position;
                pool[i].transform.rotation = transform.rotation;

                pool[i].GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
                pool[i].GetComponent<SolidSprites>().color = color;

                return pool[i];
            }
        }
        GameObject obj = Instantiate(GhostFX, transform.position, transform.rotation) as GameObject;

        obj.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
        obj.GetComponent<SolidSprites>().color = color;

        pool.Add(obj);  

        return obj;
    }

    public void SombrasSkill()
    {
        cronometro += speed * Time.deltaTime;

        if (cronometro > 1)
        {
            GetShadows();
            cronometro = 0;
        }
    }
}
