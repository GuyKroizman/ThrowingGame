using UnityEngine;

internal class ContinuousRandomDestinationAndSpeed
{
    private readonly float RANGE;
    private float currentDestination;
    private float currentSpeed;

    public ContinuousRandomDestinationAndSpeed(float RANGE)
    {
        this.RANGE = RANGE;

        GetNewRandomDestinationAndSpeed();
    }

    public Vector3 GetPosition(Vector3 currentPosition)
    {
        if (IsCloseToDestination(currentPosition.x))
            GetNewRandomDestinationAndSpeed();

        float step = Time.deltaTime * currentSpeed;        

        if (currentDestination < currentPosition.x)
            step *= -1;

        float x = currentPosition.x;
        x += step;

        return new Vector3(x, currentPosition.y, currentPosition.z);
    }

    private bool IsCloseToDestination(float currentPositionX)
    {
        return Mathf.Abs(currentPositionX - currentDestination) < 0.1;
    }

    private void GetNewRandomDestinationAndSpeed()
    {
        currentDestination = Random.Range(-1 * RANGE, RANGE);
        currentSpeed = Random.Range(3, 10);
    }
}