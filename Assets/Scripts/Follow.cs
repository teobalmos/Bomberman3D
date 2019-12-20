using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField]
    Transform target;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * 5);
    }
}
