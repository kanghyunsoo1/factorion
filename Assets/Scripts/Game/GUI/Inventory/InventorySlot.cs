using UnityEngine;
using UnityEngine.UI;

public class InventorySlot :MonoBehaviour {
    private Image _image;
    private Text _text;
    private ItemBundle _bundle;
    private SpriteManager _spriteManager;

    void Awake() {
        ManagerManager.SetManagers(this);
        _image = transform.Find("Image").GetComponent<Image>();
        _text = transform.Find("Text").GetComponent<Text>();
    }

    public void OnClick() {
        FindObjectOfType<InventoryGuiManager>().OnSlotClick(_bundle);
    }

    public void SetBundle(ItemBundle bundle) {
        _bundle = bundle;
        _image.sprite = _spriteManager.GetSprite("item", bundle.name);
    }

    private void FixedUpdate() {
        _text.text = _bundle.count + "";
    }
}
