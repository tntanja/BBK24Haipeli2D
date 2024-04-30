using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    private float currentSpeed = 3f;
    private Rigidbody2D body;

    private Transform playerTransform;
    
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.Instance.IsGamePlay()) {
            return;
        }

        GetPlayer();
        Move();
    }

    private void GetPlayer() {
        if(playerTransform == null) {
            playerTransform = GameManager.Instance.getPlayer.transform;
        }
    }

    private void Move() {
        if(playerTransform == null) {
            return;
        }
    }
}
