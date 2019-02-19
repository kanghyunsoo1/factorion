using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinnerManager :MonoBehaviour {
    public GameObject spinnerObject;

    private Text _text;
    private TextManager _tm;
    private void Awake() {
        _text = spinnerObject.transform.Find("Text").GetComponent<Text>();
        _tm = GetComponent<TextManager>();
    }
    
    public void SpinnerOn(string msg) {
        spinnerObject.SetActive(true);
        _text.text = _tm.GetText("spinner", msg);
    }

    public void SpinnerOff() {
        _text.text = "";
        spinnerObject.SetActive(false);
    }

}
