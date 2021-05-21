using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movemet : MonoBehaviour
{

    public GameObject aTarget;
    private Vector3 startPos;
    private Vector3 rayhitPos;
    private Vector3 currPos;
    bool really;
    float timeElapsed;
    float lerpDuration = 3;

    float startValue = 0;
    float endValue = 10;
    float valueToLerp;

    public Vector3 positionToMoveTo;
    public Quaternion targetRotation;
    public float speed = 10.0f;


    // Start is called before the first frame update
    void Start()
    {

        transform.position = aTarget.GetComponent<fitCollider>().hit.point;
        currPos = startPos = transform.position;
        
        positionToMoveTo = aTarget.transform.position;
        targetRotation = aTarget.transform.rotation;
        //StartCoroutine(LerpPosition(positionToMoveTo, 5));
    }

    private void Update()
    {
        float step = speed * Time.deltaTime; // calculate distance to move
        rayhitPos = aTarget.GetComponent<fitCollider>().hit.point;
        currPos = transform.position;
        if (Vector3.Distance(currPos, rayhitPos) > 2)
        {

            //transform.position = Vector3.MoveTowards(transform.position, rayhitPos, step);

            //transform.position = Vector3.Lerp(transform.position, rayhitPos, Time.deltaTime);
            StartCoroutine(LerpPosition(rayhitPos,0.1f));
            
        }


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine(LerpPosition(positionToMoveTo, 5));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(LerpPosition(startPos, 5));
        }

    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }

    IEnumerator LerpFunction(Quaternion endValue, float duration)
    {
        float time = 0;
        Quaternion startValue = transform.rotation;

        while (time < duration)
        {
            transform.rotation = Quaternion.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.rotation = endValue;
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
