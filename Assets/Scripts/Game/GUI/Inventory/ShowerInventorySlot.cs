using UnityEngine;
using UnityEngine.UI;

public class ShowerInventorySlot :MonoBehaviour {

    public void SetItem(Sprite sprite, int count) {
        transform.Find("Image").GetComponent<Image>().sprite = sprite;
        transform.Find("Text").GetComponent<Text>().text = count + "";
    }
}
