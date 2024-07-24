using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveGround : MonoBehaviour
{

    private Vector3 startPos;
    private float repeatWidth;
    public float speed = 5.0f;
    public float zDestroy = -20;
    
    // Start is called before the first frame update
    void Start()
    {
        //repeats background
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        //move road
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        //repeats road
        if (transform.position.z < startPos.z - repeatWidth)
        {
            transform.position = startPos;
        }
        //Destroys road if it goes below the screen
        if (transform.position.z < zDestroy)
        {
            Destroy(gameObject);
        }
    }
}
