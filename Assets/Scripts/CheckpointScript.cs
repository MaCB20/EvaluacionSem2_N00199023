using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointScript : MonoBehaviour
{
    // public Text levelSave;
    // private float appear = 0.1f;
    // private float dissapear = 3;
    private RespawnScript respawn;
    private BoxCollider2D checkpoint;

    // public void EnableText()
    // {
    //     levelSave.enabled = true;
    //     dissapear = Time.time + appear;
    // }
    void Start()
    {
        checkpoint = GetComponent<BoxCollider2D>();
        respawn = GameObject.FindGameObjectWithTag("Respawn").GetComponent<RespawnScript>();
    }
    // void Update()
    // {
    //     if (levelSave.enabled && (Time.time >= dissapear))
    //     {
    //         levelSave.enabled = false;
    //     }
    // }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            respawn.respawnPoint = this.gameObject;
            // levelSave.gameObject.SetActive(true);
            checkpoint.enabled = false;
        }
    }
}
