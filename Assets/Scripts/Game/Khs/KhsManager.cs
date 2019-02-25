using System;
using System.IO;
using System.Text;
using UnityEngine;

public class KhsManager :MonoBehaviour {


    public GameObject Instantiate(string name) {
        var r = Instantiate(Resources.Load<GameObject>("KhsObjects/" + name));
        r.gameObject.name = name;
        return r;
    }

    public void Save(string name) {
        var path = Application.persistentDataPath + "/" + name;
        Directory.CreateDirectory(path);
        Debug.Log(path);
        GetComponent<AlertManager>().AddAlertRaw(path, Color.black);
        var objects = FindObjectsOfType<KhsObject>();
        var sb = new StringBuilder();
        for (int i = 0; i < objects.Length; i++) {
            objects[i].position = objects[i].transform.position;
            objects[i].rotation = objects[i].transform.eulerAngles;
            var components = objects[i].GetComponents<KhsComponent>();
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
        File.WriteAllBytes(path + "/data.khs", Encoding.UTF8.GetBytes( sb.ToString()));
    }

    public void Load(string name) {
        var path = Application.persistentDataPath + "/" + name;
        if (!IsDataExists(name))
            return;

        var bytes = File.ReadAllBytes(path + "/data.khs");
        var lines = Encoding.UTF8.GetString(bytes).Split('\n');
        if (lines.Length < 3)
            return;
        foreach (var g in FindObjectsOfType<KhsObject>()) {
            Destroy(g.gameObject);
        }
        for (int i = 0; i < lines.Length; i++) {
            try {
                if (lines[i].Trim().Equals(""))
                    continue;
                var sharps = lines[i].Split('#');
                var go = Instantiate(sharps[0]);
                var khsObject = go.GetComponent<KhsObject>();
                JsonUtility.FromJsonOverwrite(sharps[2], khsObject);
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
                go.transform.position = khsObject.position;
                go.transform.rotation = Quaternion.Euler(khsObject.rotation);

            } catch (Exception e) {
                Debug.Log(e);
                GetComponent<AlertManager>().AddAlertRaw(e.ToString(), Color.black);
                GetComponent<AlertManager>().AddAlertRaw(e.Message, Color.black);
                GetComponent<AlertManager>().AddAlertRaw(e.StackTrace, Color.black);
                return;
            }
        }
    }

    public bool IsDataExists(string name) {
        return File.Exists(Application.persistentDataPath + "/" + name + "/data.khs");
    }

    public void Delete(string name) {
        var path = Application.persistentDataPath + "/" + name;
        File.Delete(path + "/data.khs");
    }
}
