using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class DataManager :MonoBehaviour {
    private SpinnerManager _spm;
    private KhsManager _km;
    private ShowerManager _sm;
    private string _mapName;
    void Awake() {
        _spm = GetComponent<SpinnerManager>();
        _km = GetComponent<KhsManager>();
        _sm = GetComponent<ShowerManager>();
        _mapName = StaticDatas.mapName;
    }

    public void Save() {
        StartCoroutine(_Save());
    }

    private IEnumerator _Save() {
        _spm.SpinnerOn("save");
        yield return null;
        _km.Save(_mapName);
        _spm.SpinnerOff();
    }

    public void Load() {
        StartCoroutine(_Load());
    }

    private IEnumerator _Load() {
        _spm.SpinnerOn("load");
        yield return null;
        _km.Load(_mapName);
        _sm.OffAll();
        _spm.SpinnerOff();
    }

    public void Delete() {
        _km.Delete(_mapName);
    }
}
