using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour {

    public EventSystem eventSystem;
    public GameObject selectedObject;

    private bool buttonSelected;

	// Update is called once per frame
	void Update () {
        if (buttonSelected == false) {
            buttonSelected = true;
            eventSystem.SetSelectedGameObject(selectedObject);
        }
	}

    private void OnDisable() {
        buttonSelected = false;
    }
}
