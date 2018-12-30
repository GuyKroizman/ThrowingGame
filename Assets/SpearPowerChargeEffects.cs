using System;
using UnityEngine;

internal class SpearPowerChargeEffects
{
    private GameObject spear;
    private float timeSinceStart = 0;

    private bool isThisThingOn = false;
    
    private const float distanceToMoveBack = 0.1f;
    private const float timeToMoveBack = 0.4f;

    
    public SpearPowerChargeEffects(GameObject spear)
    {
        this.spear = spear;
    }

    internal void Update()
    {
        if (!isThisThingOn)
            return;

        timeSinceStart += Time.deltaTime;

        if (timeSinceStart > timeToMoveBack)
            return;

        float speed = distanceToMoveBack / timeToMoveBack;

        float z = spear.transform.position.z;
        z -= speed * timeSinceStart;
        Vector3 newPos = spear.transform.position;
        newPos.z = z;
        spear.transform.position = newPos; 
    }

    internal void StopEffects()
    {
        isThisThingOn = false;
    }

    internal void StartEffects()
    {
        isThisThingOn = true;
    }

    public int GetPower()
    {
        int Power = (int)timeSinceStart + 1;
        return Power * 10;
    }
}