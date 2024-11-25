using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSystem : MonoBehaviour
{
    public GameObject template;
    List<Obstacle> obstacles = new List<Obstacle>();
    Coroutine coroutine = null; 
    public void StartRun()
    {
        coroutine = StartCoroutine(GenerateObstacles());
    }
    public void Init()
    {
        for(int i = 0; i < obstacles.Count; i ++)
        {
            Destroy(obstacles[i].gameObject);
        }
        obstacles.Clear();
    }
    public void Stop()
    {
        StopCoroutine(coroutine);
        for (int i = 0; i < obstacles.Count; i++)
        {
            obstacles[i].enabled = false;
        }
    }
    IEnumerator GenerateObstacles()
    {
        for(int i = 0; i < 3; i ++)
        {
            if (obstacles.Count < 3)
            {
                GenerateObstacle();
            }
            else
            {
                obstacles[i].enabled = true;
                obstacles[i].Init();
            }
            yield return new WaitForSeconds(1f);
        }
    }
    void GenerateObstacle()
    {
        if(obstacles.Count < 3)
        {
            GameObject obj = Instantiate(template, this.transform);
            Obstacle p = obj.GetComponent<Obstacle>();
            obstacles.Add(p);
        }
    }
}
