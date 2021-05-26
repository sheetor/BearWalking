using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyRays : MonoBehaviour
{
    RaycastHit hit;
    public List<GameObject> effectors = new List<GameObject>();
    private Vector3 OGoffset = Vector3.zero;
    private Vector3 startPos;
    private Vector3 avgPos;
    Vector3 diagonl = new Vector3(0.5f,-1, 1.1f);
    
    // Start is called before the first frame update
    void Start()
    {

        
        for (int i =0; i < effectors.Count; i++)
        {
            
            avgPos += transform.TransformPoint(effectors[i].transform.localPosition);
        }
        avgPos /= effectors.Count;
        //OGoffset += new Vector3(0, avgPos.y);
        //OGoffset = Vector3.Scale((transform.position), (new Vector3(0, 1, 0)));
        OGoffset = avgPos - transform.position;
        Debug.Log(OGoffset.ToString("F4"));
        OGoffset = OGoffset.normalized;
        Debug.Log(OGoffset.ToString("F4"));



        avgPos = Vector3.Scale(avgPos, (new Vector3(1, 0, 1)));

        //transform.position = avgPos + OGoffset;

    }

    // Update is called once per frame
    void Update()
    {
        
        avgPos = Vector3.zero;
        for (int i = 0; i < effectors.Count; i++)
        {
            //Debug.Log(transform.TransformPoint(effectors[i].transform.localPosition).ToString("F4"));
            avgPos += transform.TransformPoint(effectors[i].transform.localPosition);
        }
        avgPos /= effectors.Count;

        OGoffset = avgPos - transform.position;
        Debug.Log(OGoffset.ToString("F4"));
        OGoffset = OGoffset.normalized;
        Debug.Log(OGoffset.ToString("F4"));

        //Debug.Log(avgPos.ToString("F4"));

        Vector3 offset = OGoffset + new Vector3(0, avgPos.y);
        //Debug.Log(offset);
        //avgPos = Vector3.Scale(avgPos, (new Vector3(1, 0, 1)));
        //Debug.Log((avgPos + offset));
        OGoffset *= 1.3f;
        transform.position = avgPos ;
        //transform.position = transform.position + offset;
        transform.Translate(new Vector3(1f, 0, 0) * Time.deltaTime);
        

    }
}
