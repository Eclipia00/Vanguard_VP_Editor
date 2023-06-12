using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject cardUI;
    public GameObject deckUI;
    public GameObject systemManager;

    public void OnCardSystem()
    {
        cardUI.SetActive(true);
        deckUI.SetActive(false);
    }

    public void OnDeckSystem()
    {
        cardUI.SetActive(false);
        deckUI.SetActive(true);
        systemManager.GetComponent<DeckSystem>().ReadDeckInfo();
    }
}
