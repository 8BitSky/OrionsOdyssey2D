using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustParticles : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 0.25f);
	}
}
