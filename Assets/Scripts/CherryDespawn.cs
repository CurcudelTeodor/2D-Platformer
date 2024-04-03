using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryDespawn : MonoBehaviour
{   
    void DespawnCherry()
    {
        Debug.Log("Im here");
        Destroy(gameObject);
    }
}
