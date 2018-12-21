using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapManager :MonoBehaviour {
    public GameObject Dot;
    public GameObject CameraRect;
    public Image Minimap;
    private Dictionary<MinimapEntity, GameObject> dics;
    private Rect backRect;
    private Camera cam;
    private RectTransform cameraRectTransform;
    void Start() {
        dics = new Dictionary<MinimapEntity, GameObject>();
        backRect = Minimap.GetComponent<RectTransform>().rect;
        cam = Camera.main;
        cameraRectTransform = CameraRect.GetComponent<RectTransform>();
        StartCoroutine(Refresh());
        StartCoroutine(RefreshCamera());
    }

    public void Register(MinimapEntity me) {
        var g = Instantiate(Dot);
        g.GetComponent<RectTransform>().sizeDelta = new Vector2(me.Size, me.Size);
        g.GetComponent<Image>().color = me.Color;
        g.transform.SetParent(Minimap.transform);
        dics.Add(me, g);
    }

    public void Unregister(MinimapEntity me) {
        dics.Remove(me);
    }

    IEnumerator Refresh() {
        while (true) {
            foreach (MinimapEntity me in dics.Keys) {
                var dot = dics[me];
                dot.GetComponent<RectTransform>().localPosition = WorldToMinimapPoint(me.transform.position);
                yield return null;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator RefreshCamera() {
        while (true) {
            cameraRectTransform.sizeDelta = WorldToMinimapPoint(CameraScreenToUnit());
            cameraRectTransform.localPosition = WorldToMinimapPoint(cam.transform.position);

            yield return new WaitForSeconds(0.1f);
        }
    }

    public Vector2 WorldToMinimapPoint(Vector2 vec) {
        vec /= (float)StaticDatas.SIZE ;
        vec *= backRect.width / 2;
        return vec;
    }

    public Vector2 CameraScreenToUnit() {
        return new Vector2(cam.orthographicSize * 2 * cam.aspect, cam.orthographicSize * 2);
    }
}