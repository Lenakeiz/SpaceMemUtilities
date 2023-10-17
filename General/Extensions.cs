using System.Collections;
using System.Linq;
using UnityEngine;

public static class SpaceMemoryExtensions
{
    public static void Wiggle(this Transform transformToWiggle, MonoBehaviour behaviour, Vector3 direction, float duration, float speed = 2.0f, float distance = 2.0f)
    {
        behaviour.StartCoroutine(WiggleRoutine(transformToWiggle, direction, duration, speed, distance));
    }

    private static IEnumerator WiggleRoutine(Transform transformToWiggle, Vector3 direction, float duration, float speed = 2.0f, float distance = 2.0f)
    {
        float time = 0f;

        Vector3 startPos = transformToWiggle.localPosition;
        Vector3 wiggleDirection = direction.normalized;  // Set this to your desired wiggle direction
        float displacemnet = 0f;

        while (time <= duration)
        {
            displacemnet = Mathf.Lerp(Mathf.Sin(time * speed) * distance, 0.0f, time / duration);
            transformToWiggle.localPosition = startPos + wiggleDirection * displacemnet;
            time += Time.deltaTime;
            yield return null;
        }

        transformToWiggle.localPosition = startPos;
    }

    public static void MoveOverParabola(this Transform transformToMove, MonoBehaviour behaviour, Vector3 start, Vector3 end, float height, AnimationCurve curve, float time)
    {
        behaviour.StartCoroutine(MoveOverParabolaRoutine(transformToMove, start, end, height, curve, time));
    }

    private static IEnumerator MoveOverParabolaRoutine(Transform transformToMove, Vector3 start, Vector3 end, float height, AnimationCurve curve, float time)
    {
        Vector3 direction = (end - start).normalized; // Get the normalized direction from start to end
        Vector3 perpDirection;

        // Find a vector that is not parallel to the direction vector
        Vector3 nonParallelVector = direction == Vector3.forward || direction == Vector3.back ? Vector3.right : Vector3.forward;

        // Calculate the cross product of direction and nonParallelVector to get a vector perpendicular to the direction
        perpDirection = Vector3.Cross(direction, nonParallelVector).normalized;

        Vector3 middle = (start + end) / 2; // The point exactly between start and end
        middle += perpDirection * height; // Add the desired height in the direction that is perpendicular to the start-end direction

        float t = 0;

        while (t < time)
        {
            t += Time.deltaTime;

            float p = t / time;

            // Find the points along the lines start-middle and middle-end respectively
            Vector3 m1 = Vector3.Lerp(start, middle, p);
            Vector3 m2 = Vector3.Lerp(middle, end, p);

            // Find the point along the line m1-m2 and move the object to that point
            transformToMove.position = Vector3.Lerp(m1, m2, p);

            yield return null;
        }
    }

    public static void ShakeAndCollapse(this Transform transformToCollapse, MonoBehaviour behaviour, float duration, float shakeIntensity, float collapseSpeed)
    {
        behaviour.StartCoroutine(ShakeAndCollapseRoutine(transformToCollapse, duration, shakeIntensity, collapseSpeed));
    }

    private static IEnumerator ShakeAndCollapseRoutine(Transform transformToCollapse, float duration, float shakeIntensity, float collapseSpeed)
    {
        float elapsedTime = 0f;
        Vector3 originalPosition = transformToCollapse.localPosition;

        while (elapsedTime < duration)
        {
            // Shake along x and z axes
            float offsetX = Random.Range(-shakeIntensity, shakeIntensity);
            float offsetZ = Random.Range(-shakeIntensity, shakeIntensity);

            transformToCollapse.localPosition = originalPosition + new Vector3(offsetX, -collapseSpeed * elapsedTime, offsetZ);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transformToCollapse.localPosition = originalPosition + new Vector3(0, -collapseSpeed * duration, 0);
    }

    public static bool IsPositionedAndFacingTowards(this Transform transform, Vector3 targetPosition, Vector3 targetDirection, float positionAllowance, float directionAllowanceDegrees)
    {
        // Get the object's current position (ignoring y)
        Vector3 position = new Vector3(transform.position.x, 0, transform.position.z);

        // Check if object is close enough to the target position
        if (Vector3.Distance(position, targetPosition) > positionAllowance)
        {
            return false;
        }

        // The object is within the allowed error range of the target position
        // Now we need to check their orientation

        // Get the object's current direction (ignoring y)
        Vector3 direction = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;

        // Check if object is facing close enough to the target direction
        if (Vector3.Dot(direction, targetDirection.normalized) < Mathf.Cos(directionAllowanceDegrees * Mathf.Deg2Rad))
        {
            return false;
        }

        // The object is facing within the allowed error range of the target direction
        return true;
    }
    public static T FindNameComponentInChildren<T>(this GameObject gameObject, string childName) where T : Component
    {
        T component = gameObject.GetComponentsInChildren<T>().FirstOrDefault(c => c.gameObject.name == childName);

        if (component == null)
        {
            Debug.LogError($"No {typeof(T).Name} component found on a GameObject named '{childName}' in this GameObject or its children.");
        }

        return component;
    }

    public static GameObject FindGameObjectInChildrenWithName(this GameObject gameObject, string childName)
    {
        GameObject child = gameObject.GetComponentsInChildren<Transform>().FirstOrDefault(c => c.gameObject.name == childName)?.gameObject;

        if (child == null)
        {
            Debug.LogError($"No GameObject named '{childName}' found in this GameObject or its children.");
        }

        return child;
    }
}
