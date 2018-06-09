using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateMove : MonoBehaviour {
    
    private float direction = 1;

    [SerializeField]
    private float speed;

	void Start () {
		
	}
	
	
	void FixedUpdate () {
        if (transform.position.x > 5.5 || transform.position.x < -5.5)
            direction *= -1;

        float f = Time.deltaTime * direction * speed;
        transform.position = transform.position + new Vector3(f, 0, 0);
	}
}
