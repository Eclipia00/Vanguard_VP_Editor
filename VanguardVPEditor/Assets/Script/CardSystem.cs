using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardSystem : MonoBehaviour
{
    public string cardName;
    public string cardNation;
    public string cardClan;
    public string cardType;
    public string cardPath;
    public GameObject cardInputPrefab;
    public TMP_InputField nameInput;
    public TMP_InputField nationInput;
    public TMP_InputField clanInput;
    public TMP_InputField typeInput;
    public TMP_InputField textInput;

    public void OnCardInput()
    {
        GameObject view = GameObject.FindWithTag("Canvas");
        GameObject cardInput = Instantiate(cardInputPrefab);
        cardInput.transform.SetParent(view.transform);
        cardInput.transform.localPosition = Vector3.zero;
    }

    public void AddCard()
    {
        cardName = nameInput.text;
        cardNation = nationInput.text;
        cardClan = clanInput.text;
        cardType = typeInput.text;
        string cardText = textInput.text;

        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load("Assets/Resource/Proxy.xml");
        XmlNode cardList = xmlDocument.SelectSingleNode("Proxy/Recorded");
        XmlElement card = xmlDocument.CreateElement("Card");
        card.SetAttribute("name", cardName);
        card.SetAttribute("clan", cardClan);
        card.SetAttribute("nation", cardNation);
        card.SetAttribute("type", cardType);
        card.SetAttribute("path", "proxy");
        XmlElement text = xmlDocument.CreateElement("Text");
        text.SetAttribute("text", cardText);
        card.AppendChild(text);
        cardList.AppendChild(card);

        xmlDocument.Save("Assets/Resource/Proxy.xml");

        GameObject cardInput = GameObject.FindWithTag("CardInput");
        Destroy(cardInput);
    }
}
