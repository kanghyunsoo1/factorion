using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class InventoryManager :MonoBehaviour {
    public Inventory GetBaseInventory() {
        return FindObjectOfType<Base>().GetComponent<Inventory>();
    }
}