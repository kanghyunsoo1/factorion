using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertManager :Manager {
    public AlertBox alertBox;
    private List<AlertBox> _list;
    private TextManager _textManager;
    private AudioManager _audioManager;
    void Awake() {
        _audioManager = ManagerManager.GetManager<AudioManager>();
        _textManager = ManagerManager.GetManager<TextManager>();
        _list = new List<AlertBox>();
    }

    public void AddAlert(string textWithoutAlertHead, Color color) {
        AddAlertRaw(_textManager.GetText("alert", textWithoutAlertHead), color);
    }

    public void AddAlertRaw(string rawText, Color color) {
        _audioManager.Play("alert");
        var a = Instantiate(alertBox).GetComponent<AlertBox>();
        a.transform.SetParent(FindObjectOfType<Canvas>().transform);
        var text = a.transform.Find("Text").GetComponent<Text>();
        text.text = rawText;
        text.color = color;
        _list.Insert(0, a);
        for (int i = 0; i < _list.Count; i++) {
            var aa = _list[i];
            aa.myY = -200 - 50 * i;
        }
    }
}