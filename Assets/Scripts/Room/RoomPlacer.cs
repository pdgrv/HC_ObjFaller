using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPlacer : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    //-3.7, -26, 17.5
    [SerializeField] private LevelGenerator _levelGenerator;

    private void Start()
    {
        PlaceRoom();
    }

    [ContextMenu("PlaceRoom")]
    private void PlaceRoom()
    {
        transform.position = _offset - new Vector3(0, _levelGenerator.PlatformsHeight, 0);
    }
}
