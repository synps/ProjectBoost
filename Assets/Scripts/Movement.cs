using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float thrustValue = 1000f;
    [SerializeField] float rotationValue = 250f;
    [SerializeField] AudioClip audioClip;
    [SerializeField] ParticleSystem mainThrusterParticles, leftThrusterParticles, rightThrusterParticles;

    Rigidbody rb;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        audioSource.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();

    }

    void ProcessThrust()
    {
        if (audioSource.clip != audioClip)
            audioSource.clip = audioClip;
        if (audioSource.isPlaying != true)
            audioSource.Play();

        audioSource.loop = true;
        if (Input.GetKey(KeyCode.Space))
            StartThrusting();
        else
            StopThrusting();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
            RotateLeft();
        else if (Input.GetKey(KeyCode.D))
            RotateRight();
        else
            StopSideThrusterParticles();
    }


    private void StopThrusting()
    {
        audioSource.volume = 0.1f;
        mainThrusterParticles.Stop();
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustValue * Time.deltaTime);
        audioSource.volume = 1.0f;
        if (mainThrusterParticles.isPlaying == false)
            mainThrusterParticles.Play();
    }

    private void StopSideThrusterParticles()
    {
        leftThrusterParticles.Stop();
        rightThrusterParticles.Stop();
    }

    private void RotateRight()
    {
        ApplyRotation(rotationValue);
        if (leftThrusterParticles.isPlaying == false)
            leftThrusterParticles.Play();
    }

    private void RotateLeft()
    {
        ApplyRotation(-rotationValue);
        if (rightThrusterParticles.isPlaying == false)
            rightThrusterParticles.Play();
    }

    void ApplyRotation(float rotationValue)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationValue * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
