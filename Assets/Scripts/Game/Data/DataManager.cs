using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class DataManager :MonoBehaviour {
    private GuiManager _guim;
    private KhsManager _km;
    private ValueManager _vm;
    private string _mapName;
    void Awake() {
        _guim = GetComponent<GuiManager>();
        _km = GetComponent<KhsManager>();
        _vm = GetComponent<ValueManager>();
        _mapName = StaticDatas.mapName;
    }

    public void Save() {
        StartCoroutine(_Save());
    }

    private IEnumerator _Save() {
        _guim.OnSaveStart();
        yield return null;
        _km.Save(_mapName);
        _vm.Save(_mapName);
        _guim.OnSaveEnd();
    }

    public void Load() {
        StartCoroutine(_Load());
    }

    private IEnumerator _Load() {
        _guim.OnLoadStart();
        yield return null;
        _km.Load(_mapName);
        _vm.Load(_mapName);
        _guim.OnLoadEnd();
    }

    public void Delete() {
        _km.Delete(_mapName);
        _vm.Delete(_mapName);
    }
}
