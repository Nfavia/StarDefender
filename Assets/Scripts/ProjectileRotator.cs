using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRotator : MonoBehaviour
{
    float rotateSpeed = 50f;

    bool rotateOn = false;   

    void Update()
    {
        if(rotateOn)
        {
            transform.Rotate(transform.rotation.x, transform.rotation.y, 
                                rotateSpeed);
        }
        
    }

    public bool TurnRoationOn(float speed)
    {
        rotateSpeed = speed;
        return rotateOn = !rotateOn;
    }
}
