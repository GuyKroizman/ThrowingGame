using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [SerializeField]
    private GameObject Projectile;

    // This is updated once the player breaks a crate to the next crate position
    private Vector3 WannaBePosition;

    // A variable to help keep fire rate at normal speed.
    // It will be set to false after a throw for about half a second.
    private bool AllowFire = true;
    private float CountDown;
    public float MinTimeBetweenThrowsSeconds = 0.5f;
    

    private void Start()
    {
        WannaBePosition = transform.position;
    }


    void Update()
    {
        UpdateAllowFire();

        UpdatePlayerPosition();

        if (Input.GetButtonDown("Fire1") && AllowFire)
        {
            Throw();             
        }

        if (IsTouchedScreen() && AllowFire)
        {
            Throw();
        }
    }

    private void UpdateAllowFire()
    {
        CountDown -= Time.deltaTime;
        if (CountDown <= 0)
        {
            AllowFire = true;
            CountDown = MinTimeBetweenThrowsSeconds;
        }
    }

    private void UpdatePlayerPosition()
    {
        transform.position = Vector3.Lerp(transform.position, WannaBePosition, Time.deltaTime * 2);
    }

    private bool IsTouchedScreen()
    {
        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                return true;
            }
        }

        return false;
    }

    private IEnumerator StartCountdown()
    {
        // a little delay before moving the player to the next position. 
        // so the player can see the crates break.
        int currCountdownValue = 1;
        while (currCountdownValue > 0)
        {
            yield return new WaitForSeconds(0.30f);
            currCountdownValue--;
        }

        // Set position to start move to. The update will move this until we reach WannaBePosition.
        WannaBePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 70);
    }

    private void Throw()
    {
        AllowFire = false;
        float y = Random.Range(-0.5f, 0.5f);

        Instantiate(Projectile, transform.position + new Vector3(0, y, 0), Quaternion.identity);

        var audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    // gets called when the crate is destructed.
    internal void MoveToNextTarget()
    {
        StartCoroutine(StartCountdown());
    }
}
