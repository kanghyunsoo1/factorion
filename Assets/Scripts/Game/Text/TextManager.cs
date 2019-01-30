using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TextManager :MonoBehaviour {
    private Dictionary<string, string> _dic;

    private void Awake() {
        LoadText(Application.systemLanguage);
    }

    public void LoadText(SystemLanguage e) {
        string tail = "_en";
        switch (e) {
            case SystemLanguage.Korean:
                tail = "_kr";
                break;
        }

        _dic = new Dictionary<string, string>();
        string t = Resources.Load<TextAsset>("text" + tail).text;
        t = t.Replace("\t", "");
        var lines = t.Split('\n');
        var head = "";
        foreach (string line in lines) {
            if (line.Trim().Equals(""))
                continue;
            if (line.Contains("@")) {
                head = line.Replace("@", "").Trim();

                if (!head.Equals(""))
                    head += "_";
            } else {
                var cols = line.Split('#');
                _dic.Add(head + cols[0].ToLower(), cols[1]);
            }
        }
    }

    public string GetText(string key) {
        try {
            return _dic[key.ToLower()];
        } catch (Exception) {
            return "Unknown";
        }
    }
    public string GetText(string head, string key) {
        try {
            return _dic[head.ToLower() + "_" + key.ToLower()];
        } catch (Exception) {
            return "Unknown";
        }
    }

    void Update() {

    }
}
