using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Camera _main;
    float _yMouse;
    float _yRotation;
    void Start()
    {
        _main = Camera.main;
    }

    void Update()
    {
        YRotate();  

    }
    void YRotate()
    {
        _yMouse = Input.GetAxisRaw("Mouse Y");
        _yRotation -= _yMouse;
        _main.transform.localRotation = Quaternion.Euler(_yRotation, 0, 0);
    }
}
