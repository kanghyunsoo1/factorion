using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GuiManager :MonoBehaviour {
    public Text mapNameText;

    private DataManager _dm;
    private DialogManager _dim;
    private AlertManager _am;
    private AudioManager _aum;

    void Awake() {
        _dm = GetComponent<DataManager>();
        _dim = GetComponent<DialogManager>();
        _am = GetComponent<AlertManager>();
        _aum = GetComponent<AudioManager>();
        mapNameText.text = StaticDatas.mapName;
    }

    public void OnButtonClick(string name) {
        _aum.Play("click");
        switch (name) {
            case "Save":
                _dm.Save();
                _am.AddAlert("save", Color.blue);
                break;
            case "Exit":
                _dim.ShowYesOrNo("exit", Exit, null);
                break;
            case "Delete":
                _dim.ShowYesOrNo("delete1", Delete1, null);
                break;
        }
    }

    public void Exit() {
        SceneManager.LoadScene("Main");
    }

    public void Delete1() {
        _dim.ShowYesOrNo("delete2", Delete2, null);
    }

    public void Delete2() {
        _dm.Delete();
        SceneManager.LoadScene("Main");
    }

}
