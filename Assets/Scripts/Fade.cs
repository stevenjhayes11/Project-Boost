using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    
    Material foreground;
    float currentA;
    [SerializeField] float factor;
    Color foregroundColor;
    bool increasing;
    // Start is called before the first frame update
    void Start()
    {
        foreground = GetComponent<Renderer>().material;
        foregroundColor = foreground.color;
        currentA = 0f;
        increasing = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (currentA < 1 && increasing)
        {
            currentA = currentA + (factor * Time.deltaTime);
        }
        else
        {
            increasing = false;
            currentA = currentA - (factor * Time.deltaTime);
            if(currentA <= 0)
            {
                increasing = true;
            }
        }

        Color newColor = new Color(foregroundColor.r, foregroundColor.g, foregroundColor.b, currentA);
        foreground.color = newColor;
        


    }
}
