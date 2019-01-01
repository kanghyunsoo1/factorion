using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TextManager :MonoBehaviour {
    public TextAsset text;
    Dictionary<string, string> dic;

    private void Awake() {
        LoadText(Application.systemLanguage);
    }

    public void LoadText(SystemLanguage e) {
        int index = 1;
        switch (e) {
            case SystemLanguage.Korean:
                index = 2;
                break;
        }

        dic = new Dictionary<string, string>();
        string t = text.text;
        var lines = t.Split('\n');
        foreach (string str in lines) {
            if (str.Trim().Equals(""))
                continue;
            var cols = str.Split('#');
            dic.Add(cols[0].ToLower(), cols[index]);
        }
    }

    public string GetText(string key) {
        try {
            return dic[key.ToLower()];
        } catch (Exception) {
            return "Unknown";
        }
    }

    void Update() {

    }
}
