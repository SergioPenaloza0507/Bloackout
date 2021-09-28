using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Transform mainCameraTransform = null;

    private GameSettings.PlayerMovementSettings settings;
    private Rigidbody rb;
    [SerializeField] private ParticleSystem dashParticles;
    private void Awake()
    {
        if (Camera.main != null) mainCameraTransform = Camera.main.transform;
        settings = GameSettings.Instiance.playerMovementSettings;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
       Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void Move(float x, float y)
    {
        var up = Vector3.up;
        Vector3 upVector = Vector3.ProjectOnPlane(mainCameraTransform.forward, up).normalized * y;
        Vector3 rightVector = Vector3.ProjectOnPlane(mainCameraTransform.right, up).normalized * x;
        Vector3 dir = (upVector + rightVector).normalized;
        transform.LookAt(transform.position + dir);
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(dir * settings.dashSpeed, ForceMode.Acceleration);
            dashParticles.Play();
            return;
        }
        transform.Translate(dir * (settings.movementSpeed * Time.deltaTime),Space.World);
    }
}
