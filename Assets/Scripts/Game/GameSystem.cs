using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem :MonoBehaviour {
    private DataManager _dataManager;
    private SpawnManager _spawnManager;

    void Awake() {
        _dataManager = ManagerManager.GetManager<DataManager>();
        _spawnManager = ManagerManager.GetManager<SpawnManager>();
        if (!StaticDatas.wasMainLoad) {
            SceneManager.LoadScene("Main");
        }
    }

    private void Start() {
        _spawnManager.Spawn();
        _dataManager.Load();
    }
}