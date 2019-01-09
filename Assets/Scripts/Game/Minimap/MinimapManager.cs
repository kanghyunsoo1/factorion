﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapManager :MonoBehaviour {
    public GameObject dot;
    public GameObject cameraRect;
    public Image minimap;
    private Dictionary<MinimapEntity, GameObject> _dic;
    private Rect _backRect;
    private Camera _camera;
    private RectTransform _cameraRectTransform;

    void Start() {
        _dic = new Dictionary<MinimapEntity, GameObject>();
        _backRect = minimap.GetComponent<RectTransform>().rect;
        _camera = Camera.main;
        _cameraRectTransform = cameraRect.GetComponent<RectTransform>();
        StartCoroutine(Refresh());
        StartCoroutine(RefreshCamera());
    }

    public void Register(MinimapEntity me) {
        var g = Instantiate(dot);
        g.GetComponent<RectTransform>().sizeDelta = new Vector2(me.size, me.size);
        g.GetComponent<Image>().color = me.color;
        g.transform.SetParent(minimap.transform);
        _dic.Add(me, g);
    }

    public void Unregister(MinimapEntity me) {
        _dic.Remove(me);
    }

    IEnumerator Refresh() {
        while (true) {
            foreach (MinimapEntity me in _dic.Keys) {
                var dot = _dic[me];
                dot.GetComponent<RectTransform>().localPosition = WorldToMinimapPoint(me.transform.position);
                yield return null;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator RefreshCamera() {
        while (true) {
            _cameraRectTransform.sizeDelta = WorldToMinimapPoint(CameraScreenToUnit());
            _cameraRectTransform.localPosition = WorldToMinimapPoint(_camera.transform.position);

            yield return new WaitForSeconds(0.1f);
        }
    }

    public Vector2 WorldToMinimapPoint(Vector2 vec) {
        vec /= (float)StaticDatas.SIZE;
        vec *= _backRect.width / 2;
        return vec;
    }

    public Vector2 CameraScreenToUnit() {
        return new Vector2(_camera.orthographicSize * 2 * _camera.aspect, _camera.orthographicSize * 2);
    }
}