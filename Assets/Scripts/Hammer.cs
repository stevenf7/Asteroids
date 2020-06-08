using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hammer : MonoBehaviour
{
    public float delay = 3f;

    private float countdown;
    public float radius = 3f;
    private bool hasExploded = false;
    public float force = 100f;
    public GameObject explosionEffect;
    public string explosionSoundTrack;
    public string launchingSoundTrack;
    public float spin_speed = 50000f;
    private bool first_collid = true;
    void Start()
    {
        countdown = delay;
        FindObjectOfType<AudioManager>().Play("HammerThrow");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * spin_speed * Time.deltaTime);
        countdown -= Time.deltaTime;
        if (countdown <= 0f && hasExploded == false)
        {

            Explode();
            hasExploded = true;
        }

    }
    void OnCollisionEnter(UnityEngine.Collision collision)
    {
       if (first_collid)
        {
            first_collid = false;
            return;
        } 
        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, 0.4f);
        foreach (Collider nearbyObject in collidersToDestroy)
        {
            //add damage

            Interactable dest = nearbyObject.GetComponent<Interactable>();
            if (dest != null)
            {
                dest.Destruction();
                
            }
        }
        FindObjectOfType<AudioManager>().Play("Snap");
    }
    void Explode()
    {
        FindObjectOfType<pointer>().missle_launched--;
        Instantiate(explosionEffect, transform.position, transform.rotation * Quaternion.Euler(Random.Range(-45, 45), Random.Range(-45, 45), Random.Range(-45, 45)));
        //showing explosion effect
        FindObjectOfType<AudioManager>().Play(explosionSoundTrack);

        //finding near by object
        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in collidersToDestroy)
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
