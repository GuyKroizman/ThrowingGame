
using UnityEngine;

public class NoneSenseThrower : MonoBehaviour {

    public GameObject[] thingsToThrow;

    private float throwCountDownCounter;

	void Start () {
        throwCountDownCounter = 2;

    }
	
	void Update () {
        if(throwCountDownCounter < 0)
        {
            int indexOfThingToThrow = Random.Range(0, thingsToThrow.Length);
            var position = new Vector3(Random.Range(-6, 6), -2, transform.position.z + 2);
            var thingy = Instantiate(thingsToThrow[indexOfThingToThrow], position, Random.rotation);

            var thingyRigidBody = thingy.GetComponent<Rigidbody>();
            float direction = 1;
            if (position.x < 0)
                direction = -1;
            thingyRigidBody.AddForce(new Vector3(direction * Random.Range(0,4) , 9, 0), ForceMode.Impulse);

            Destroy(thingy, 8);

            throwCountDownCounter = 1;
        }
        throwCountDownCounter -= Time.deltaTime;

    }
}
