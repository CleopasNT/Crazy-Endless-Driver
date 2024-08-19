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
    public bool isDestroyed = false;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.z / 2;
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);

        RepeatWidth();

        if (transform.position.z < zDestroy)
        {
            Destroy(gameObject);
            isDestroyed = true;
        }
    }

    public void RepeatWidth()
    {
        if (transform.position.z < startPos.z - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}