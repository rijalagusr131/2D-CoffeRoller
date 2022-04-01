using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private float rotz;
    public float rotationSpeed;
    public bool Clockwiserotation;


    // Update is called once per frame
    void Update()
    {
        if (Clockwiserotation == false)
        {
            rotz += Time.deltaTime * rotationSpeed;
        }
        else
        {
            rotz += -Time.deltaTime * rotationSpeed;
        }

        transform.rotation = Quaternion.Euler(0, 0, rotz);
    }
}
