using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKManager : MonoBehaviour
{

    public List<Transform> boneTransform = new List<Transform>();
    public List<GameObject> children= new List<GameObject>();
    public GameObject targetsphere;

    public float Delta = 0.001f;
    public int index;
    int maxIterationCount = 10;
    protected Quaternion TargetInitialRotation;
    protected Quaternion EndInitialRotation;
    private Vector3 targetPosition;

    public float speed = 0.7f;

    // Start is called before the first frame update
    void Awake()
    {
        boneTransform.Add(this.transform);
        GetChildren(transform, boneTransform);

        //Debug.Log(boneTransform.Count/2);
        TargetInitialRotation = targetsphere.transform.rotation;
        EndInitialRotation = transform.rotation;
        for (int i = 0; i <= boneTransform.Count / 2; i++)
        {
            boneTransform.RemoveAt(boneTransform.Count-1);
        }
        //targetPosition = boneTransform[boneTransform.Count - 1].position;
    }    

    // Update is called once per frame
    void LateUpdate()
    {

        targetPosition = targetsphere.transform.position;

        for (int i = boneTransform.Count - 2; i >= 0; i--) //den här skiten som snappar de just nu, kanske kan fucka runt i rotatebone? 
        {
            RotateBone(boneTransform[boneTransform.Count - 1], boneTransform[i], targetPosition);
        }
        /*for (int i = boneTransform.Count - 3; i >= 0; i--) //den här skiten som snappar de just nu, kanske kan fucka runt i rotatebone? 
        {
            RotateBone(boneTransform[boneTransform.Count - 1], boneTransform[i], targetPosition);
        }*/


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

    private void GetChildren(Transform parent, List<Transform> list)
    {
        foreach (Transform child in parent)
        {
            list.Add(child);
            GetChildren(child, list);
        }
    }

    public static void RotateBone(Transform effector, Transform bone, Vector3 goalPosition)
    {
        Vector3 effectorPosition = effector.position;
        Vector3 bonePosition = bone.position;
        Quaternion boneRotation = bone.rotation;

        Vector3 boneToEffector = effectorPosition - bonePosition;
        Vector3 boneToGoal = goalPosition - bonePosition;

        Quaternion fromToRotation = Quaternion.FromToRotation(boneToEffector,boneToGoal);
        Quaternion newRotation = fromToRotation * boneRotation;

        bone.rotation = newRotation;
    }
    
    public Vector3 ForwardKinematics(float[] angles)
    {
        Vector3 prevPoint = boneTransform[0].transform.position;
        Quaternion rotation = Quaternion.identity;
        for (int i = 1; i < boneTransform.Count; i++)
        {
            // Rotates around a new axis
            rotation *= Quaternion.AngleAxis(angles[i - 1], boneTransform[i - 1].GetComponent<RobotJoint>().Axis);
            Vector3 nextPoint = prevPoint + rotation * boneTransform[i].GetComponent<RobotJoint>().StartOffset;

            prevPoint = nextPoint;
        }

        return prevPoint;
    }
    public float DistanceFromTarget(Vector3 target, float[] angles)
    {
        Vector3 point = ForwardKinematics(angles);
        return Vector3.Distance(point, target);
    }
}
