using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRepeating : MonoBehaviour
{
    private float last_time_spawn_ground;
    private Transform current_ground;
    [SerializeField] private GameObject ground_prefab;
    [SerializeField] private float repeating_rate;
    [SerializeField] private float ground_gap_distance;

    private void Update()
    {
        current_ground = transform.GetChild(transform.childCount - 1);
        if(Time.time > last_time_spawn_ground && transform.childCount < 15)
        {
            Vector2 new_pos = new Vector2(current_ground.position.x + ground_gap_distance, current_ground.position.y);
            GameObject new_ground = Instantiate(ground_prefab, new_pos, Quaternion.identity);
            new_ground.transform.SetParent(transform);
            last_time_spawn_ground = Time.time + 1f/repeating_rate;
        }
    }
}
