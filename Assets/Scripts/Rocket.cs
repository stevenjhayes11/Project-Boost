using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    [SerializeField] float rotationSpeed = 40f;
    [SerializeField] float thrustSpeed = 40f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip levelCompleteSound;
    [SerializeField] float levelLoadDelay;
    [SerializeField] ParticleSystem engineParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem victoryParticles;

    bool detectCollisions;
    AudioSource audioSource;
    enum State {Alive, Dying, Advancing}
    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        detectCollisions = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ProcessInput();
    }
    private void Update()
    {
        if(Debug.isDebugBuild)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                LevelComplete();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                detectCollisions = !detectCollisions;
            }
        }
        RespondToFlip();

    }

    private void LoadNextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        currentScene++;
        if (currentScene >= SceneManager.sceneCountInBuildSettings)
            currentScene = 0;
        SceneManager.LoadScene(currentScene);
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
                    LevelComplete();
                    break;
                case "Friendly":
                    break;
                default:
                    if (detectCollisions)
                    {
                        PlayerDeath();
                    }
                    break;
            }
        }
    }

    private void LevelComplete()
    {
        state = State.Advancing;
        audioSource.Stop();
        audioSource.PlayOneShot(levelCompleteSound);
        Invoke("LoadNextScene", levelLoadDelay);
        victoryParticles.Play();
    }
    private void PlayerDeath()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(deathSound);
        Invoke("LoadFirstLevel", levelLoadDelay);
        deathParticles.Play();
        engineParticles.Stop();
    }

    private void ProcessInput()
    {
        if(state == State.Alive)
        {
            RespondToThrust();
            RespondToRotate();
        }
        
    }

    private void RespondToFlip()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            float newZ = rigidBody.transform.eulerAngles.z + 180;
            rigidBody.transform.eulerAngles = new Vector3(rigidBody.transform.eulerAngles.x, rigidBody.transform.eulerAngles.y, newZ);

        }
        
    }
    private void RespondToRotate()
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

    private void RespondToThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
            engineParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        engineParticles.Play();
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        rigidBody.AddRelativeForce((Vector3.up * thrustSpeed) * (1 + Time.deltaTime));
        
    }
}
