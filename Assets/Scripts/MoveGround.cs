using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveGround : MonoBehaviour
{

    private Vector3 startPos;
    private float repeatWidth;
    public float speed = 5.0f;
    public float xDestroy = -20;
    
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
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        //repeats road
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
        //Destroys road if it goes below the screen
        if (transform.position.x < xDestroy)
        {
            Destroy(gameObject);
        }
    }
}
