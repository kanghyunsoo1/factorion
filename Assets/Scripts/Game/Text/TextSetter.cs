using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSetter :MonoBehaviour {
    public string Head;
    public string Key;
    public string Tail;
    void Start() {
        GetComponent<Text>().text = Head+FindObjectOfType<TextManager>().GetText(Key)+Tail;
        Destroy(this);
    }
}