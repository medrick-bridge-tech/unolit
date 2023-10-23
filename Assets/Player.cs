using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private RoomManager _roomManager;
    
    void Update()
    {
        var horizontalAxisValue = Input.GetAxis("Horizontal");
        var verticalAxisValue = Input.GetAxis("Vertical");
        transform.Translate(_moveSpeed * Time.deltaTime * new Vector2(horizontalAxisValue, verticalAxisValue));
        
        var clampedXPos = Mathf.Clamp(transform.position.x, -9.5f, 9.5f);
        var clampedYPos = Mathf.Clamp(transform.position.y, -4.5f, 4.5f);
        transform.position = new Vector2(clampedXPos, clampedYPos);
    }

    // private void OnTriggerEnter2D(Collider2D col)
    // {
    //     if (col.name == "Room1")
    //     {
    //         //_roomManager.AudioService.Initialize(_roomManager.AudioSource1, _roomManager.AudioSource2);
    //         StartCoroutine(_roomManager.AudioService.Play(_roomManager.AudioClips[0]));
    //     }
    //     else if (col.name == "Room2")
    //     {
    //         //_roomManager.AudioService.Initialize(_roomManager.AudioSource1, _roomManager.AudioSource2);
    //         StartCoroutine(_roomManager.AudioService.Play(_roomManager.AudioClips[1]));
    //     }
    //     else if (col.name == "Room3")
    //     {
    //         //_roomManager.AudioService.Initialize(_roomManager.AudioSource1, _roomManager.AudioSource2);
    //         StartCoroutine(_roomManager.AudioService.Play(_roomManager.AudioClips[2]));
    //     }
    //     else if (col.name == "Room4")
    //     {
    //         StartCoroutine(_roomManager.AudioService.Stop());
    //     }
    // }
    //
    // private void OnTriggerExit2D(Collider2D col)
    // {
    //     StartCoroutine(_roomManager.AudioService.Stop());
    // }
}
