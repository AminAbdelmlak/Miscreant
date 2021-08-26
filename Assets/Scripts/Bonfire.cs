using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    public GameObject Explosion;

    void OnDestroy()
    {
        Instantiate(Explosion, transform.position, Quaternion.identity);
    }
}
