using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBackgroundScroll : MonoBehaviour {

    public float scrollSpeed;
    public float tilesizeX;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tilesizeX);
        transform.position = startPosition + Vector3.forward * newPosition;
    }
}
