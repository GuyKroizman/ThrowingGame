using System;
using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [SerializeField]
    private GameObject Projectile;

    private Vector3 WannaBePosition;

    private bool AllowFire = true;
    private float CountDown = 0.5f;

    private void Start()
    {
        WannaBePosition = transform.position;
    }



    void Update()
    {
        CountDown -= Time.deltaTime;
        if (CountDown <= 0)
        {
            AllowFire = true;
            CountDown = 0.6f;
        }

        transform.position = Vector3.Lerp(transform.position, WannaBePosition, Time.deltaTime * 2);

        if (Input.GetButtonDown("Fire1") && AllowFire)
        {
            Throw();
        }

        if (IsTouchedScreen() && AllowFire)
        {
            Throw();
        }
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

    public IEnumerator StartCountdown()
    {
        int currCountdownValue = 1;
        while (currCountdownValue > 0)
        {
            yield return new WaitForSeconds(0.50f);
            currCountdownValue--;
        }

        WannaBePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 70);
    }

    void Throw()
    {
        AllowFire = false;
        float y = UnityEngine.Random.Range(-0.5f, 0.5f);

        Instantiate(Projectile, transform.position + new Vector3(0, y, 0), Quaternion.identity);

        var audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    internal void MoveToNextTarget()
    {
        StartCoroutine(StartCountdown());
    }
}
