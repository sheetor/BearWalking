using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotJoint : MonoBehaviour
{

    public Vector3 Axis;
    public Vector3 StartOffset;

    public float MinAngle;
    public float MaxAngle;

    private void Awake()
    {
        StartOffset = transform.localPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartOffset = transform.localPosition;

    }
}
