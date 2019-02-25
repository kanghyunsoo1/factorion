using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager :MonoBehaviour {
    public delegate void VoidCallback();
    public delegate void ItemRecipeCallback(ItemRecipe recipe);

    private Transform _dialogs;
    private Transform _yesOrNo;
    private Text _yesOrNoText;
    private VoidCallback _yesCallback, _noCallback;
    private ItemRecipeCallback _recipeCallback;
    private TextManager _textManager;

    private void Awake() {
        _dialogs = GameObject.Find("Canvas").transform.Find("Dialogs").transform;
        _yesOrNo = _dialogs.Find("YesOrNo");
        _yesOrNoText = _yesOrNo.Find("Text").GetComponent<Text>();
        _textManager = GetComponent<TextManager>();
    }

    private void Start() {
        CloseYesOrNo();
    }

    public void ShowYesOrNo(string msg, VoidCallback yesback, VoidCallback noback) {
        _yesCallback = yesback;
        _noCallback = noback;
        _yesOrNo.gameObject.SetActive(true);
        _yesOrNoText.text = _textManager.GetText("dialog", msg);
    }

    public void ShowRecipeSelect(ItemRecipeCallback callback, ItemRecipe.Type filter) {
        //TODO 필터 적용해서 리스트 출력
    }
    
    public void OnYesOrNo(bool yes) {
        CloseYesOrNo();
        try {
            if (yes)
                _yesCallback();
            else
                _noCallback();
        } catch (Exception) {
        }
    }

    public void CloseYesOrNo() {
        _yesOrNo.gameObject.SetActive(false);
    }
}