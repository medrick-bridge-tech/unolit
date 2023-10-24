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
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _roomManager.AudioService.Pause();
        }
        
        if (Input.GetKeyDown(KeyCode.U))
        {
            _roomManager.AudioService.UnPause();
            
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Room1")
        {
            _roomManager.AudioService.Initialize(_roomManager.AudioSourceFader1, _roomManager.AudioSourceFader2);
            _roomManager.AudioService.Play(_roomManager.AudioClips[0]);
        }
        else if (col.name == "Room2")
        {
            _roomManager.AudioService.Initialize(_roomManager.AudioSourceFader1, _roomManager.AudioSourceFader2);
            _roomManager.AudioService.Play(_roomManager.AudioClips[1]);
        }
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        _roomManager.AudioService.Stop();
    }
}
