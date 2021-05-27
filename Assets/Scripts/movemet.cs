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
        currPos = aTarget.GetComponent<fitCollider>().hit.point;
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

        
        rayhitPos = aTarget.GetComponent<fitCollider>().hit.point;
        currPos = transform.position;
        float totalDist = Vector3.Distance(currPos, rayhitPos);
        if (totalDist > 1.2f && oTargets[0].GetComponent<movemet>().CR_running == false && oTargets[1].GetComponent<movemet>().CR_running == false)
        {
            Vector3 midPoint = Vector3.Lerp(currPos, rayhitPos, 0.5f) + new Vector3(0,.5f,0);
            StartCoroutine(LerpPosition(midPoint, 0.1f));

            
        }

    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        CR_running = true;
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        time = 0;
        transform.position = targetPosition;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(targetPosition, rayhitPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = rayhitPos;
        CR_running = false;

    }


    public static Quaternion RotateBone(Transform effector, Transform bone, Vector3 goalPosition)
    {
        Vector3 effectorPosition = effector.position;
        Vector3 bonePosition = bone.position;
        Quaternion boneRotation = bone.rotation;

        Vector3 boneToEffector = effectorPosition - bonePosition;
        Vector3 boneToGoal = goalPosition - bonePosition;

        Quaternion fromToRotation = Quaternion.FromToRotation(boneToEffector, boneToGoal);
        Quaternion newRotation = fromToRotation * boneRotation;

        return newRotation;
    }
}
