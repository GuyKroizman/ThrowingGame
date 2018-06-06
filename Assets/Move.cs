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
            transform.position = transform.position + new Vector3(0, 0, speed * Time.deltaTime);
        
        if(Falling)
            transform.position = transform.position + new Vector3(0, -1 * 3 * Time.deltaTime);

        DestroyWhenTooFar();
	}

    private void DestroyWhenTooFar()
    {
        var distanceToCamera = Vector3.Distance(Camera.main.transform.position, transform.position);

        if (distanceToCamera > 50)
            Destroy(gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Spear trigger enter");        

        var destructible = other.gameObject.GetComponent<Destructible>();
        if (destructible == null)
            return;

        Moving = false;

        if(!IsHitCrateFrontPanel(other))
        {
            Falling = true;
            return;
        }

        var audioSource = GetComponent<AudioSource>();
        audioSource.Play();

        destructible.AddStuckProjectile(this);

        destructible.TakeDamage(10);
    }

    private bool IsHitCrateFrontPanel(Collider other)
    {
        var otherCollider = other.GetComponent<Collider>();        

        float crateFrontPanelZPosition = other.transform.position.z - otherCollider.bounds.size.z;

        return transform.position.z - crateFrontPanelZPosition < 0.6;
    }

    internal void Unstuck()
    {
        Falling = true;
        Destroy(gameObject, 4);
    }
}
