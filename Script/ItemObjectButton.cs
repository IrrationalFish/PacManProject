using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ItemObjectButton : MonoBehaviour {

    public Sprite laserImage;
    public Sprite wallBreakerImage;
    public Sprite GrenadeImage;
    public Sprite portalImage;
    public Sprite portalBImage;
    public Sprite emptyImage;

    public string itemObjectType;

    private void Start() {
        //setItemObjectType("Grenade");
    }

    public void ItemISUsed() {
        itemObjectType="";
        gameObject.GetComponentsInChildren<Image>()[1].sprite=emptyImage;
    }

    public string GetItemObjectType() {
        return itemObjectType;
    }

    public void SetItemObjectType(string name) {
        itemObjectType=name;
        if (itemObjectType=="Laser") {
            gameObject.GetComponentsInChildren<Image>()[1].sprite=laserImage;
            gameObject.GetComponentsInChildren<RectTransform>()[1].sizeDelta=new Vector2(90f, 90f);
        } else if (itemObjectType=="WallBreaker") {
            gameObject.GetComponentsInChildren<Image>()[1].sprite=wallBreakerImage;
            gameObject.GetComponentsInChildren<RectTransform>()[1].sizeDelta=new Vector2(90f, 90f);
        } else if (itemObjectType=="Grenade") {
            gameObject.GetComponentsInChildren<Image>()[1].sprite=GrenadeImage;
            gameObject.GetComponentsInChildren<RectTransform>()[1].sizeDelta=new Vector2(100f, 100f);
        } else if (itemObjectType=="Portal") {
            gameObject.GetComponentsInChildren<Image>()[1].sprite=portalImage;
            gameObject.GetComponentsInChildren<RectTransform>()[1].sizeDelta=new Vector2(90f, 90f);
        } else if (itemObjectType=="PortalB") {
            gameObject.GetComponentsInChildren<Image>()[1].sprite=portalBImage;
            gameObject.GetComponentsInChildren<RectTransform>()[1].sizeDelta=new Vector2(90f, 90f);
        } else {
            gameObject.GetComponentsInChildren<Image>()[1].sprite=emptyImage;
        }
    }
}
