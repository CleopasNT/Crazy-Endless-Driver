using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveDown : MonoBehaviour
{

    public float speed = 5.0f;
    Rigidbody objectRb;
    public float zDestroy = 28;

    // Start is called before the first frame update
    void Start()
    {
        objectRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move object downward
        objectRb.AddForce(Vector3.forward * -speed * Time.deltaTime);

        // Destroy object if it goes below the screen
        if (transform.position.z < -zDestroy)
        {
            Destroy(gameObject);
        }
    }
}
