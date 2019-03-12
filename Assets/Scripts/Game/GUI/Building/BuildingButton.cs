using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButton :MonoBehaviour {
    public string buildingName;

    private void Start() {
        transform.Find("Image").GetComponent<Image>().sprite = ManagerManager.GetManager<SpriteManager>().GetSprite("building", buildingName);
    }

    public void OnClick() {
        ManagerManager.GetManager<BuildGuiManager>().OnBuildingButtonClick(buildingName);
        ManagerManager.GetManager<AudioManager>().Play("beep");
    }
}
