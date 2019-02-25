using System.Collections;
using UnityEngine;
public class DataManager :MonoBehaviour {
    private SpinnerManager _spinnerManager;
    private KhsManager _khsManager;
    private ShowerManager _showerManager;
    private string _mapName;

    void Awake() {
        _spinnerManager = GetComponent<SpinnerManager>();
        _khsManager = GetComponent<KhsManager>();
        _showerManager = GetComponent<ShowerManager>();
        _mapName = StaticDatas.mapName;
    }

    public void Save() {
        StartCoroutine(_Save());
    }

    private IEnumerator _Save() {
        _spinnerManager.SpinnerOn("save");
        yield return null;
        _khsManager.Save(_mapName);
        _spinnerManager.SpinnerOff();
    }

    public void Load() {
        StartCoroutine(_Load());
    }

    private IEnumerator _Load() {
        _spinnerManager.SpinnerOn("load");
        yield return null;
        _khsManager.Load(_mapName);
        _showerManager.OffAll();
        _spinnerManager.SpinnerOff();
    }

    public void Delete() {
        _khsManager.Delete(_mapName);
    }
}
