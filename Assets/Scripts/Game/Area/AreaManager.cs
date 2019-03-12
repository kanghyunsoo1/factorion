using System.Collections.Generic;
using UnityEngine;

public class AreaManager :MonoBehaviour {
    private GameObject[,] _area = new GameObject[StaticDatas.SIZE * 2, StaticDatas.SIZE * 2];
    private List<Item> _items;

    void Awake() {
        for (int i = 0; i < StaticDatas.SIZE * 2; i++) {
            for (int j = 0; j < StaticDatas.SIZE * 2; j++) {
                _area[i, j] = null;
            }
        }
        _items = new List<Item>();
    }

    public void RegisterUser(GameObject go) {
        var vi = GetAreaVectorFromVector(go.transform.position);
        _area[vi.x, vi.y] = go;
    }

    public void UnregisterUser(GameObject go) {
        var vi = GetAreaVectorFromVector(go.transform.position);
        if (_area[vi.x, vi.y].Equals(go)) {
            _area[vi.x, vi.y] = null;
        }
    }

    public GameObject GetUser(Vector3 position) {
        var vi = GetAreaVectorFromVector(position);
        return _area[vi.x, vi.y];
    }

    public static Vector2Int GetAreaVectorFromVector(Vector3 vector) {
        return new Vector2Int(Mathf.Clamp(Mathf.RoundToInt(vector.x), -StaticDatas.SIZE, StaticDatas.SIZE - 1) + StaticDatas.SIZE, Mathf.Clamp(Mathf.RoundToInt(vector.z), -StaticDatas.SIZE, StaticDatas.SIZE - 1) + StaticDatas.SIZE);
    }

    public void RegisterItem(Item item) {
        _items.Add(item);
    }

    public void UnregisterItem(Item item) {
        _items.Remove(item);
    }

    public Item[] GetItemsAt(Vector3 ingame) {
        Vector2Int where = new Vector2Int(Mathf.RoundToInt(ingame.x), Mathf.RoundToInt(ingame.z));
        List<Item> result = new List<Item>();
        foreach (Item item in _items) {
            var pos = item.transform.position;
            if (where.Equals(new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.z)))) {
                result.Add(item);
            }
        }
        return result.ToArray();
    }
}