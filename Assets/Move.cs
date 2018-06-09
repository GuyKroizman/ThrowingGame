using UnityEngine;

public class Move : MonoBehaviour {

    public float speed;

    private bool moving = true;
    private bool falling = false;
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    void Update () {
        if(moving)
            transform.position = transform.position + new Vector3(0, 0, speed * Time.deltaTime);
        
        if(falling)
            transform.position = transform.position + new Vector3(0, -1 * 3 * Time.deltaTime);

        DestroyWhenTooFar();
	}

    private void DestroyWhenTooFar()
    {
        var distance = Vector3.Distance(initialPosition, transform.position);

        if (distance > 50)
            Destroy(gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Spear trigger enter");        

        var destructible = other.gameObject.GetComponent<Destructible>();
        if (destructible == null)
            return;

        moving = false;

        if(!IsHitCrateFrontPanel(other))
        {
            falling = true;
            return;
        }

        var audioSource = GetComponent<AudioSource>();
        audioSource.Play();

        destructible.AddStuckProjectile(this);

        destructible.TakeDamage(10);
    }

    private bool IsHitCrateFrontPanel(Collider other)
    {
        var otherCollider = other.GetComponent<Collider>();        

        float crateFrontPanelZPosition = other.transform.position.z - otherCollider.bounds.size.z;

        return transform.position.z - crateFrontPanelZPosition < 0.6;
    }

    internal void Unstuck()
    {
        falling = true;
        Destroy(gameObject, 4);
    }
}
