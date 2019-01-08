using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSetter :MonoBehaviour {
    public string head;
    public string key;
    public string tail;
    void Start() {
        GetComponent<Text>().text = head + FindObjectOfType<TextManager>().GetText(key) + tail;
        Destroy(this);
    }
}