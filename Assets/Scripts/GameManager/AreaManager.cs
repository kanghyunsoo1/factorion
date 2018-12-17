using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour {
    private bool[,] area = new bool[StaticDatas.WIDTH*2,StaticDatas.HEIGHT*2];
    // Use this for initialization
    void Start () {
        for(int i = 0; i < StaticDatas.WIDTH * 2; i++)
        {
            for(int j = 0; j < StaticDatas.HEIGHT * 2; j++)
            {
                area[i, j] = false;
            }
        }
	}

    public void LockArea(int x, int y)
    {
        area[x + StaticDatas.WIDTH, y + StaticDatas.HEIGHT] = true;
    }
    public void UnlockArea(int x, int y)
    {
        area[x + StaticDatas.WIDTH, y + StaticDatas.HEIGHT] = false;
    }

    public bool isEmpty(int x, int y)
    {
        return area[x + StaticDatas.WIDTH, y + StaticDatas.HEIGHT];
    }
}
