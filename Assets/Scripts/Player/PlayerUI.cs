using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI promptMessage;

    void Start()
    {
        
    }

    public void UpdateText(string promptText)
    {
        promptMessage.text = promptText;
	}
}
