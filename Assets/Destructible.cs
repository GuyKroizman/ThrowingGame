using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

    public GameObject DestroyVersion;

    [SerializeField]
    private int StartHealth;

    [SerializeField]
    private Shooter Shooter;

    private int CurrentHealth;

    private List<Move> StuckProjectile = new List<Move>();

    private void Start()
    {
        CurrentHealth = StartHealth;
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;

        if(CurrentHealth <= 0)
        {
            var clone = Instantiate(gameObject);
            clone.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 70);

            Shooter.MoveToNextTarget();

            Destruct();
        }
    }

    public void AddStuckProjectile(Move move)
    {
        StuckProjectile.Add(move);
        move.transform.SetParent(transform);
    }

    private void Destruct()
    {
        foreach (Move p in StuckProjectile)
        {
            p.Unstuck();
        }

        StuckProjectile.Clear();

        Instantiate(DestroyVersion, transform.position, transform.rotation);
        transform.DetachChildren();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("on trigger");

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("On collision !!!");
    }
}
