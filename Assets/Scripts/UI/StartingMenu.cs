using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartingMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) //-- для билда
        //if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            gameObject.SetActive(false);
    }
}