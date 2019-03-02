using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapManager :MonoBehaviour {
    public GameObject dot;
    public GameObject cameraRect;
    public Image minimap;

    private Dictionary<MinimapEntity, GameObject> _dic;
    private Rect _backRect;
    private LookAtMe _lam;
    private RectTransform _cameraRectTransform;

    private void Awake() {
        _dic = new Dictionary<MinimapEntity, GameObject>();
        _backRect = minimap.GetComponent<RectTransform>().rect;
        _lam = FindObjectOfType<LookAtMe>();
        _cameraRectTransform = cameraRect.GetComponent<RectTransform>();
    }

    private void Start() {
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
            var keys = new List<MinimapEntity>(_dic.Keys);
            foreach (MinimapEntity entity in keys) {
                try {
                    var dot = _dic[entity];

                    dot.GetComponent<RectTransform>().localPosition = WorldToMinimapPoint(entity.transform.position);
                } catch (Exception) { };
                yield return null;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator RefreshCamera() {
        while (true) {
            _cameraRectTransform.sizeDelta = new Vector2(_lam.height, _lam.height) * 0.7f;
            _cameraRectTransform.localPosition = WorldToMinimapPoint(_lam.transform.position);

            yield return new WaitForSeconds(0.1f);
        }
    }

    public Vector2 WorldToMinimapPoint(Vector3 vec) {
        vec /= (float)StaticDatas.SIZE;
        vec *= _backRect.width / 2;
        vec.y = vec.z;
        vec.z = 0;
        return vec;
    }

}