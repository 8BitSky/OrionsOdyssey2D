using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    private PlatformEffector2D effector;
    private bool platformReset = true;

    // Use this for initialization
    void Start() {
        effector = GetComponent<PlatformEffector2D>();
        effector.rotationalOffset = 0.0f;
    }

    private void OnCollisionStay2D(Collision2D other)
    {

        if (platformReset == true && Input.GetKey(KeyCode.S))
        {
            effector.rotationalOffset = 180f;
            platformReset = false;
            StartCoroutine(ResetPlatform());

        }

    }

    IEnumerator ResetPlatform()
    {
        yield return new WaitForSeconds(0.5f);
        effector.rotationalOffset = 0.0f;
        platformReset = true;
    }
}
