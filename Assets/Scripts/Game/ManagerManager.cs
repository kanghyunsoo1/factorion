using System;
using UnityEngine;

public class ManagerManager :MonoBehaviour {
    private static Manager[] _managers;

    void Awake() {
        _managers = GetComponents<Manager>();
    }

    public static T GetManager<T>() where T : Manager {
        return (T)Array.Find(_managers, x => x.GetType() == typeof(T));
    }
}