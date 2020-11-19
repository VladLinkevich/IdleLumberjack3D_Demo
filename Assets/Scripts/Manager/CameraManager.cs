using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance = null;

    [SerializeField] private float smoothTime = 0.2f;
    [SerializeField] private Vector3 offset = Vector3.zero;

    private Transform _target = null;
    private Vector3 _velocity = Vector3.zero;

    public Transform Target
    {
        set { _target = value; }
    }

    void Start()
    {
        if (instance == null) { instance = this; }

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_target != null)
        {
            Vector3 taregtPos = new Vector3(transform.position.x - offset.x,
                transform.position.y -  offset.y, _target.position.z - offset.z);

            transform.position = Vector3.SmoothDamp(transform.position,
                taregtPos, ref _velocity, smoothTime);
        }
    }
}
