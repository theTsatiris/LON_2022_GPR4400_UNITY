using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    Rigidbody body;

    [SerializeField]
    Rigidbody[] planets;
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 gravity = new Vector3();
        foreach(Rigidbody planet in planets)
        {
            float gravMagnitude = (body.mass * planet.mass) / Mathf.Pow(Vector3.Distance(body.position, planet.position), 2.0f);

            Vector3 gravDirection = Vector3.Normalize(planet.position - body.position);
            gravity += gravDirection * gravMagnitude;
        }

        body.AddForce(gravity);
    }
}
