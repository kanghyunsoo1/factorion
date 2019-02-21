using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager :MonoBehaviour {

    public delegate void Callback();

    private Transform _dialogs;
    private Transform _yesOrNo;
    private Text _yesOrNoText;
    private Callback _yesback, _noback;
    private TextManager _tm;

    private void Awake() {
        _dialogs = GameObject.Find("Canvas").transform.Find("Dialogs").transform;
        _yesOrNo = _dialogs.Find("YesOrNo");
        _yesOrNoText = _yesOrNo.Find("Text").GetComponent<Text>();
        _tm = GetComponent<TextManager>();
    }

    private void Start() {
        CloseYesOrNo();
    }

    public void ShowYesOrNo(string msg, Callback yesback, Callback noback) {
        _yesback = yesback;
        _noback = noback;
        _yesOrNo.gameObject.SetActive(true);
        _yesOrNoText.text = _tm.GetText("dialog", msg);
    }

    public void OnYesOrNo(bool yes) {
        CloseYesOrNo();
        try {
            if (yes)
                _yesback();
            else
                _noback();
        } catch (Exception e) {
            Debug.Log(e.StackTrace);
        }
    }

    public void CloseYesOrNo() {
        _yesOrNo.gameObject.SetActive(false);
    }
}