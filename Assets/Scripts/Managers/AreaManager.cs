using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager :MonoBehaviour {
    private GameObject[,] area = new GameObject[StaticDatas.SIZE * 2, StaticDatas.SIZE * 2];

    void Start() {
        for (int i = 0; i < StaticDatas.SIZE * 2; i++) {
            for (int j = 0; j < StaticDatas.SIZE * 2; j++) {
                area[i, j] = null;
            }
        }
    }

    public void LockArea(GameObject go) {
        var vi = GetAreaVectorFromVector(go.transform.position);
        area[vi.x, vi.y] = go;
    }

    public void UnlockArea(GameObject go) {
        var vi = GetAreaVectorFromVector(go.transform.position);
        area[vi.x, vi.y] = null;
    }

    public GameObject GetUser(Vector2 position) {
        var vi = GetAreaVectorFromVector(position);
        return area[vi.x, vi.y];
    }

    public static Vector2Int GetAreaVectorFromVector(Vector2 vector) {
        return new Vector2Int(Mathf.Clamp((int)vector.x, 0, StaticDatas.SIZE - 1) + StaticDatas.SIZE, Mathf.Clamp((int)vector.y, 0, StaticDatas.SIZE - 1) + StaticDatas.SIZE);
    }
}