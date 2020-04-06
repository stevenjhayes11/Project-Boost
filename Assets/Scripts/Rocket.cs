using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    Rigidbody rigidBody;
    [SerializeField]float rotationSpeed = 40f;
    [SerializeField] float thrustSpeed = 40f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag) 
        {
            case "Friendly":
                print("Fine");
                break;
            default:
                print("Dead");
                break;
        }

    }
    private void ProcessInput()
    {
        Thrust();
        Rotate();
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true;
        
        float rotation = rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back, rotation);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward, rotation);
        }
        rigidBody.freezeRotation = false;
    }

    private void Thrust()
    {
        AudioSource engineSound = GetComponent<AudioSource>();
        if (Input.GetKey(KeyCode.Space))
        {
            if (!engineSound.isPlaying)
            {
                engineSound.Play();
            }
            rigidBody.AddRelativeForce(Vector3.up * thrustSpeed);
        }
        else
        {
            engineSound.Stop();
        }
    }
}
