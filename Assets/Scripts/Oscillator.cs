using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    bool done = false;
    [SerializeField] Vector3 movementVector;
    [Range(0,1)][SerializeField] float movementFactor;
    bool goRight = true;
    Vector3 startingPosition;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float gameTime = Time.time;
        float modifier;
        Transform wallPosition = GetComponent<Transform>();
        modifier = Mathf.Sin(gameTime);
        Vector3 offset = movementVector * modifier;
        
        wallPosition.position = startingPosition + offset;

        /* if(goRight)
       {
            offset = movementVector * movementFactor;
            goRight = wallPosition.position.x <= (startingPosition.x + movementVector.x);
       }
        else
        {
            offset = -(movementVector * movementFactor);
            goRight = wallPosition.position.x <= startingPosition.x;
            print("here");
        }
        offset = offset / 100;
        wallPosition.position = wallPosition.position + offset; 
        */
    }
}
