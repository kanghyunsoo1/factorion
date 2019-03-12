using System;
using System.IO;
using System.Text;
using UnityEngine;

public class RiceCakeManager :Manager {
    private readonly int offset = 5;
    private readonly string fileName = "data.rcm";
    private readonly string directoryName = "RiceCakePrefabs";

    public GameObject Instantiate(string name) {
        var r = Instantiate(Resources.Load<GameObject>(directoryName + "/" + name));
        r.gameObject.name = name;
        return r;
    }

    public void Save(string name) {
        var path = Application.persistentDataPath + "/" + name;
        Directory.CreateDirectory(path);
        Debug.Log(path);
        var objects = FindObjectsOfType<RiceCakeObject>();
        var sb = new StringBuilder();
        for (int i = 0; i < objects.Length; i++) {
            objects[i].position = objects[i].transform.position;
            objects[i].rotation = objects[i].transform.eulerAngles;
            var components = objects[i].GetComponents<RiceCakeComponent>();
            sb.Append(objects[i].gameObject.name.Replace("(Clone)", ""));
            sb.Append("#");
            sb.Append(components.Length);
            sb.Append("#");
            sb.Append(JsonUtility.ToJson(objects[i]));
            sb.Append('\n');
            for (int j = 0; j < components.Length; j++) {
                var type = components[j].GetType();
                sb.Append(type.ToString());
                sb.Append("#");
                sb.Append(JsonUtility.ToJson(components[j]));
                sb.Append('\n');
            }
        }
        var bytes = Encoding.UTF8.GetBytes(sb.ToString());
        for (int i = 0; i < bytes.Length; i++) {
            var cha = bytes[i] - Byte.MaxValue + offset;
            if (cha >= Byte.MinValue)
                bytes[i] = (byte)cha;
            else
                bytes[i] += (byte)offset;
        }
        File.WriteAllBytes(path + "/" + fileName, bytes);
    }

    public void Load(string name) {
        var path = Application.persistentDataPath + "/" + name;
        if (!IsDataExists(name))
            return;

        var bytes = File.ReadAllBytes(path + "/" + fileName);
        for (int i = 0; i < bytes.Length; i++) {
            var cha = bytes[i] - offset;
            if (cha < 0)
                bytes[i] = (byte)(Byte.MaxValue + cha);
            else
                bytes[i] -= (byte)offset;
        }
        var lines = Encoding.UTF8.GetString(bytes).Split('\n');
        if (lines.Length < 3)
            return;
        foreach (var g in FindObjectsOfType<RiceCakeObject>()) {
            Destroy(g.gameObject);
        }
        for (int i = 0; i < lines.Length; i++) {
            try {
                if (lines[i].Trim().Equals(""))
                    continue;
                var sharps = lines[i].Split('#');
                var go = Instantiate(sharps[0]);
                var riceCakeObject = go.GetComponent<RiceCakeObject>();
                JsonUtility.FromJsonOverwrite(sharps[2], riceCakeObject);
                int componentsCount = int.Parse(sharps[1]);
                if (componentsCount != 0)
                    for (int j = 0; j < componentsCount; j++) {
                        i += 1;
                        sharps = lines[i].Split('#');
                        try {
                            var component = go.GetComponent(Type.GetType(sharps[0]));
                            JsonUtility.FromJsonOverwrite(sharps[1], component);
                        } catch (Exception e) { Debug.Log(e); }
                    }
                go.transform.position = riceCakeObject.position;
                go.transform.rotation = Quaternion.Euler(riceCakeObject.rotation);

            } catch (Exception e) {
                Debug.Log(e);
            }
        }
    }

    public bool IsDataExists(string name) {
        return File.Exists(Application.persistentDataPath + "/" + name + "/" + fileName);
    }

    public void Delete(string name) {
        var path = Application.persistentDataPath + "/" + name;
        File.Delete(path + "/" + fileName);
    }
}