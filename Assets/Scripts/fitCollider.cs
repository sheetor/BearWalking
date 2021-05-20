using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fitCollider : MonoBehaviour
    {
    public RaycastHit hit;

    public Transform poletarget;
    private Vector3 currPos;
    Vector3 distanc;
    // Start is called before the first frame update
    void Start()
    {
        distanc = transform.position - poletarget.position;
        transform.position = poletarget.position + distanc;
        GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;


    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = poletarget.position + distanc;
        
        if (Physics.Raycast(transform.position, (Vector3.down), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, (Vector3.down) * hit.distance, Color.red);
            //Debug.Log("Did Hit");
            //Debug.Log(hit.point);
            transform.position = hit.point+ new Vector3(0,2f);
            
        }
        else
        {
            Debug.DrawRay(transform.position, (Vector3.down) * 1000, Color.white);
            Debug.Log("Did not Hit");
            //Vector3 targetPosition = Vector3.Lerp(boneTransform[boneTransform.Count - 1].position, targetsphere.transform.position, 1f);
        }
    }
}
