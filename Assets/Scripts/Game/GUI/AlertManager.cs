using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertManager :MonoBehaviour {
    public AlertBox alertBox;
    private List<AlertBox> _list;
    private TextManager _tm;
    void Awake() {
        _tm = GetComponent<TextManager>();
        _list = new List<AlertBox>();
    }

    public void AddAlert(string textWithoutAlertHead, Color color) {
        var a = Instantiate(alertBox).GetComponent<AlertBox>();
        a.transform.SetParent(FindObjectOfType<Canvas>().transform);
        var text = a.transform.Find("Text").GetComponent<Text>();
        text.text = _tm.GetText("alert", textWithoutAlertHead);
        text.color = color;
        _list.Insert(0, a);
        for (int i = 0; i < _list.Count; i++) {
            var aa = _list[i];
            aa.myY = -200 - 50 * i;
        }
    }
}
