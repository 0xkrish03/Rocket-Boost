using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustManager = 100f;
    [SerializeField] float rotationStrength = 1f;

    Rigidbody rb;
    AudioSource audioSource;
    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        Thrusting();
        Rotating();
    }

    private void Thrusting()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustManager * Time.fixedDeltaTime);
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
            audioSource.Stop();
    }
    private void Rotating()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput > 0)
        {
            ForRoatating(-1);
        }
        else if (rotationInput < 0)
        {
            ForRoatating(1);
        }
    }

    private void ForRoatating(float b)
    {
        var originalConstraints = rb.constraints;
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationStrength * Time.fixedDeltaTime * b);
        rb.freezeRotation = false;
        rb.constraints = originalConstraints;
    }
}
