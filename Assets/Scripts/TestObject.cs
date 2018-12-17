using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : SavableObject
{
    public int a = 2;
    public string b="234";
    bool c = false;
	void Start () {
        StartCoroutine(FindObjectOfType<GameSystem>().SaveAll());
	}
	// Update is called once per frame
	void Update () {
		
	}

    public override string ToJson()
    {
        return JsonUtility.ToJson(this,true);
    }
}
