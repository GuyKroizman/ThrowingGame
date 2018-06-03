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
        float y = Random.Range(-0.5f, 0.5f);
        var clone = Instantiate(Projectile, transform.position + new Vector3(0, y, 0), Quaternion.identity);

        var audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }
}
