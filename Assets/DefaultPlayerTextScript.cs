using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultPlayerTextScript : MonoBehaviour {

    private InputField playerNameInput;
    private Text playerName;

    public void SetPlayerName() {
        playerNameInput = Resources.FindObjectsOfTypeAll<InputField>()[0];

        if(GameManager.playerName != "") {
            playerNameInput.text = GameManager.playerName;
        }

        playerNameInput.ActivateInputField();
    }
}
