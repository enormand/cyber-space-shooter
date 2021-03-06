﻿using UnityEngine;
using System.Collections;

public class MunitionController : MonoBehaviour {

	public float damageFactor = 1f;
	public float fireVelocity = 5f;
	public float destroyBelowVelocity = 1f;

	private ExplosionManager explosionManager;
	private bool isExploding = false;

	void Awake () {
		explosionManager = GetComponent<ExplosionManager> ();
	}

	void FixedUpdate () {
		DestroyBelowVelocity ();
	}

	/*
	 * Destroy the munition if it's velocity is below the threshold 'destroyBelowVelocity'.
	 */
	void DestroyBelowVelocity () {
		float velocityMagnitude = gameObject.GetComponent<Rigidbody> ().velocity.magnitude;
		if (!isExploding && velocityMagnitude < destroyBelowVelocity 
			&& velocityMagnitude != 0) { // Don't destroy it at it's creation, before the force has been applied by the weapon
			Destroy (gameObject);
		}
	}

	/*
	 * Hit every object with a LifeController and inflict damages.
	 */
	void OnCollisionEnter (Collision colision) {
		LifeShieldManager otherLife = colision.gameObject.GetComponent<LifeShieldManager> ();
		if (otherLife != null) {
			otherLife.Hit (damageFactor);
		}

		isExploding = true;
		System.Action<GameObject> explodeCallback = (GameObject explosion) => {
			Destroy (gameObject);
		};
		StartCoroutine (explosionManager.Explode(explodeCallback));
	}
}
