using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveGround : MonoBehaviour
{

    // Variables
    private Vector3 startPos;
    private float repeatWidth;
    public float speed = 5.0f;
    public float zDestroy = -30;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);

        if (transform.position.z < zDestroy)
        {
            Destroy(gameObject);
        }
    }
}