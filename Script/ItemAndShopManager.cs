using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ItemAndShopManager : MonoBehaviour {

    public int ownedPP;

    public Text ownedPPText;
    public GameObject wallBreakerIcon;
    public GameObject grenadeIcon;
    public GameObject laserIcon;
    public GameObject pelletIcon;
    public GameObject portalIcon;
    public GameObject itemCapacityIcon;
    public GameObject energyCapacityIcon;
    public GameObject pacmanLifeIcon;

    public bool wallBreakerUnlocked;
    public bool grenadeUnlocked;
    public bool laserUnlocked;
    public bool pelletUnlocked;
    public bool portalUnlocked;

    // Use this for initialization
    void Start () {
        ownedPP=0;
        ShowCheckMarkIcon(wallBreakerIcon);
        ShowItemIcon(grenadeIcon);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddPP(int number) {
        ownedPP=ownedPP+number;
        ownedPPText.text="Owned Pac Points (PP): "+ownedPP;
    }

    private void ShowCheckMarkIcon(GameObject icon) {       //1=checkmark 2=icon
        icon.GetComponentsInChildren<Image>()[1].enabled=true;
        icon.GetComponentsInChildren<Image>()[2].enabled=false;
    }

    private void ShowItemIcon(GameObject icon) {
        icon.GetComponentsInChildren<Image>()[1].enabled=false;
        icon.GetComponentsInChildren<Image>()[2].enabled=true;
    }
}
