using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class ShitForDemo : MonoBehaviour {
    AttributeManager amg;
    public TextMeshProUGUI tmp;
    void Start() {
        amg = gameObject.GetComponent<AttributeManager>();
        
    }

    // Update is called once per frame
    void Update() {
        if(amg.IsAlive()){
            tmp.SetText("Current HP: "+amg.CurrentHealth);
        }
        else{
            tmp.SetText("Dead");
        }

    }
}
