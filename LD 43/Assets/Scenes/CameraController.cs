using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Player player;

    private Vector3 _lastPlayerPos;

    private float _distToMove;

    void Start()
    {
        player = FindObjectOfType<Player>();
        _lastPlayerPos = player.transform.position;
    }
    void Update()
    {
        _distToMove = player.transform.position.x - _lastPlayerPos.x;
        transform.position = new Vector3(transform.position.x + _distToMove, transform.position.y, transform.position.z);
        _lastPlayerPos = player.transform.position;
    }
}