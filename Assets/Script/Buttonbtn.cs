using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttonbtn : MonoBehaviour {

    public GameObject optionList;
    

    private bool clicked = false;
    private Image sprite;

    void Awake() {
        sprite = GetComponent<Image>();
    }
    public void Click() {
        
        optionList.SetActive(clicked);
    }

    public bool ReClicked() {
        optionList.SetActive(clicked);
        return clicked;
    }

}
