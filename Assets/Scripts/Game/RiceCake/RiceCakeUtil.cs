﻿using System;
using UnityEngine;

public class RiceCakeUtil {
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
    public static T GetNearestObjectExcept<T>(T[] array, Vector2 pos, T willBeExcept) where T : MonoBehaviour => GetNearestObject<T>(array, pos, x => !x.Equals(willBeExcept));
    public static T GetNearestObjectExcept<T>(T[] array, Vector2 pos, Predicate<T> match, T willBeExcept) where T : MonoBehaviour => GetNearestObject<T>(array, pos, (x => !x.Equals(willBeExcept) && match(x)));
}