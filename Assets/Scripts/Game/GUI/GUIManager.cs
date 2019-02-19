using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GuiManager :MonoBehaviour {
    public Text mapNameText;

    private DataManager _dm;
    void Awake() {
        _dm = GetComponent<DataManager>();
        mapNameText.text = StaticDatas.mapName;
    }

    public void OnButtonClick(string name) {
        switch (name) {
            case "Save":
                _dm.Save();
                break;
            case "Exit":
                SceneManager.LoadScene("Main");
                break;
            case "Delete":
                _dm.Delete();
                SceneManager.LoadScene("Main");
                break;
        }
    }

}
