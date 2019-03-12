using UnityEngine;
using UnityEngine.SceneManagement;

public class Logo :MonoBehaviour {
    public string nextScene;
    public float delay;
    void Start() {
        Destroy(gameObject, delay);
    }

    private void OnDestroy() {
        SceneManager.LoadScene(nextScene);
    }
}
