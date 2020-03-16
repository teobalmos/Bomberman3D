using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CameraControl : MonoBehaviour
{

    private int noOfPlayers;
    private GameObject crate;

    private Vector3 offset;
    private Vector3 finalPosition;
    private GameObject[] players;
    private Camera camera;
    private Vector3 moveVelocity;
    private float zoomSpeed;
    
    private void Awake()
    {
        camera = GetComponentInChildren<Camera>();
    }

    private void FindAveragePosition()
    {
        var averagePos = new Vector3();
        var noOfTargets = 0;


        foreach (var player in players)
        {
            if(!player.gameObject.activeSelf)
                continue;

            averagePos += player.transform.position;
            noOfTargets++;
        }
        
//        Debug.Log("targets: " + noOfTargets);

        if (noOfTargets > 0)
            averagePos /= noOfTargets;

        averagePos.y = transform.position.y;

        finalPosition = averagePos;
    }

    private float FindRequiredSize()
    {
        var desiredLocalPos = transform.InverseTransformPoint(finalPosition);
        var size = 0f;

        foreach (var player in players)
        {
            if(!player.gameObject.activeSelf)
                continue;
            var targetLocalPos = transform.InverseTransformPoint(player.transform.position);
            var posToTarget = targetLocalPos - desiredLocalPos;

            size = Mathf.Max(size, Mathf.Abs(posToTarget.y));
            size = Mathf.Max(size, Mathf.Abs(posToTarget.x / camera.aspect));
            // edge buffer
            size += 0.5f;
            // size not too small
            size = Mathf.Max(size, 6f);
        }

        return size;
    }

    private void Move()
    {
        FindAveragePosition();

//        transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref moveVelocity, 0.2f);
        transform.position = finalPosition;
    }

    private void Zoom()
    {
        var requiredSize = FindRequiredSize();
        camera.orthographicSize = Mathf.SmoothDamp(camera.orthographicSize, requiredSize, ref zoomSpeed, 0.2f);
    }

    private void FixedUpdate()
    {
//        Move();
//        Zoom();
//        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * 5);
    }
}
