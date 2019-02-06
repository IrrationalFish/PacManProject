using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject {

    private string itemName;

    public void SetName(string name) {
        itemName=name;
    }

	public string GetItemName() {
        return itemName;
    }
}
