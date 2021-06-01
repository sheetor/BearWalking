using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movemet : MonoBehaviour
{

    public GameObject aTarget;
    public GameObject[] oTargets = new GameObject[2];
    bool CR_running = false;
    
    
    public float dist = 2f;
    Vector3 currPos;
    Vector3 rayhitPos;

    private void Awake()
    {
        currPos = aTarget.GetComponent<rayScript>().hit.point;
    }

    // Start is called before the first frame update
    void Start()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (Vector3.down), out hit, Mathf.Infinity))
        {

            //transform.position = hit.point;

        }
        //transform.position = aTarget.GetComponent<fitCollider>().hit.point;
        //currPos = startPos = transform.position;
        //StartCoroutine(LerpPosition(positionToMoveTo, 5));
    }

    private void Update()
    {

        
        rayhitPos = aTarget.GetComponent<rayScript>().hit.point;
        currPos = transform.position;
        float totalDist = Vector3.Distance(currPos, rayhitPos);
        if (totalDist > 1.2f && oTargets[0].GetComponent<movemet>().CR_running == false && oTargets[1].GetComponent<movemet>().CR_running == false) //move the target/leg with interpolation 
        {                                                                                                                //when body has moved a certain distance as well as opposite legs being grounded
            Vector3 midPoint = Vector3.Lerp(currPos, rayhitPos, 0.5f) + new Vector3(0,.5f,0);
            StartCoroutine(LerpPosition(midPoint, 0.1f));

            
        }

    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)//unity linear interpolation
    {
        CR_running = true;
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration) //interpolates/moves to a midpoint with some height 
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        time = 0;
        transform.position = targetPosition;

        while (time < duration)//interpolates/moves to the targetpoint from the midpoint
        {
            transform.position = Vector3.Lerp(targetPosition, rayhitPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = rayhitPos;
        CR_running = false;

    }

}
