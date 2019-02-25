using UnityEngine;

public class AlertBox :MonoBehaviour {
    public float myY;
    private RectTransform _rectTransform;
    private void Start() {
        _rectTransform = GetComponent<RectTransform>();
        Destroy(gameObject, 3f);
    }
    void Update() {
        _rectTransform.anchoredPosition = Vector3.Lerp(_rectTransform.anchoredPosition, new Vector3(0f, myY, 0f), 0.5f);
    }
}