using UnityEngine;

public class Shooter : MonoBehaviour {

    [SerializeField]
    private GameObject Projectile;


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var clone = Instantiate(Projectile, transform.position, Quaternion.identity);

            var audioSource = GetComponent<AudioSource>();
            audioSource.Play();
            
        }
    }
}
