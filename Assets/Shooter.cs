using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    public Move spearPrefab;
    private Move spear;

    // This is updated once the player breaks a crate to the next crate position
    private Vector3 WannaBePosition;

    // A variable to help keep fire rate at normal speed.
    // It will be set to false after a throw for about half a second.
    private bool AllowFire = true;
    public float MinTimeBetweenThrowsSeconds = 5f;

    private AudioSource audioSource;
    

    private void Start()
    {
        WannaBePosition = transform.position;

        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        UpdatePlayerPosition();



        if (AllowFire)
        {
            if (Input.GetButtonDown("Fire1") || Input.GetKeyDown("space"))
            {
                InstantiateSpear();
                return;
            }

            if (Input.GetButtonUp("Fire1") || Input.GetKeyUp("space"))
            {
                Throw();
            }

            if (IsTouchedBegun())
            {
                InstantiateSpear();
            }

            if(IsTouchedEnded())
            {
                Throw();
            }
        }
        
    }



    private void UpdatePlayerPosition()
    {
        transform.position = Vector3.Lerp(transform.position, WannaBePosition, Time.deltaTime * 2);
    }

    private bool IsTouchedBegun()
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

    private bool IsTouchedEnded()
    {
        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Ended)
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

    private IEnumerator WaitAlittleBeforeAllowFire()
    {
        yield return new WaitForSeconds(MinTimeBetweenThrowsSeconds);
        AllowFire = true;
    }

    private void InstantiateSpear()
    {
        

        float y = Random.Range(-0.5f, 0.5f);

        spear = Instantiate(spearPrefab, transform.position + new Vector3(0, y, 0), Quaternion.identity);

        audioSource.Play();
    }

    private void Throw()
    {
        AllowFire = false;

        StartCoroutine(WaitAlittleBeforeAllowFire());

        spear.StartMoving();    
    }

    // gets called when the crate is destructed.
    internal void MoveToNextTarget()
    {
        StartCoroutine(StartCountdown());
    }
}
