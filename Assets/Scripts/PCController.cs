using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCController : MonoBehaviour
{
    public Petuch petuch;
    public float sensitivity = 1;
    private bool isMobilePlatform = true;

    private Vector3 tablePosition;
    // Start is called before the first frame update
    void Start()
    {
        if (petuch == null)
        {
            throw new ArgumentNullException(nameof(petuch));
        }
        tablePosition = Vector3.zero;
        isMobilePlatform = Application.isMobilePlatform;
    }

    // Update is called once per frame
    void Update()
    {
        petuch.TableSpeed = isMobilePlatform ? GetAngleForMobile() : GetAngleForPc();
    }

    private Vector3 GetAngleForPc()
    {
        return new Vector3(
            x: Input.GetAxis("Horizontal") * sensitivity,
            y: 0,
            z: Input.GetAxis("Vertical") * sensitivity);
    }
    
    private Vector3 GetAngleForMobile()
    {
        var acceleration = Input.acceleration;
        return new Vector3(
            acceleration.x * sensitivity,
            0,
            acceleration.y * sensitivity);
    }
}
