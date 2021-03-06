using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]
    float explosionForce;

    [SerializeField]
    float explosionRadius;

    [SerializeField]
    float upwardsModifier;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("car"))
        {
            GameObject[] cars = GameObject.FindGameObjectsWithTag("car");
            foreach (GameObject car in cars)
            {
                Rigidbody rb = car.GetComponent<Rigidbody>();
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsModifier, ForceMode.Force);
            }
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("car"))
        {
            //SOLUTION 1: SEARCH FOR ALL CARS
            /*GameObject[] cars = GameObject.FindGameObjectsWithTag("car");
            foreach (GameObject car in cars)
            {
                Rigidbody rb = car.GetComponent<Rigidbody>();
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsModifier, ForceMode.Force);
            }*/

            //SOLUTION 2: SPHERECAST TO FIND NEARBY CARS
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, explosionRadius, transform.right, explosionRadius);
            foreach(RaycastHit hit in hits)
            {
                if(hit.collider.CompareTag("car"))
                {
                    hit.rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsModifier, ForceMode.Force);
                }
            }
        }
    }
}
