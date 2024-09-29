using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{

    [SerializeField] float thrust = 1000.0f;
    [SerializeField] float rotation = 250.0f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem jetParticles;
    [SerializeField] ParticleSystem leftThrust;
    [SerializeField] ParticleSystem rightThrust;
    
    Rigidbody rb;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            EndThrusting();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!jetParticles.isPlaying)
        {
            jetParticles.Play();
        }
    }

    private void EndThrusting()
    {
        audioSource.Stop();
        jetParticles.Stop();
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            StartRightRotation();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            StartLeftRotation();
        }
        else
        {
            EndRotation();
        }
    }

    private void EndRotation()
    {
        leftThrust.Stop();
        rightThrust.Stop();
    }

    private void StartLeftRotation()
    {
        ApplyRotation(-rotation);
        if (!leftThrust.isPlaying)
        {
            leftThrust.Play();
            rightThrust.Stop();
        }
    }

    private void StartRightRotation()
    {
        ApplyRotation(rotation);
        if (!rightThrust.isPlaying)
        {
            rightThrust.Play();
            leftThrust.Stop();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // Freezing the physics rotation to do manual rotation.
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // Unfreezing the physics rotation.
    }

}
