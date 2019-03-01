using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ShowerManager :MonoBehaviour {
    public Text infoShower;
    public GameObject select;
    public GameObject inventoryShower;

    private TextManager _textManager;

    void Awake() {
        _textManager = GetComponent<TextManager>();
    }

    public void OnTouch(GameObject go) {
        if (go == null)
            return;
        OffAll();
        var objName = go.name.Replace("(Clone)", "").ToLower();
        var tName = _textManager.GetText("name", objName);
        var sbInfo = new StringBuilder();

        var inventory = go.GetComponent<Inventory>();
        var resource = go.GetComponent<Resource>();

        if (inventory != null) {
            inventoryShower.SetActive(true);
            inventoryShower.GetComponent<InventoryShower>().SetInventory(inventory, go.name);
            sbInfo.Append(_textManager.GetText("gui", "inventory-exists"));
            sbInfo.Append("\r\n");
        }

        if (resource != null) {
            sbInfo.Append(_textManager.GetText("gui", "resource"));
            sbInfo.Append(": <color=#400000>");
            sbInfo.Append(_textManager.GetText("item", resource.name));
            sbInfo.Append("</color>\r\n");
        }

        infoShower.gameObject.SetActive(true);



        var sb = new StringBuilder();
        sb.Append("<size=50>");
        sb.Append(tName);
        sb.Append("</size>");
        sb.Append("\r\n");
        sb.Append("<size=25>");
        sb.Append(sbInfo.ToString());
        sb.Append("</size>");
        infoShower.text = sb.ToString();
        select.transform.position = go.transform.position;
    }

    public void OffAll() {
        infoShower.gameObject.SetActive(false);
        inventoryShower.GetComponent<InventoryShower>().Clear();
        inventoryShower.SetActive(false);
        select.transform.position = new Vector3(123564, 125354);
        StopAllCoroutines();
    }

}