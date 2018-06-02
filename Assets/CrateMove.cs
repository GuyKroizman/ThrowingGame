using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateMove : MonoBehaviour {
    
    private float Direction = 1;

    [SerializeField]
    private float Speed;

	void Start () {
		
	}
	
	
	void FixedUpdate () {
        if (transform.position.x > 5.5 || transform.position.x < -5.5)
            Direction *= -1;

        float f = Time.deltaTime * Direction * Speed;
        transform.position = transform.position + new Vector3(f, 0, 0);
	}
}
