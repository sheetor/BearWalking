using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyRays : MonoBehaviour
{

    public List<Vector3> hits = new List<Vector3>();
    RaycastHit hit;
    public List<Vector3> directions = new List<Vector3>();
    Vector3 diagonl = new Vector3(0.5f,-1, 1.1f);
    // Start is called before the first frame update
    void Start()
    {
        for(int i =0; i < transform.childCount; i++)
        {
            //Debug.Log(transform.GetChild(i).position - transform.position + " this shit");
            Vector3 temp = transform.position - Vector3.down;
            directions.Add(transform.GetChild(i).position - temp);
        }
    }

    // Update is called once per frame
    void Update()
    {

        hits.Clear();
        transform.Translate(new Vector3(-1f, 0, 0) * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            Debug.Log("sadai");
            
            //this.GetComponent<Rigidbody>().velocity = transform.forward * 10f;
        }
        
    }
}
