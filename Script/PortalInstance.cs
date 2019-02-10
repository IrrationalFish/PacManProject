using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PortalInstance : MonoBehaviour {

    public GameObject linkedPortal;
    public float coolDown;
    public float currentPower;
    public Slider powerSlider;

    void Start() {
        linkedPortal=GameObject.Find("PortalAInstance(Clone)");
        if (linkedPortal!=null) {
            linkedPortal.GetComponent<PortalInstance>().linkedPortal=this.gameObject;
        }
    }

    // Update is called once per frame
    void Update() {
        if (currentPower<=5) {
            currentPower=currentPower+Time.deltaTime;
            powerSlider.value=currentPower;
        }
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Meet portal!");
        if(linkedPortal!=null && currentPower >=coolDown) {
            other.transform.SetPositionAndRotation(linkedPortal.transform.position, other.transform.rotation);
            currentPower=0;
            linkedPortal.GetComponent<PortalInstance>().currentPower=0;
            powerSlider.value=0;
        }
    }
}
