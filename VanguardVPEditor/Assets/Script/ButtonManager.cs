using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;
using System.IO;

public class ButtonManager : MonoBehaviour
{
    public GameObject buttons;
    public GameObject buttonPrefab;
    public GameObject view;
    public TMP_Text tmp;

    public void CreateTitleButton()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        XmlDocument document = new XmlDocument();
        DatabaseManager databaseManager = new DatabaseManager();
        string path = databaseManager.GetXmlPath(name);
        document.Load(path);

        XmlNode title = document.SelectSingleNode(name);
        XmlNodeList titleList = title.SelectNodes("Deck");

        Button[] titleButton = new Button[titleList.Count];

        Transform transform = view.transform;

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < titleList.Count; i++)
        {
            XmlNode temp = titleList[i];
            titleButton[i] = new Button();
            titleButton[i].buttonName = temp.Attributes["name"].Value;
            titleButton[i].path = temp.Attributes["code"].Value;
            buttons = Instantiate(buttonPrefab);
            buttons.transform.SetParent(view.transform);
            buttons.GetComponent<Button>().buttonName = titleButton[i].buttonName;
            buttons.GetComponent<Button>().path = titleButton[i].path;
            tmp = buttons.transform.GetComponentInChildren<TMP_Text>();
            tmp.text = titleButton[i].buttonName;
        }
    }

    public void CreateCardButton()
    {
        string name = EventSystem.current.currentSelectedGameObject.GetComponent<Button>().path;
        XmlDocument document = new XmlDocument();
        DatabaseManager databaseManager = new DatabaseManager();
        string path = databaseManager.GetXmlPath(name);
        document.Load(path);

        XmlNode card = document.SelectSingleNode(name);
        XmlNodeList cardList = card.SelectNodes("Recorded/Card");

        this.view = GameObject.FindWithTag("CardList");
        CardInformation[] cardInformation = new CardInformation[cardList.Count];

        Transform transform = view.transform;

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < cardList.Count; i++)
        {
            XmlNode temp = cardList[i];
            cardInformation[i] = new CardInformation();
            cardInformation[i].cname = temp.Attributes["name"].Value;
            cardInformation[i].clan = temp.Attributes["clan"].Value;
            cardInformation[i].nation = temp.Attributes["nation"].Value;
            cardInformation[i].code = string.Concat(name, "/", temp.Attributes["path"].Value); ;
            XmlNode textnode = temp.SelectSingleNode("Text");
            cardInformation[i].text = textnode.Attributes["text"].Value;
            buttons = Instantiate(buttonPrefab);
            buttons.transform.SetParent(view.transform);
            buttons.GetComponent<CardInformation>().cname = temp.Attributes["name"].Value;
            buttons.GetComponent<CardInformation>().clan = temp.Attributes["clan"].Value;
            buttons.GetComponent<CardInformation>().nation = temp.Attributes["nation"].Value;
            buttons.GetComponent<CardInformation>().text = textnode.Attributes["text"].Value;
            buttons.GetComponent<CardInformation>().type = temp.Attributes["type"].Value;
            buttons.GetComponent<CardInformation>().code = cardInformation[i].code;
            tmp = buttons.transform.GetComponentInChildren<TMP_Text>();
            tmp.text = temp.Attributes["name"].Value;
        }
    }

    public void LoadCardInfo()
    {
        this.view = GameObject.FindWithTag("Canvas");
        buttons = EventSystem.current.currentSelectedGameObject;
        GameObject cardInfo = Instantiate(buttonPrefab);
        cardInfo.transform.SetParent(view.transform);
        cardInfo.GetComponent<CardInformation>().cname = buttons.GetComponent<CardInformation>().cname;
        cardInfo.GetComponent<CardInformation>().nation = buttons.GetComponent<CardInformation>().nation;
        cardInfo.GetComponent<CardInformation>().clan = buttons.GetComponent<CardInformation>().clan;
        cardInfo.GetComponent<CardInformation>().code = buttons.GetComponent<CardInformation>().code;
        cardInfo.GetComponent<CardInformation>().text = buttons.GetComponent<CardInformation>().text;
        cardInfo.GetComponent<CardInformation>().LoadCard();
        cardInfo.transform.localPosition = Vector3.zero;
    }

    public void Close()
    {
        GameObject closeView = this.transform.parent.gameObject;
        Destroy(closeView);
    }

    public void outCard()
    {
        Destroy(this.gameObject);
    }
}
