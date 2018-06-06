using System;
using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour {

    [SerializeField]
    private GameObject Projectile;

    private Vector3 WannaBePosition;

    private void Start()
    {
        WannaBePosition = transform.position;
    }

    void Update()
    {
 
         transform.position = Vector3.Lerp(transform.position, WannaBePosition, Time.deltaTime * 2);

        

        if (Input.GetButtonDown("Fire1"))
        {
            Throw();
        }

        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Throw();

                // TODO: does this fix the issue that upon touch most likely two spears are thrown?
                break;
            }
        }
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
