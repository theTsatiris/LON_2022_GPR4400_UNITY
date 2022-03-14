using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitController : MonoBehaviour
{
    [SerializeField]
    float speed = 1.0f;

    [SerializeField]
    Transform orbitCenter;

    [SerializeField]
    float orbitRadius = 1.0f;

    [SerializeField]
    Transform tiltingPointA;
    [SerializeField]
    Transform tiltingPointB;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    //THE BAD WAY (tilting points around the final orbit center)
    /*void Update()
    {
        float delta = Mathf.Cos(Time.time * speed) * 0.5f + 0.5f;

        transform.position = new Vector3(Mathf.Sin(Time.time * speed) * orbitRadius, transform.position.y, Mathf.Cos(Time.time * speed) * orbitRadius);

        transform.position += orbitCenter.position;

        float newY = (1 - delta) * tiltingPointA.position.y + delta * tiltingPointB.position.y;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }*/

    //THE RIGHT WAY (tilting points around 0.0.0)
    void Update()
    {

        float delta = Mathf.Cos(Time.time * speed) * 0.5f + 0.5f;
        float newY = (1 - delta) * tiltingPointA.position.y + delta * tiltingPointB.position.y;
        transform.position = new Vector3(Mathf.Sin(Time.time* speed)* orbitRadius, newY, Mathf.Cos(Time.time* speed)* orbitRadius);

        transform.position += orbitCenter.position;
    }
}
