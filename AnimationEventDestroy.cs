using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventDestroy : MonoBehaviour {

    public void DestroyCoin()
    {
        Destroy(transform.parent.gameObject);
    }
}
