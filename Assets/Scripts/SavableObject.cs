using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavableObject : MonoBehaviour {
    public Vector3 position;
    public Vector3 rotation;
    public void SetBaseVar()
    {
        position = transform.position;
        rotation = transform.rotation.eulerAngles;
    }
    public void RestoreBaseVar()
    {
        transform.position = position;
        transform.rotation = Quaternion.Euler(rotation);
    }
    public virtual string ToJson()
    {
        return "";
    }
}
