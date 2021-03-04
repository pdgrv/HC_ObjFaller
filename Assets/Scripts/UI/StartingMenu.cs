using UnityEngine;
using UnityEngine.EventSystems;

public class StartingMenu : MonoBehaviour
{
    private void Update()
    {
#if (UNITY_ANDROID && !UNITY_EDITOR)
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) //-- for build
#elif (UNITY_EDITOR && UNITY_ANDROID)
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
#endif
                gameObject.SetActive(false);
    }
}