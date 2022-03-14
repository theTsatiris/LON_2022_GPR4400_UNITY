using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float rotSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3();
        if (Input.GetKey("w"))
        {
            //transform.position += new Vector3(0.0f, 0.0f, speed * Time.deltaTime);
            //direction += new Vector3(0.0f, 0.0f, speed * Time.deltaTime);

            //USING transform.forward
            //transform.position += transform.forward * speed * Time.deltaTime;
            direction += transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            //transform.position += new Vector3(-speed * Time.deltaTime, 0.0f, 0.0f);
            //direction += new Vector3(-speed * Time.deltaTime, 0.0f, 0.0f);

            //USING transform.forward
            //transform.position -= transform.right * speed * Time.deltaTime;
            direction -= transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            //transform.position += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
            //direction += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);

            //USING transform.forward
            //transform.position += transform.right * speed * Time.deltaTime;
            direction += transform.right * speed * Time.deltaTime;
        }

        //transform.LookAt(transform.position + direction);

        //DO NOT ATTEMPT THIS AT HOME!!
        /*Vector3 normalDir = Vector3.Normalize(transform.position + direction);
        float dotProd = Vector3.Dot(normalDir, transform.forward);
        float angles = (Mathf.Acos(dotProd) * 180.0f)/Mathf.PI;
        transform.Rotate(new Vector3(0.0f, angles * Time.deltaTime, 0.0f));*/

        Vector3 targetDir = Vector3.Normalize(direction);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDir, rotSpeed * Time.deltaTime, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDirection);

        if (Input.GetKey("s"))
        {
            //transform.position += new Vector3(0.0f, 0.0f, -speed * Time.deltaTime);
            //direction += new Vector3(0.0f, 0.0f, -speed * Time.deltaTime);

            //USING transform.forward
            //transform.position -= transform.forward * speed * Time.deltaTime;
            direction -= transform.forward * speed * Time.deltaTime;
        }

        transform.position += direction;
    }
}
