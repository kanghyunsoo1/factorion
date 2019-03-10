using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButton :MonoBehaviour {
    public string buildingName;

    private void Start() {
        transform.Find("Image").GetComponent<Image>().sprite = FindObjectOfType<SpriteManager>().GetSprite("building", buildingName);
    }

    public void OnClick() {
        FindObjectOfType<BuildGuiManager>().OnBuildingButtonClick(buildingName);
        FindObjectOfType<AudioManager>().Play("beep");
    }
}
