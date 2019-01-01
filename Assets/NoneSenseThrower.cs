
using UnityEngine;

public class NoneSenseThrower : MonoBehaviour {

    public GameObject[] thingsToThrow;

    [SerializeField]
    private float InitialDelayBeforeFirstThrow=10;
    [SerializeField]
    private float DelayBetweenThrowning = 4;
    private float throwCountDownCounter;

	void Start () {
        throwCountDownCounter = InitialDelayBeforeFirstThrow;
    }
	
	void Update () {
        if(throwCountDownCounter < 0)
        {
            int indexOfThingToThrow = Random.Range(0, thingsToThrow.Length);
            var position = new Vector3(Random.Range(-6, 6), -2, transform.position.z + 3);
            var thingy = Instantiate(thingsToThrow[indexOfThingToThrow], position, Random.rotation);

            // get the rigid body from children because we have a wrapper to change the model pivot
            var thingyRigidBody = thingy.GetComponentInChildren<Rigidbody>();
            float direction = 1;
            if (position.x < 0)
                direction = -1;
            thingyRigidBody.AddForce(new Vector3(direction * Random.Range(0,4) , 9, 0), ForceMode.Impulse);

            thingyRigidBody.AddTorque(1, 1, Random.Range(5f, 20f), ForceMode.Impulse);

            Destroy(thingy, 8);

            throwCountDownCounter = DelayBetweenThrowning;
        }
        throwCountDownCounter -= Time.deltaTime;

    }
}
