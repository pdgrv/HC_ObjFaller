using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButton(0))
            gameObject.SetActive(false);
    }
}
