using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed = -10;
    public float maxrange = 6;
    public float minrange = -1;
    private float t = 0;
    void Start()
    {
        this.Init();
    }
    
    public void Init()
    {
        float y = Random.Range(minrange, maxrange);
        this.transform.localPosition = new Vector3(4, y, 0);
    }
    void Update()
    {
        this.transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
        t += Time.deltaTime;
        if(t > 3f)
        {
            t = 0;
            this.Init();
        }
    }
}
