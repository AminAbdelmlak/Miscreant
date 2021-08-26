using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform Player;

    void LateUpdate()
    {
        Vector3 NewPosition = Player.position;
        //NewPosition.y = transform.position.y;
        NewPosition.z = transform.position.z;
        transform.position = NewPosition;
    }
}
