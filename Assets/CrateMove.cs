using UnityEngine;

public class CrateMove : MonoBehaviour {

    private int movementMethod;

    ContinuousRandomDestinationAndSpeed continuousRandomDestination;

    const float RANGE = 5.5f;

    private void Start()
    {
        movementMethod = Random.Range(0, 2);

        continuousRandomDestination = new ContinuousRandomDestinationAndSpeed(RANGE);
    }
    void FixedUpdate () {
        if (movementMethod == 0)
            transform.position = GetPosition();
        if (movementMethod == 1)
            transform.position = continuousRandomDestination.GetPosition(transform.position);
    }

    private Vector3 GetPosition()
    {              
        const float SPEED = 300;

        float f = Time.time * Time.deltaTime * SPEED;
        float x = Mathf.PingPong(f, 2*RANGE) - RANGE;
        return new Vector3(x, transform.position.y, transform.position.z);
    }


}
