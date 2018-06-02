using UnityEngine;

public class Shooter : MonoBehaviour {

    [SerializeField]
    private GameObject Projectile;


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Throw();
        }

        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Throw();
            }
        }
    }

    void Throw()
    {
        var clone = Instantiate(Projectile, transform.position, Quaternion.identity);

        var audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }
}
