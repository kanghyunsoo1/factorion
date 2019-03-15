using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem :MonoBehaviour {
    private DataManager _dataManager;
    private SpawnManager _spawnManager;

    void Awake() {
        if (!StaticDatas.wasMainLoad) {
            SceneManager.LoadScene("Main");
        }
        _dataManager = ManagerManager.GetManager<DataManager>();
        _spawnManager = ManagerManager.GetManager<SpawnManager>();

    }

    private void Start() {
        if (_dataManager.IsDataExists())
            _dataManager.Load();
        else {
            _spawnManager.Spawn();
            FindObjectOfType<Warehouse>().GetComponent<Inventory>().AddItem("iron-bar", 500);
            FindObjectOfType<Warehouse>().GetComponent<Inventory>().AddItem("copper-bar", 500);
            FindObjectOfType<Warehouse>().GetComponent<Inventory>().AddItem("brick", 500);
            FindObjectOfType<Warehouse>().GetComponent<Inventory>().AddItem("rice", 500);
            FindObjectOfType<Warehouse>().GetComponent<Inventory>().AddItem("raw-coal", 500);
        }
    }
}