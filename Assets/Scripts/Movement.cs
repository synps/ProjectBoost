using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float thrustValue = 1000f;
    [SerializeField] float rotationValue = 250f;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();

    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
            rb.AddRelativeForce(Vector3.up * thrustValue * Time.deltaTime);
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
            ApplyRotation(rotationValue);
        else if (Input.GetKey(KeyCode.D))
            ApplyRotation(-rotationValue);

    }
    void ApplyRotation(float rotationValue)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationValue * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
