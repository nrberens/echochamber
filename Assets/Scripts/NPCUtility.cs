using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NPCUtility : MonoBehaviour {

    //Make all methods static

    public static Vector3 FindRandomValidLocation(Transform transform, float range)
    {
        Vector3 point;
        bool pointIsValid = false;
        do
        {
            pointIsValid = RandomPoint(transform.position, range, out point);
        } while (!pointIsValid);

        return point;
    }

    public static bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    public static Quaternion GetRandomLookRotation()
    {
        //choose random point to look at
        float degrees = Random.Range(0, 360);
        Quaternion rotation = Quaternion.AngleAxis(degrees, Vector3.up);
        return rotation;
    }

    public static bool HasLOSToTarget(Transform origin, Transform target) {
            Vector3 shotVector = target.position - origin.position;
            Ray ray = new Ray(origin.position, shotVector.normalized);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)) {
                if(hit.transform == target.transform) {
                    return true;
                }
            }
            return false;
    }

    public static bool TargetIsInFOV(Transform origin, Transform target, float fov) {
            Vector3 direction = target.position - origin.position;
            float angle = Vector3.Angle(direction, origin.forward);

            if(angle < fov * 0.5f) { return true; }

            return false;
    }
    
    public static bool IsGrounded(Transform transform, float distToGround) {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
}
