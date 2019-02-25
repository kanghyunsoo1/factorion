using UnityEngine;
using UnityEngine.UI;

public class FpsShower :MonoBehaviour {
    private void Update() {
        GetComponent<Text>().text = "" + 1 / Time.deltaTime;
    }
}