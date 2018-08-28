using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    public float scrollSpeed;
    public float tileSize;

    private Vector3 startingPosition;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSize);
        transform.position = startingPosition + Vector3.forward * newPosition;
    }
}
