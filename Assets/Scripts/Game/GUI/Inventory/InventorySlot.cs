using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot :MonoBehaviour {
    private Image _image;
    private Text _text;
    private ItemStack _stack;

    void Awake() {
        _image = transform.Find("Image").GetComponent<Image>();
        _text = transform.Find("Text").GetComponent<Text>();
    }

    public void OnClick() {
        FindObjectOfType<InventoryGuiManager>().OnSlotClick(_stack);
    }

    public void SetStack(ItemStack stack) {
        _stack = stack;
        _image.sprite = FindObjectOfType<SpriteManager>().GetSprite("item", stack.name);
    }

    private void FixedUpdate() {
        _text.text = _stack.count + "";
    }
}
