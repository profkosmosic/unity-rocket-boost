using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    private AudioSource thrustSound;

    [SerializeField]private float thrust = 500f;
    [SerializeField]private float rotationThrust = 250f;

    [SerializeField]AudioClip mainEngine;

    [SerializeField]ParticleSystem mainEngineParticle;
    [SerializeField]ParticleSystem leftThrusterParticle;
    [SerializeField]ParticleSystem rightThrusterParticle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        thrustSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() {
        if(Input.GetKey(KeyCode.Space)) {
            rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime); // or (0, 1, 0)
            if(!thrustSound.isPlaying) {
                thrustSound.PlayOneShot(mainEngine);
            }
            else {
                thrustSound.Stop();
                mainEngineParticle.Stop();
            }
            if(!mainEngineParticle.isPlaying) {
                mainEngineParticle.Play();
            }
        }
    }
    void ProcessRotation() {
        if(Input.GetKey(KeyCode.A)) {
            ApplyRotation(rotationThrust);
            leftThrusterParticle.Play();
        }
        else if(Input.GetKey(KeyCode.D)) {
            ApplyRotation(-rotationThrust);
            rightThrusterParticle.Play();
        }
        else {
            leftThrusterParticle.Stop();
            rightThrusterParticle.Stop();
        }
    }
    void ApplyRotation(float rThrust) {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rThrust * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
