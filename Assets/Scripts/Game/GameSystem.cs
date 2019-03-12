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
        _spawnManager.Spawn();
        _dataManager.Load();
    }
}