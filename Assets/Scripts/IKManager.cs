using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKManager : MonoBehaviour
{

    public List<Transform> boneTransform = new List<Transform>();
    public GameObject targetsphere;
    public Transform Pole;
    
    private Vector3 targetPosition;

    public float speed = 0.7f;

    // Start is called before the first frame update
    void Awake()
    {
        boneTransform.Add(this.transform);
        GetChildren(transform, boneTransform);

        //Debug.Log(boneTransform.Count/2);
        for (int i = 0; i <= boneTransform.Count / 2; i++) //removes all unneccesary gameobjects, in this case, empty ones containing colliders
        {
            boneTransform.RemoveAt(boneTransform.Count-1);

        }

        //targetPosition = boneTransform[boneTransform.Count - 1].position;
    }    

    // Update is called once per frame
    void LateUpdate()
    {

        targetPosition = targetsphere.transform.position;

        for (int i = boneTransform.Count - 2; i >= 0; i--)//start the loop at one joint away from the endpoint
        {
            RotateBone(boneTransform[boneTransform.Count - 1], boneTransform[i], targetPosition);

            if (Pole != null && i + 2 <= boneTransform.Count - 1) //code from https://github.com/ditzel/SimpleIK/blob/master/FastIK/Assets/FastIK/Scripts/FastIK/FastIKCCD.cs
            {                                                     //makes the joints rotate towards a target (pole)
                var plane = new Plane(boneTransform[i + 2].position - boneTransform[i].position, boneTransform[i].position);
                var projectedPole = plane.ClosestPointOnPlane(Pole.position);
                var projectedBone = plane.ClosestPointOnPlane(boneTransform[i + 1].position);
                if ((projectedBone - boneTransform[i].position).sqrMagnitude > 0.01f)
                {
                    var angle = Vector3.SignedAngle(projectedBone - boneTransform[i].position, projectedPole - boneTransform[i].position, plane.normal);
                    boneTransform[i].rotation = Quaternion.AngleAxis(angle, plane.normal) * boneTransform[i].rotation;
                }
            }

        }



    }

    private void GetChildren(Transform parent, List<Transform> list)//recursive function to get all child gameobject
    {
        foreach (Transform child in parent)
        {
            list.Add(child);
            GetChildren(child, list);
        }
    }

    public static void RotateBone(Transform effector, Transform currBone, Vector3 targetPosition)//IK function, rotates joints into place
    {
        Vector3 boneToEffector = effector.position - currBone.position;
        Vector3 boneToGoal = targetPosition - currBone.position;

        Quaternion fromToRotation = Quaternion.FromToRotation(boneToEffector,boneToGoal);
        Quaternion newRotation = fromToRotation * currBone.rotation;

        currBone.rotation = newRotation;
    }
    
    
}
