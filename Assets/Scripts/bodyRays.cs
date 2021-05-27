using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyRays : MonoBehaviour
{
    //RaycastHit hit;
    public List<GameObject> effectors = new List<GameObject>();
    private Vector3 OGoffset = Vector3.zero;
    private Vector3 startPos, avgPos, avgLeft, avgRight;
    public float distance;
    Vector3 diagonl = new Vector3(0.5f,-1, 1.1f);
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i =0; i < effectors.Count; i++)
        {
            
            avgPos += (effectors[i].transform.position);
        }
        avgPos /= effectors.Count;
        distance = Vector3.Distance(avgPos, transform.position);
        OGoffset = avgPos - transform.position;
        OGoffset = OGoffset.normalized;
        avgPos = Vector3.Scale(avgPos, (new Vector3(1, 0, 1)));

    }

    // Update is called once per frame
    void Update()
    {
        
        avgPos = Vector3.zero;
        for (int i = 0; i < effectors.Count; i++)
        {
            avgPos += effectors[i].transform.position;
        }
        avgLeft = effectors[1].transform.position + effectors[2].transform.position;
        avgLeft /= 2;
        avgRight = effectors[0].transform.position + effectors[3].transform.position;
        avgRight /= 2;

        avgPos /= effectors.Count;
        OGoffset = (transform.position - avgPos).normalized;
        OGoffset *= distance;
        transform.position = avgPos + OGoffset;

        transform.Translate(new Vector3(1f, 0, 0) * Time.deltaTime);
        

    }
}
