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

    public int wallBreakerPrice;
    public int grenadePrice;
    public int laserPrice;
    public int pelletPrice;
    public int portalPrice;
    public int itemCapacityPrice;
    public int energyPrice;
    public int lifePrice;

    private ItemGenerator itemGenScript;
    private GameSceneManager gmScript;

    void Start () {
        ownedPP=0;
        itemGenScript=gameObject.GetComponent<ItemGenerator>();
        gmScript=gameObject.GetComponent<GameSceneManager>();
	}

    public void SetShopMenuState(int numberOfPP) {
        AddPP(numberOfPP);
        SetItemsPrice();
        SetExtraPrice();
    }

    public void UnlockItem(GameObject icon) {
        Debug.Log("Try to unlock item");
        string itemName = icon.name;
        if (itemName.Equals("WallBreakerIconInShop")) {
            if (ownedPP>=wallBreakerPrice) {
                itemGenScript.wallBreakerUnlocked=true;
                SetIconAfterUnlocking(icon, wallBreakerPrice);
            } else {
                Debug.Log("No enough PP for wall breaker");
            }
        } else if (itemName.Equals("GrenadeIconInShop")) {
            if (ownedPP>=grenadePrice) {
                itemGenScript.grenadeUnlocked=true;
                SetIconAfterUnlocking(icon, grenadePrice);
            } else {
                Debug.Log("No enough PP for grenade");
            }
        } else if (itemName.Equals("LaserIconInShop")) {
            if (ownedPP>=laserPrice) {
                itemGenScript.laserUnlocked=true;
                SetIconAfterUnlocking(icon, laserPrice);
            } else {
                Debug.Log("No enough PP for laser");
            }
        } else if (itemName.Equals("PelletIconInShop")) {
            if (ownedPP>=pelletPrice) {
                itemGenScript.pelletUnlocked=true;
                SetIconAfterUnlocking(icon, pelletPrice);
            } else {
                Debug.Log("No enough PP for pellet");
            }
        } else if (itemName.Equals("PortalIconInShop")) {
            if (ownedPP>=portalPrice) {
                itemGenScript.portalUnlocked=true;
                SetIconAfterUnlocking(icon, portalPrice);
            } else {
                Debug.Log("No enough PP for portal");
            }
        }
    }

    public void GetExtraItemCapacity() {
        if (ownedPP>=itemCapacityPrice) {
            gmScript.GetOneMoreItemCapacity();
            ownedPP=ownedPP-itemCapacityPrice;
            itemCapacityIcon.GetComponentsInChildren<Text>()[1].text=itemCapacityPrice.ToString();
        } else {
            Debug.Log("No enough PP for Capacity");
        }
        if (gmScript.itemsCapacity>=6) {
            ShowUnlockedIcon(itemCapacityIcon);
            itemCapacityIcon.GetComponentsInChildren<Text>()[1].text="Full";
            itemCapacityIcon.GetComponentsInChildren<Text>()[1].color=Color.blue;
        }
    }

    public void GetExtraEnergyCapacity() {
        if (ownedPP>=energyPrice) {
            gmScript.GetExtraEnergyCapacity();
            ownedPP=ownedPP-energyPrice;
            energyCapacityIcon.GetComponentsInChildren<Text>()[1].text=energyPrice.ToString();
        } else {
            Debug.Log("No enough PP for extra energy");
        }
        if (gmScript.pacmanEnergyCapacity>=300) {
            ShowUnlockedIcon(energyCapacityIcon);
            energyCapacityIcon.GetComponentsInChildren<Text>()[1].text="Full";
            energyCapacityIcon.GetComponentsInChildren<Text>()[1].color=Color.blue;
        }
    }

    public void GetExtraLife() {
        if (ownedPP>=lifePrice) {
            gmScript.GetExtraLife();
            ownedPP=ownedPP-lifePrice;
            pacmanLifeIcon.GetComponentsInChildren<Text>()[1].text=lifePrice.ToString();
        } else {
            Debug.Log("No enough PP for extra life");
        }
        if (gmScript.currentPacManLives>=3) {
            ShowUnlockedIcon(pacmanLifeIcon);
            pacmanLifeIcon.GetComponentsInChildren<Text>()[1].text="Full";
            pacmanLifeIcon.GetComponentsInChildren<Text>()[1].color=Color.blue;
        }
    }

    private void AddPP(int number) {
        ownedPP=ownedPP+number;
        ownedPPText.text="Owned Pac Points (PP): "+ownedPP;
    }

    private void SetItemsPrice() {
        Debug.Log("Set items price");
        int level = gmScript.level;
        float multiValue = 0.1f*((level-1)%3);    //0,0.1,0.2,0
        wallBreakerPrice=Mathf.RoundToInt((2+multiValue)*gmScript.totalPacDotsInCurrentStage);
        grenadePrice =Mathf.RoundToInt((1.2f+multiValue)*gmScript.totalPacDotsInCurrentStage);
        laserPrice =Mathf.RoundToInt((1.6f+multiValue)*gmScript.totalPacDotsInCurrentStage);
        pelletPrice =Mathf.RoundToInt((0.8f+multiValue)*gmScript.totalPacDotsInCurrentStage);
        portalPrice =Mathf.RoundToInt((2.4f+multiValue)*gmScript.totalPacDotsInCurrentStage);
        if (!itemGenScript.wallBreakerUnlocked) {       //item is locked
            wallBreakerIcon.GetComponentsInChildren<Text>()[1].text=wallBreakerPrice.ToString();
        }
        if (!itemGenScript.grenadeUnlocked) {       //item is locked
            grenadeIcon.GetComponentsInChildren<Text>()[1].text=grenadePrice.ToString();
        }
        if (!itemGenScript.laserUnlocked) {       //item is locked
            laserIcon.GetComponentsInChildren<Text>()[1].text=laserPrice.ToString();
        }
        if (!itemGenScript.pelletUnlocked) {       //item is locked
            pelletIcon.GetComponentsInChildren<Text>()[1].text=pelletPrice.ToString();
        }
        if (!itemGenScript.portalUnlocked) {       //item is locked
            portalIcon.GetComponentsInChildren<Text>()[1].text=portalPrice.ToString();
        }
    }

    private void SetExtraPrice() {
        int neededPacDotsInLastStage = gmScript.pacDotsNeeded;
        itemCapacityPrice=neededPacDotsInLastStage;
        energyPrice=neededPacDotsInLastStage;
        lifePrice=neededPacDotsInLastStage;
        if (gmScript.itemsCapacity<6) {
            itemCapacityIcon.GetComponentsInChildren<Text>()[1].text=itemCapacityPrice.ToString();
        }

        if (gmScript.pacmanEnergyCapacity<300) {
            energyCapacityIcon.GetComponentsInChildren<Text>()[1].text=energyPrice.ToString();
        }

        if (gmScript.currentPacManLives<3) {
            ShowLockedIcon(pacmanLifeIcon);
            pacmanLifeIcon.GetComponentsInChildren<Text>()[1].text=lifePrice.ToString();
        } else {
            ShowUnlockedIcon(pacmanLifeIcon);
            pacmanLifeIcon.GetComponentsInChildren<Text>()[1].text="Full";
        }
    }

    private void ShowUnlockedIcon(GameObject icon) {       //1=checkmark 2=icon
        icon.GetComponentsInChildren<Image>()[1].enabled=true;
        icon.GetComponentsInChildren<Image>()[2].enabled=false;
        icon.GetComponentsInChildren<Button>()[0].enabled=false;
    }

    private void SetIconAfterUnlocking(GameObject icon, int price) {
        ShowUnlockedIcon(icon);
        AddPP(-price);
        icon.GetComponentsInChildren<Text>()[1].text="Unlocked";
        icon.GetComponentsInChildren<Text>()[1].color=Color.blue;
    }

    private void ShowLockedIcon(GameObject icon) {
        icon.GetComponentsInChildren<Image>()[1].enabled=false;
        icon.GetComponentsInChildren<Image>()[2].enabled=true;
        icon.GetComponentsInChildren<Button>()[0].enabled=true;
    }
}
