using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

    public GameObject DestroyVersion;

    [SerializeField]
    private int StartHealth;

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
        foreach(Move p in StuckProjectile)
        {
            p.Unstuck();
        }

        StuckProjectile.Clear();

        Instantiate(DestroyVersion, transform.position, transform.rotation);
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
