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

        transform.DetachChildren();
        var clone = Instantiate(gameObject);
        clone.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 70);

        var crateParts = Instantiate(DestroyVersion, transform.position, transform.rotation);
        Destroy(crateParts, 4);
        Destroy(gameObject);
    }

}
