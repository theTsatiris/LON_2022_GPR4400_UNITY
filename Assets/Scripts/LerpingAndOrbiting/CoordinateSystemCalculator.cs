using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateSystemCalculator : MonoBehaviour
{
    [SerializeField]
    Transform RefObject;
    [SerializeField]
    Transform Target;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PrintPosition());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PrintPosition()
    {
        while(true)
        { 
            yield return new WaitForSeconds(1.0f);
            Debug.Log("WORLD SPACE COORDINATES: " + Target.position);
            Vector3 localCoords = Target.position - RefObject.position;
            Debug.Log("LOCAL (OBJECT) SPACE COORDINATES: " + localCoords);
        }
    }
}
