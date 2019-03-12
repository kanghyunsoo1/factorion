using UnityEngine;
using UnityEngine.UI;

public class TextSetter :MonoBehaviour {
    public string key;
    void Start() {
        GetComponent<Text>().text = ManagerManager.GetManager<TextManager>().GetText("gui", key);
        Destroy(this);
    }
}