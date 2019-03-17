using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ShowerManager :Manager {
    public Text infoShower;
    public Text gustnShower;
    public GameObject select;
    public GameObject inventoryShower;

    private TextManager _textManager;
    private AudioManager _audioManager;
    private ValueManager _valueManager;

    private static readonly string COLOR_1 = "#40ffff";
    private static readonly string COLOR_2 = "#F9FF00";
    private static readonly int SIZE_BIG = 60;
    private static readonly int SIZE_NORMAL = 30;

    private bool _isBuildingInfo = false;

    void Awake() {
        ManagerManager.SetManagers(this);
    }

    public void OnTouch(GameObject go) {
        if (go == null || _isBuildingInfo)
            return;
        OffAll(false);
        _audioManager.Play("beep");
        var objName = go.name.Replace("(Clone)", "").ToLower();
        var tName = _textManager.GetText("name", objName);
        var sbInfo = new StringBuilder();

        var inventory = go.GetComponent<Inventory>();
        var resource = go.GetComponent<Resource>();

        if (inventory != null) {
            inventoryShower.SetActive(true);
            inventoryShower.GetComponent<InventoryShower>().SetInventory(inventory, go.name);
            sbInfo.Append("<color=");
            sbInfo.Append(COLOR_1);
            sbInfo.Append(">");
            sbInfo.Append(_textManager.GetText("gui", "inventory-exists"));
            sbInfo.Append("</color>\r\n");
        }

        if (resource != null) {
            sbInfo.Append(_textManager.GetText("gui", "resource"));
            sbInfo.Append(": <color=");
            sbInfo.Append(COLOR_1);
            sbInfo.Append(">");
            sbInfo.Append(_textManager.GetText("item", resource.name));
            sbInfo.Append("</color>\r\n");
        }

        var sb = new StringBuilder();
        sb.Append("<size=");
        sb.Append(SIZE_BIG);
        sb.Append(">");
        sb.Append(tName);
        sb.Append("</size>");
        sb.Append("\r\n\r\n");
        sb.Append("<size=");
        sb.Append(SIZE_NORMAL);
        sb.Append(">");
        sb.Append(sbInfo.ToString());
        sb.Append("</size>");
        infoShower.text = sb.ToString();
        select.transform.position = go.transform.position;
    }

    public void OnTouch(BuildingInfo bi) {
        OffAll(true);
        _isBuildingInfo = true;
        _audioManager.Play("beep");
        var name = _textManager.GetText("name", bi.name);
        var des = _textManager.GetText("des", bi.name);
        var price = bi.price;
        var power = bi.power;
        var sb = new StringBuilder();
        sb.Append("<size=");
        sb.Append(SIZE_BIG);
        sb.Append(">");
        sb.Append(name);
        sb.Append("</size>");
        sb.Append("\r\n\r\n");
        sb.Append("<size=");
        sb.Append(SIZE_NORMAL);
        sb.Append(">");
        sb.Append(des);
        sb.Append("\r\n");
        sb.Append(_textManager.GetText("gui", "price"));
        sb.Append(": <color=");
        sb.Append(COLOR_2);
        sb.Append(">");
        sb.Append(price);
        sb.Append("</color>");
        sb.Append("\r\n");
        sb.Append(_textManager.GetText("gui", "power"));
        sb.Append(": <color=");
        sb.Append(COLOR_1);
        sb.Append(">");
        sb.Append(power);
        sb.Append("</color>");
        sb.Append("</size>");
        infoShower.text = sb.ToString();
    }

    public void OffAll(bool ignoreBuildingInfo) {
        if (!ignoreBuildingInfo && _isBuildingInfo)
            return;
        _isBuildingInfo = false;
        infoShower.text = "";
        inventoryShower.GetComponent<InventoryShower>().Clear();
        inventoryShower.SetActive(false);
        select.transform.position = new Vector3(123564, 125354);
    }

    private void FixedUpdate() {
        gustnShower.text = (int)_valueManager.GetValue("gustn").Value + " G";
    }
}