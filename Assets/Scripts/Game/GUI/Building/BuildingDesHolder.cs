using UnityEngine;
using UnityEngine.UI;

public class BuildingDesHolder :MonoBehaviour {

    public void SetText(string txt) {
        GetComponent<Text>().text = txt;
    }
}
