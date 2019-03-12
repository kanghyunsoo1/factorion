using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GuiManager :MonoBehaviour {
    public Text mapNameText;

    private DataManager _dataManager;
    private DialogManager _dialogManager;
    private AlertManager _alertManager;
    private AudioManager _audioManager;

    void Awake() {
        _dataManager = ManagerManager.GetManager<DataManager>();
        _dialogManager = ManagerManager.GetManager<DialogManager>();
        _alertManager = ManagerManager.GetManager<AlertManager>();
        _audioManager = ManagerManager.GetManager<AudioManager>();
        mapNameText.text = StaticDatas.mapName;
    }

    public void OnButtonClick(string name) {
        _audioManager.Play("click");
        switch (name) {
            case "Save":
                _dataManager.Save();
                _alertManager.AddAlert("save", Color.blue);
                break;
            case "Exit":
                _dialogManager.ShowYesOrNo("exit", Exit, null);
                break;
            case "Delete":
                _dialogManager.ShowYesOrNo("delete1", Delete1, null);
                break;
        }
    }

    public void Exit() {
        SceneManager.LoadScene("Main");
    }

    public void Delete1() {
        _dialogManager.ShowYesOrNo("delete2", Delete2, null);
    }

    public void Delete2() {
        _dataManager.Delete();
        SceneManager.LoadScene("Main");
    }
}