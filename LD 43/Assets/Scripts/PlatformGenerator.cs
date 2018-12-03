using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {

    public GameObject platform;
    public Transform generationPoint;
    public float distBetween;

    private float _platformWidth;

	// Use this for initialization
	void Start () {
        _platformWidth = platform.GetComponent<BoxCollider2D>().size.x;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x < generationPoint.position.x)
        {
            transform.position = new Vector3(transform.position.x + _platformWidth + distBetween, transform.position.y, transform.position.y);
            Instantiate(platform, transform.position, transform.rotation);
        }
	}
}
