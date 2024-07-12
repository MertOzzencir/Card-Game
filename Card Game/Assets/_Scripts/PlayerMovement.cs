using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    [SerializeField] private float _speed = 12f;
    public int PlayerHealth;
    public int AttackDamage = 2;
    public int HealtValue = 2;
    public bool gotSilence = false;
    float _mouseX;
    float _xRotation;
    private void Start()
    {
        instance = this;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        Movement();
        XRotate();



    }
    void Movement()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        transform.Translate(input * Time.deltaTime * _speed);
    }
    void XRotate()
    {
        _mouseX = Input.GetAxisRaw("Mouse X");
        _xRotation += _mouseX;
        transform.localRotation = Quaternion.Euler(0, _xRotation, 0);

    }
}
