using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float thrust = 1000.0f;
    [SerializeField] float rotation = 250.0f;
    [SerializeField] AudioClip mainEngine;
    
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
            rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
            if (!audioSource.isPlaying){
                audioSource.PlayOneShot(mainEngine);
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotation);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotation);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // Freezing the physics rotation to do manual rotation.
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // Unfreezing the physics rotation.
    }
}
