using System;
using UnityEngine;
using System.Reflection;

public class ManagerManager :MonoBehaviour {
    private static Manager[] _managers;

    void Awake() {
        _managers = GetComponents<Manager>();
    }

    public static void SetManagers(MonoBehaviour o) {
        Type t = o.GetType();
        Type managerType = typeof(Manager);
        var fieldInfos = t.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (var i in fieldInfos) {
            Type fieldType = i.FieldType;
            if (fieldType.IsSubclassOf(managerType)) {
                i.SetValue(o,
                    Array.Find(_managers, x => x.GetType() == fieldType)
                    );
            }
        }
    }

    public static T GetManager<T>() where T : Manager => (T)Array.Find(_managers, x => x.GetType() == typeof(T));
}