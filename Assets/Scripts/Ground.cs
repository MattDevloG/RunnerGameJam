using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    // Start is called before the first frame updat
    [SerializeField] private Sprite[] building_sprites;
    private SpriteRenderer building_sr;

    private void Start()
    {
        building_sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        building_sr.sprite = building_sprites[Random.Range(0, building_sprites.Length)];
    }

}
