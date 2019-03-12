using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot :MonoBehaviour {
    private Image _image;
    private Text _text;
    private ItemBundle _bundle;

    void Awake() {
        _image = transform.Find("Image").GetComponent<Image>();
        _text = transform.Find("Text").GetComponent<Text>();
    }

    public void OnClick() {
        FindObjectOfType<InventoryGuiManager>().OnSlotClick(_bundle);
    }

    public void SetBundle(ItemBundle bundle) {
        _bundle = bundle;
        _image.sprite = ManagerManager.GetManager<SpriteManager>().GetSprite("item", bundle.name);
    }

    private void FixedUpdate() {
        _text.text = _bundle.count + "";
    }
}
