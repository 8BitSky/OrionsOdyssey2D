using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        ICollectable pickup = other.GetComponent<ICollectable>();

        if(pickup != null)
        {
            pickup.Collect();
        }
    }
}
