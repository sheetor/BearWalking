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

        for (int i = boneTransform.Count - 2; i >= 0; i--)
        {
            RotateBone(boneTransform[boneTransform.Count - 1], boneTransform[i], targetPosition);

            if (Pole != null && i + 2 <= boneTransform.Count - 1)
            {
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
    
    
}
