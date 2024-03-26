using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D body;

    private Master controls;
    private Vector2 moveInput;

    private Vector2 aimInput;
   // private bool isUsingMouse = false;

    public Transform gunTransform;

    private void Awake() {
        controls = new Master();
        body = GetComponent<Rigidbody2D>();
    }

    // kun objekti on aktiivinen
    private void OnEnable() {
        controls.Enable();      // aktivoi input controllsin
    }

    // kun objekti ei ole aktiivinen
    private void OnDisable() {
        controls.Disable();     // pysäyttää input controllsin
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move() {
        moveInput = controls.Player.Move.ReadValue<Vector2>();
        Vector2 movement = new Vector2(moveInput.x, moveInput.y) * moveSpeed * Time.fixedDeltaTime;
        body.MovePosition(body.position + movement);
    }

    private void Update() {
        Shoot();

        if (UsingMouse()){
            AimWithMouse();
        } else {

        }
    }

    private void Shoot() {
        if (controls.Player.Shoot.triggered){
            Debug.Log("ammu nappula toimii");
            GameObject bullet = BulletPoolManager.Instance.GetBullet();
            bullet.transform.position = gunTransform.position;
            bullet.transform.rotation = gunTransform.rotation;
        }
    }

    private bool UsingMouse() {
        if (Mouse.current.delta.ReadValue().sqrMagnitude > 0.1){
            return true;
        }
        return false;
    }

    private void AimWithMouse() {
        aimInput = controls.Player.Aim.ReadValue<Vector2>();

        if (aimInput.sqrMagnitude > 0.1){
            
            Vector3 mouseScreenPosition = Mouse.current.position.ReadValue();
            mouseScreenPosition.z = Camera.main.transform.position.z; // Ensure the z-coordinate is set to the camera's z-coordinate
            
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            mouseWorldPosition.z = 0; // Set the z-coordinate to zero (or whatever value is appropriate for your game)

            // Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseWorldPosition.current.position.ReadValue());
            // mouseWorldPosition.z = 0;

            Vector2 aimDirection = (mouseWorldPosition - gunTransform.position).normalized;

            aimDirection.y = -aimDirection.y;
            
            float angle = (Mathf.Atan2(aimDirection.x, aimDirection.y)) * Mathf.Rad2Deg;
            gunTransform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
