using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{

    [SerializeField] Vector3 movementVector;
    [Range(0,1)][SerializeField] float movementFactor;
    bool goRight = true;
    Vector3 startingPosition;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset;
        Transform wallPosition = GetComponent<Transform>();
        if(goRight)
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
    }
}
