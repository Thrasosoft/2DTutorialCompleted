using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDeath : MonoBehaviour
{
    Transform spawnPoint;
    void Start()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = spawnPoint.position;
        }
    }
}
