using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    public float speed;

    private bool Moving = true;
    private bool Falling = false;

	void Update () {
        if(Moving)
            transform.position = transform.position + new Vector3(0,0, speed * Time.deltaTime);
        
        if(Falling)
            transform.position = transform.position + new Vector3(0, -1 * 3 * Time.deltaTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Spear trigger enter");        

        var destructible = other.gameObject.GetComponent<Destructible>();
        if (destructible == null)
            return;

        Moving = false;

        var audioSource = GetComponent<AudioSource>();
        audioSource.Play();

        destructible.AddStuckProjectile(this);

        destructible.TakeDamage(10);
    }

    internal void Unstuck()
    {
        Falling = true;
        Destroy(gameObject, 4);
    }
}
