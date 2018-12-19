using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ResourceInfo {
    public int resId;
    public string resName;
    public int minAmount;
    public int maxAmount;
    public int rangeFactor;
    public float chance;
    public float minRange;
}
