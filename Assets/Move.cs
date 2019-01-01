using UnityEngine;

public class Move : MonoBehaviour {

    public float speed;

    private AudioSource audioSource;
    public AudioEvent CrateImpact;

    private bool moving = false;
    private bool falling = false;
    private Vector3 initialPosition;

    private SpearPowerChargeEffects spearPowerChargeEffects;

    private void Start()
    {
        initialPosition = transform.position;
        audioSource = GetComponent<AudioSource>();

        spearPowerChargeEffects = new SpearPowerChargeEffects(gameObject);

        spearPowerChargeEffects.StartEffects();
    }
    // return how much to decrease the y position (height), based on the current y position.
    private float DropEasing()
    {
        float abs = Mathf.Abs(transform.position.y);
        float minabs = Mathf.Max(1, abs);        
        
        float drop = minabs * 2f * Time.deltaTime;
        return -1 * drop;
    }

    void Update () {
        if(moving)
            transform.position = transform.position + new Vector3(0, DropEasing( ), speed * Time.deltaTime);
        
        if(falling)
            transform.position = transform.position + new Vector3(0, -1 * 10 * Time.deltaTime, 0);

        spearPowerChargeEffects.Update();

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
        var destructible = other.gameObject.GetComponent<Destructible>();
        if (destructible == null)
            return;

        moving = false;

        if (!IsHitCrateFrontPanel(other))
        {
            falling = true;
            return;
        }

        int hitPoints = GetHitPoints(other.gameObject.transform.position.x) * spearPowerChargeEffects.GetPower();
        
        GameScore.score += hitPoints;

        CrateImpact.Play(audioSource);

        destructible.AddStuckProjectile(this);

        destructible.TakeDamage(hitPoints);
    }

    private static int GetHitPoints(float hitPosition)
    {
        float distanceFromCrateCenter = Mathf.Abs(hitPosition);
        float normalizedDistance = Mathf.Min(distanceFromCrateCenter, 1);
        float invertDistance = 1 - normalizedDistance;
        int pointsForHit = (int)(10.0f * invertDistance) + 1;
        return pointsForHit;
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

    public void StartMoving()
    {
        spearPowerChargeEffects.StopEffects();
        moving = true;
    }
}
