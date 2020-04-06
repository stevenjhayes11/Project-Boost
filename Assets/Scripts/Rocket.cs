using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    int currentScene = 0;
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
        if (currentScene == 0)
        {
            currentScene++;
        } 
        switch (collision.gameObject.tag) 
        {
            
            case "Finish":
                print("Hit Finish");
                if (currentScene == 0)
                {
                    currentScene++;
                }
                SceneManager.LoadScene(currentScene);
                break;
            case "Friendly":
                print("Friendly");
                break;
            default:
                print("Dead");
                currentScene = 0;
                SceneManager.LoadScene(currentScene);
                break;
        }

    }
    private void ProcessInput()
    {
        Thrust();
        Rotate();
        Flip();
    }

    private void Flip()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            print("here");
            float newZ = rigidBody.transform.eulerAngles.z + 180;
            rigidBody.transform.eulerAngles = new Vector3(rigidBody.transform.eulerAngles.x, rigidBody.transform.eulerAngles.y, newZ);

        }
        
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
