using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObstacleManager : MonoBehaviour
{
    private float last_time_add_score;
    private int current_score;
    private float last_time_spawn_obstacle;
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private Transform spawn_point;
    [SerializeField] private float spawn_rate;
    [SerializeField] private TMP_Text score_text;

    private void Update()
    {
        if(last_time_spawn_obstacle < Time.time)
        {
            SpawnObstacle();
            last_time_spawn_obstacle = Time.time + 1f/spawn_rate;
        }

        if(last_time_add_score < Time.time)
        {
            current_score++;
            score_text.text = $"Score : {current_score}";
            last_time_add_score = Time.time + 1f;
        }
    }

    private void SpawnObstacle()
    {
        Instantiate(obstacles[Random.Range(0, obstacles.Length)], spawn_point.position, Quaternion.identity);
    }
}
