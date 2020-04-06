using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    Rigidbody rigidBody;

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

    private void ProcessInput()
    {
        Thrust();
        Rotate();
    }

    private void Rotate()
    {
        float rotation = 40 * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back, rotation);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward, rotation);
        }
    }

    private void Thrust()
    {
        AudioSource engineSound = GetComponent<AudioSource>();
        if (Input.GetKey(KeyCode.Space))
        {
            print(engineSound.isPlaying);
            if (!engineSound.isPlaying)
            {
                engineSound.Play();
            }
            rigidBody.AddRelativeForce(Vector3.up);
        }
        else
        {
            engineSound.Stop();
        }
    }
}
