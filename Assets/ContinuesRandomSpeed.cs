using UnityEngine;

internal class ContinuousRandomSpeed
{
    private readonly float RANGE;
    private float currentDestination;
    private float currentSpeed;

    public ContinuousRandomSpeed(float RANGE)
    {
        this.RANGE = RANGE;

        currentDestination = RANGE;
        UpdateRandomSpeed();
    }

    public Vector3 GetPosition(Vector3 currentPosition)
    {
        if (IsCloseToDestination(currentPosition.x))
        {
            currentDestination *= -1;
            UpdateRandomSpeed();
        }

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

    private void UpdateRandomSpeed()
    {        
        currentSpeed = Random.Range(3, 12);
    }
}