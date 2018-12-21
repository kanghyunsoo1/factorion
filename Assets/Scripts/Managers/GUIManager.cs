using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{
    public GameObject saving;
    public GameObject loading;
    public GameObject spawning;
    DataManager dm;
    void Start()
    {
        dm = GetComponent<DataManager>();

    }

    public void OnSaveStart()
    {
        saving.SetActive(true);
    }
    public void OnSaveEnd()
    {
        saving.SetActive(false);
    }
    public void OnLoadStart()
    {
        loading.SetActive(true);
    }
    public void OnLoadEnd()
    {
        loading.SetActive(false);
    }
    public void OnSpawnStart()
    {
        spawning.SetActive(true);
    }
    public void OnSpawnEnd()
    {
        spawning.SetActive(false);
    }
    public void OnSave()
    {
        dm.Save();
    }
    public void OnExit()
    {
        SceneManager.LoadScene("Main");
    }
}
