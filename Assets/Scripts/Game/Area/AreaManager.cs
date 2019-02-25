using UnityEngine;

public class AreaManager :MonoBehaviour {
    private GameObject[,] _area = new GameObject[StaticDatas.SIZE * 2, StaticDatas.SIZE * 2];

    void Awake() {
        for (int i = 0; i < StaticDatas.SIZE * 2; i++) {
            for (int j = 0; j < StaticDatas.SIZE * 2; j++) {
                _area[i, j] = null;
            }
        }
    }

    public void LockArea(GameObject go) {
        var vi = GetAreaVectorFromVector(go.transform.position);
        _area[vi.x, vi.y] = go;
    }

    public void UnlockArea(GameObject go) {
        var vi = GetAreaVectorFromVector(go.transform.position);
        if (_area[vi.x, vi.y].Equals(go)) {
            _area[vi.x, vi.y] = null;
        }
    }

    public GameObject GetUser(Vector2 position) {
        var vi = GetAreaVectorFromVector(position);
        return _area[vi.x, vi.y];
    }

    public static Vector2Int GetAreaVectorFromVector(Vector2 vector) {
        return new Vector2Int(Mathf.Clamp(Mathf.RoundToInt(vector.x), -StaticDatas.SIZE, StaticDatas.SIZE - 1) + StaticDatas.SIZE, Mathf.Clamp(Mathf.RoundToInt(vector.y), -StaticDatas.SIZE, StaticDatas.SIZE - 1) + StaticDatas.SIZE);
    }
}