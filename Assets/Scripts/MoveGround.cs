using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveGround : MonoBehaviour
{

    private Vector3 startPos;
    private float repeatWidth;
    
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
        //repeats background
        if (transform.position.z < startPos.z - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
