using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Grenade : MonoBehaviour
{
    public float delay = 3f;

    private float countdown;
    public float radius = 3f;
    private bool hasExploded = false;
    public float force = 100f;
    public GameObject explosionEffect;
    public string explosionSoundTrack;
    public string launchingSoundTrack;
    void Start()
    {
        countdown = delay;
        FindObjectOfType<AudioManager>().Play(launchingSoundTrack);
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && hasExploded == false)
        {

            Explode();
            hasExploded = true;
        }

    }
    void OnCollisionEnter(UnityEngine.Collision collision)
    {

        Explode();
        hasExploded = true;

     }
    void Explode()
    {
        FindObjectOfType<pointer>().missle_launched--;
        Instantiate(explosionEffect, transform.position, transform.rotation * Quaternion.Euler(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5)));
        //showing explosion effect
        FindObjectOfType<AudioManager>().Play(explosionSoundTrack);

        //finding near by object
        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider nearbyObject in collidersToDestroy)
        {
            //add damage

            Interactable dest = nearbyObject.GetComponent<Interactable>();
            if (dest != null)
            {
                dest.Destruction();
            }
        }

        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in collidersToMove)
        {
            //add force

            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }





        //destroy grenade
        Destroy(gameObject);
        
        hasExploded = true;
    }

}
