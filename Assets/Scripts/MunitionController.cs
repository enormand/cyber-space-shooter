﻿using UnityEngine;
using System.Collections;

public class MunitionController : MonoBehaviour {

	public float damageFactor = 1f;
	public float fireForce = 100f;

	void OnCollisionEnter (Collision colision) {
		Debug.Log (colision.gameObject.name);
		LifeController otherLife = colision.gameObject.GetComponent<LifeController> ();
		if (otherLife != null) {
			otherLife.Hit (damageFactor);
		}
		Destroy (this.gameObject);
	}
}