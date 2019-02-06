using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int maxItemsNumber = 5;
    public string[] itemsNameList;

    private Item[] itemsList;
    [SerializeField]private int energy = 0;
    [SerializeField]private int ownedItems = 0;

	void Start () {
        itemsList=new Item[maxItemsNumber];
        itemsNameList=new string[maxItemsNumber];
    }
	
	void Update () {

	}

    public void GetItem(string itemName) {
        Item item = ScriptableObject.CreateInstance<Item>();
        item.SetName(itemName);
        for (int i =0; i<maxItemsNumber; i++) {
            if(itemsList[i] ==null) {
                itemsList[i]=item;
                itemsNameList[i]=item.GetItemName();
                ownedItems++;
                break;
            }
        }
    }

    public void AddEnergy(int number) {
        energy=energy+number;
    }

    public bool ItemsListHasSpace() {
        return ownedItems<maxItemsNumber;
    }
}
