using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem :MonoBehaviour {
    private DataManager _dataManager;
    private SpawnManager _spawnManager;

    void Awake() {
        _dataManager = GetComponent<DataManager>();
        _spawnManager = GetComponent<SpawnManager>();
        if (!StaticDatas.wasMainLoad) {
            SceneManager.LoadScene("Main");
        }
    }

    private void Start() {
        _spawnManager.Spawn();
        _dataManager.Load();
    }

    private void FixedUpdate() {
        var i = FindObjectOfType<Base>().GetComponent<Inventory>();
        switch (Random.Range(0, 100)) {
            case 0: i.AddItem("iron", 1); break;
            case 1: i.AddItem("iron-bar", 1); break;
            case 2: i.AddItem("coal", 1); break;
            case 3: i.AddItem("copper", 1); break;
            case 4: i.AddItem("copper-bar", 1); break;
            case 5: i.AddItem("tin", 1); break;
            case 6: i.AddItem("tin-bar", 1); break;
        }
    }
}