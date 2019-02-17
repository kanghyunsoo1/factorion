using System;
using UnityEngine;

public class KhsUtil {

    public static T GetNearestObject<T>(T[] array, Vector2 pos, Predicate<T> match) where T : MonoBehaviour {
        float minDistance = 100f;
        T result = null;
        foreach (var obj in array) {
            if (!match(obj))
                continue;
            var distance = Vector2.Distance(pos, obj.transform.position);
            if (distance < minDistance) {
                minDistance = distance;
                result = obj;
            }
        }
        return result;
    }

    public static T GetNearestObject<T>(T[] array, Vector2 pos) where T : MonoBehaviour => GetNearestObject<T>(array, pos, x => true);
}
