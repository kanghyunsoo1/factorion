using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour {
    public GameObject saving;
	void Start () {
		
	}

    public void OnSaveStart()
    {
        saving.SetActive(true);
    }
    public void OnSaveEnd()
    {
        saving.SetActive(false);
    }
}
