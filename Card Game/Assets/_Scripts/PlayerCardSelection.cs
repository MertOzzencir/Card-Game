using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class PlayerCardSelection : MonoBehaviour
{
    public LayerMask CardMask;
    GameObject selectedObject;
    public static bool canPlace;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 15f, CardMask) && RoundManager.instance.GeneralTurn == RoundManager.Turn.PlayerTurn)
        {
            selectedObject = hit.transform.gameObject;
            if (selectedObject.GetComponent<ICards>() != null)
            {
                if (Input.GetMouseButton(0) )
                {
                    selectedObject.layer = default;
                    Vector3 newPosition = selectedObject.transform.position;
                    newPosition.x = hit.point.x; 
                    newPosition.z = hit.point.z;
                    newPosition.y = 0.1f;
                    selectedObject.transform.position = newPosition ;
                    selectedObject.layer = 6;
                    canPlace = false;
                }
                else
                {
                    canPlace = true;
                }
               
            }
        }
    }
}
