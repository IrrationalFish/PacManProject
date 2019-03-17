using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsController : MonoBehaviour {

    private Player playerScript;

    private void Start() {
        playerScript=gameObject.GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag =="Item") {
            string itemName = other.gameObject.GetComponent<ItemObject>().itmeName;
            if(itemName =="EnergyPellet") {
                Debug.Log("Meet Energy Pellet!");
                MeetEnergyPellet();
                Destroy(other.gameObject);
            }else if(other.tag=="Item" &&playerScript.ItemsListHasSpace()) {
                Debug.Log("Meet "+itemName+" !");
                playerScript.GetItem(itemName);
                Destroy(other.gameObject);
            }
            /*else if (itemName=="Laser" && playerScript.ItemsListHasSpace()) {
                Debug.Log("Meet Laser!");
                playerScript.GetItem(new Item("Laser"));
                Destroy(other.gameObject);
            }else if (itemName=="Grenade"&&playerScript.ItemsListHasSpace()) {
                Debug.Log("Meet Grenade!");
                playerScript.GetItem(new Item("Grenade"));
                Destroy(other.gameObject);
            }*/
        }
    }

    private void MeetEnergyPellet() {
        playerScript.AddEnergyIgnoreLimit(100);
    }

}
