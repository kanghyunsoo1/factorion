using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : SavableObject
{
    public int a = 2;
    public string b="234";
    public bool c = false;
	void Start () {

	}

	void Update () {
        transform.Translate(Random.Range(0.1f, 0.1f), Random.Range(0.1f, 0.1f), 0);
        a += Random.Range(-2, 2);
	}

    public override string ToJson()
    {
        SetBaseVar();
        return JsonUtility.ToJson(this,true);
    }
}
