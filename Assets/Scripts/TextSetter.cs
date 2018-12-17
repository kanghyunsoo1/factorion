using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSetter : MonoBehaviour
{
    public string key;
    void Start()
    {
        GetComponent<Text>().text= FindObjectOfType<TextManager>().GetText(key);
        Destroy(this);
    }
}