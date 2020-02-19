using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globe : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            //transform.localRotation *= Quaternion.Euler()
        }
    }

    public void Rotate(Vector3 eulerAngles)
    {
         
    }
}
