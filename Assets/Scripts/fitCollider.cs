using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fitCollider : MonoBehaviour
    {

    public Transform poletarget;
    Vector3 distanc;
    // Start is called before the first frame update
    void Start()
    {
        distanc = transform.position - poletarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = poletarget.position + distanc;
    }
}
