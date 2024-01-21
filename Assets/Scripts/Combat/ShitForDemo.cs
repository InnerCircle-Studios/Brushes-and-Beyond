using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class ShitForDemo : MonoBehaviour {
    CharacterAttributes attributes;
    public TextMeshProUGUI tmp;
    void Start() {
        attributes = gameObject.GetComponent<AttributeManager2>().GetAttributes();
        
    }

    // Update is called once per frame
    void Update() {
        if(attributes.CurrentHealth > 0){
            tmp.SetText("Current HP: "+attributes.CurrentHealth);
        }
        else{
            tmp.SetText("Dead");
        }

    }
}
