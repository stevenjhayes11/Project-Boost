using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    int currentScene = 0;
    Rigidbody rigidBody;
    [SerializeField] float rotationSpeed = 40f;
    [SerializeField] float thrustSpeed = 40f;
    

    enum State {Alive, Dying, Advancing}
    State state = State.Alive;

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

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }
    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(state == State.Alive)
        {
            switch (collision.gameObject.tag)
            {

                case "Finish":
                    Invoke("LoadNextScene", 1f);
                    state = State.Advancing;
                    break;
                case "Friendly":
                    break;
                default:
                    state = State.Dying;
                    AudioSource engineSound = GetComponent<AudioSource>();
                    engineSound.Stop();
                    Invoke("LoadFirstLevel", 3f);
                    break;
            }
        }
        

    }
    private void ProcessInput()
    {
        if(!(state == State.Dying))
        {
            Thrust();
            Rotate();
            Flip();
        }
        
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
