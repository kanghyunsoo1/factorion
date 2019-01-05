using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavableObject :MonoBehaviour {
    public Vector3 position;
    public Vector3 rotation;
    public void SaveTransform() {
        position = transform.position;
        rotation = transform.rotation.eulerAngles;
    }
    public void LoadTransform() {
        transform.position = position;
        transform.rotation = Quaternion.Euler(rotation);
    }
    public void BeforeSave() {
        SaveTransform();
    }
    public void AfterLoad() {
        LoadTransform();
    }
    public string ToJson() {
        return JsonUtility.ToJson(this);
    }
}