using UnityEngine;
using UnityEngine.UI;

public class SpinnerManager :Manager {
    public GameObject spinnerObject;

    private Text _text;
    private TextManager _textManager;

    private void Awake() {
        _text = spinnerObject.transform.Find("Text").GetComponent<Text>();
        ManagerManager.SetManagers(this);
    }

    public void SpinnerOn(string msg) {
        spinnerObject.SetActive(true);
        _text.text = _textManager.GetText("spinner", msg);
    }

    public void SpinnerOff() {
        _text.text = "";
        spinnerObject.SetActive(false);
    }
}