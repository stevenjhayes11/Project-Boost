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
        float rotation = 40 * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {

            rigidBody.AddRelativeForce(Vector3.up);
            print("Space pressed");
        }
        if(Input.GetKey(KeyCode.D))
        {
            print("Right pressed");
            transform.Rotate(Vector3.back, rotation);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            print("Left pressed");
            transform.Rotate(Vector3.forward, rotation);
        }
    }
}
