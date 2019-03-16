using UnityEngine;
using UnityEngine.UI;

public class BuildingButton :MonoBehaviour {
    public string buildingName;
    private SpriteManager _spriteManager;
    private BuildGuiManager _buildGuiManager;

    private void Awake() {
        ManagerManager.SetManagers(this);
    }

    private void Start() {
        transform.Find("Image").GetComponent<Image>().sprite = _spriteManager.GetSprite("building", buildingName);
    }

    public void OnClick() {
        _buildGuiManager.OnBuildingButtonClick(buildingName);
    }
}
