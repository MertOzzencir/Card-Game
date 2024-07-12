using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounter : MonoBehaviour
{
    public bool PlayerCounterAction;
    public LayerMask LayerMask;
    public GameObject _objectHolder;
    public int index;
    public GameObject counterColor;
    Color instanceColor;
    private void Start()
    {
        instanceColor = counterColor.GetComponent<MeshRenderer>().material.color;
        
    }
    private void Update()
    {
        bool objectCheckter = Physics.CheckSphere(transform.position, 0.5f, LayerMask);
        if (objectCheckter)
        {
            Ray ray = new Ray(transform.position, transform.up);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit,5f,LayerMask) && PlayerCardSelection.canPlace && _objectHolder == null)
            {
                _objectHolder = hit.transform.gameObject;
                counterColor.GetComponent<MeshRenderer>().material.color = Color.green;
                hit.transform.position = transform.position + new Vector3(0,0.1f,0);
                SpaceWorldUIController.instance.ManaChanged(index, hit.transform.gameObject.GetComponent<ICards>().ManaCost(), PlayerCounterAction);

            }
           
         
        }
        else
        {
            counterColor.GetComponent<MeshRenderer>().material.color = instanceColor;
            _objectHolder = null;
            SpaceWorldUIController.instance.ManaChanged(index,0, PlayerCounterAction);

        }
     

    }



}
