using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour {

    private GameObject torchUnlit;
    private GameObject torchLit;
    private GameObject lightAnimation;
    private GameObject spotLight;
    //[SerializeField] private float torchDuration = 5.0f;

	// Use this for initialization ... Look at changing to a looping function
	private void Start () {
        torchUnlit = this.gameObject.transform.GetChild(0).gameObject;
        torchLit = this.gameObject.transform.GetChild(1).gameObject;
        lightAnimation = this.gameObject.transform.GetChild(2).gameObject;
        spotLight = this.gameObject.transform.GetChild(3).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            torchUnlit.SetActive(false);
            torchLit.SetActive(true);
            lightAnimation.SetActive(true);
            spotLight.SetActive(true);
        }
    }
    
}
