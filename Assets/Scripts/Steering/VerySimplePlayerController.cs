using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerySimplePlayerController : MonoBehaviour
{
    [SerializeField]
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3();

        if(Input.GetKey("w"))
        {
            direction += transform.forward;
        }
        if (Input.GetKey("a"))
        {
            direction -= transform.right;
        }
        if (Input.GetKey("s"))
        {
            direction -= transform.forward;
        }
        if (Input.GetKey("d"))
        {
            direction += transform.right;
        }

        transform.position += direction * speed * Time.deltaTime;
    }
}
