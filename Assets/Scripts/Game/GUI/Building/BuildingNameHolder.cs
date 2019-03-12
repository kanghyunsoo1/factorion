using UnityEngine;
using UnityEngine.UI;

public class BuildingNameHolder :MonoBehaviour {

    public void SetText(string txt) {
        GetComponent<Text>().text = txt;
    }
}
