using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.Net.Mime.MediaTypeNames;

public class DeckSystem : MonoBehaviour
{
    public GameObject view;
    public GameObject deckButtonPrefab;
    public GameObject deckButton;
    public GameObject deckMaker;
    public GameObject cardSearch;
    public RawImage image;
    public TMP_InputField nameInput;

    public void ReadDeckInfo()
    {
        XmlDocument document = new XmlDocument();
        document.Load("Assets/Resource/DeckList.xml");

        XmlNode deck = document.SelectSingleNode("DeckList");
        XmlNodeList deckList = deck.SelectNodes("Deck");

        Button[] titleButton = new Button[deckList.Count];

        Transform transform = view.transform;

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < deckList.Count; i++)
        {
            XmlNode temp = deckList[i];
            titleButton[i] = new Button();
            titleButton[i].buttonName = temp.Attributes["name"].Value;
            titleButton[i].path = temp.Attributes["code"].Value;
            deckButton = Instantiate(deckButtonPrefab);
            deckButton.transform.SetParent(view.transform);
            deckButton.GetComponent<Button>().buttonName = titleButton[i].buttonName;
            deckButton.GetComponent<Button>().path = titleButton[i].path;
            TMP_Text tmp = deckButton.transform.GetComponentInChildren<TMP_Text>();
            tmp.text = titleButton[i].buttonName;
        }
    }

    public void OnDeckMaker()
    {
        GameObject deckMake = Instantiate(deckMaker);
        this.view = GameObject.FindWithTag("Canvas");
        deckMake.transform.SetParent(view.transform);
        deckMake.transform.localPosition = Vector3.zero;
    }

    public void OnCardSearch()
    {
        GameObject cardFind = Instantiate(cardSearch);
        this.view = GameObject.FindWithTag("Canvas");
        cardFind.transform.SetParent(view.transform);
        cardFind.transform.localPosition = Vector3.zero;
    }

    public void DeckIn()
    {
        GameObject cardButton = EventSystem.current.currentSelectedGameObject;
        deckButton = Instantiate(deckButtonPrefab);
        string kind = cardButton.GetComponent<CardInformation>().type;
        if (kind == "G¿Ø¥÷")
        {
            this.view = GameObject.FindWithTag("GUnit");
        }
        else if (kind == "≥Î∏÷ ¿Ø¥÷")
        {
            this.view = GameObject.FindWithTag("NUnit");
        }
        else if (kind == "∆Æ∏Æ∞≈ ¿Ø¥÷")
        {
            this.view = GameObject.FindWithTag("TUnit");
        }
        else
        {
            return;
        }
        deckButton.transform.SetParent(view.transform);
        deckButton.GetComponent<CardInformation>().cname = cardButton.GetComponent<CardInformation>().cname;
        deckButton.GetComponent<CardInformation>().clan = cardButton.GetComponent<CardInformation>().clan;
        deckButton.GetComponent<CardInformation>().nation = cardButton.GetComponent<CardInformation>().nation;
        deckButton.GetComponent<CardInformation>().code = cardButton.GetComponent<CardInformation>().code;
        deckButton.GetComponent<CardInformation>().type = cardButton.GetComponent<CardInformation>().type;
        deckButton.GetComponent<CardInformation>().LoadDeckCard();

        this.view = GameObject.FindWithTag("CardSearch");
        Destroy(view);
    }

    public void EDeckIn()
    {
        GameObject cardButton = EventSystem.current.currentSelectedGameObject;
        deckButton = Instantiate(deckButtonPrefab);
        string kind = cardButton.GetComponent<CardInformation>().type;
        if (kind == "G¿Ø¥÷")
        {
            this.view = GameObject.FindWithTag("EGUnit");
        }
        else if (kind == "≥Î∏÷ ¿Ø¥÷")
        {
            this.view = GameObject.FindWithTag("ENUnit");
        }
        else if (kind == "∆Æ∏Æ∞≈ ¿Ø¥÷")
        {
            this.view = GameObject.FindWithTag("ETUnit");
        }
        else
        {
            return;
        }
        deckButton.transform.SetParent(view.transform);
        deckButton.GetComponent<CardInformation>().cname = cardButton.GetComponent<CardInformation>().cname;
        deckButton.GetComponent<CardInformation>().clan = cardButton.GetComponent<CardInformation>().clan;
        deckButton.GetComponent<CardInformation>().nation = cardButton.GetComponent<CardInformation>().nation;
        deckButton.GetComponent<CardInformation>().code = cardButton.GetComponent<CardInformation>().code;
        deckButton.GetComponent<CardInformation>().type = cardButton.GetComponent<CardInformation>().type;
        deckButton.GetComponent<CardInformation>().LoadDeckCard();

        this.view = GameObject.FindWithTag("CardSearch");
        Destroy(view);
    }

    public void AddDeck()
    {
        string deckName = nameInput.text;
        if (deckName.Length == 0)
        {
            return;
        }

        XmlDocument document = new XmlDocument();
        document.Load("Assets/Resource/DeckList.xml");

        XmlNode deck = document.SelectSingleNode("DeckList");
        XmlNodeList deckList = deck.SelectNodes("Deck");

        for (int i = 0; i < deckList.Count; i++)
        {
            if (deckName == deckList[i].Attributes["name"].Value)
            {
                return;
            }
        }

        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration("1.0", "utf-8", "yes"));
        XmlNode root = xmlDocument.CreateElement("", deckName, "");
        xmlDocument.AppendChild(root);
        XmlNode GUnit = xmlDocument.CreateElement("", "GUnit", "");
        XmlNode NUnit = xmlDocument.CreateElement("", "NUnit", "");
        XmlNode TUnit = xmlDocument.CreateElement("", "TUnit", "");
        root.AppendChild(GUnit);
        root.AppendChild(NUnit);
        root.AppendChild(TUnit);

        List<CardInformation> GUnitList = new List<CardInformation>();
        List<CardInformation> NUnitList = new List<CardInformation>();
        List<CardInformation> TUnitList = new List<CardInformation>();

        int check;
        this.view = GameObject.FindWithTag("GUnit");
        foreach (Transform child in view.transform)
        {
            CardInformation cardInformation = new CardInformation();
            cardInformation.count = 1;
            cardInformation.cname = child.gameObject.GetComponent<CardInformation>().cname;
            cardInformation.clan = child.gameObject.GetComponent<CardInformation>().clan;
            cardInformation.nation = child.gameObject.GetComponent<CardInformation>().nation;
            cardInformation.code = child.gameObject.GetComponent<CardInformation>().code;
            cardInformation.type = child.gameObject.GetComponent<CardInformation>().type;
            check = 0;
            for (int i = 0; i < GUnitList.Count; i++)
            {
                if (cardInformation.code == GUnitList[i].code)
                {
                    GUnitList[i].count++;
                    check = 1;
                    break;
                }
            }
            if (check == 0)
            {
                GUnitList.Add(cardInformation);
            }
        }

        this.view = GameObject.FindWithTag("NUnit");
        foreach (Transform child in view.transform)
        {
            CardInformation cardInformation = new CardInformation();
            cardInformation.count = 1;
            cardInformation.cname = child.gameObject.GetComponent<CardInformation>().cname;
            cardInformation.clan = child.gameObject.GetComponent<CardInformation>().clan;
            cardInformation.nation = child.gameObject.GetComponent<CardInformation>().nation;
            cardInformation.code = child.gameObject.GetComponent<CardInformation>().code;
            cardInformation.type = child.gameObject.GetComponent<CardInformation>().type;
            check = 0;
            for (int i = 0; i < NUnitList.Count; i++)
            {
                if (cardInformation.code == NUnitList[i].code)
                {
                    NUnitList[i].count++;
                    check = 1;
                    break;
                }
            }
            if (check == 0)
            {
                NUnitList.Add(cardInformation);
            }
        }

        this.view = GameObject.FindWithTag("TUnit");
        foreach (Transform child in view.transform)
        {
            CardInformation cardInformation = new CardInformation();
            cardInformation.count = 1;
            cardInformation.cname = child.gameObject.GetComponent<CardInformation>().cname;
            cardInformation.clan = child.gameObject.GetComponent<CardInformation>().clan;
            cardInformation.nation = child.gameObject.GetComponent<CardInformation>().nation;
            cardInformation.code = child.gameObject.GetComponent<CardInformation>().code;
            cardInformation.type = child.gameObject.GetComponent<CardInformation>().type;
            check = 0;
            for (int i = 0; i < TUnitList.Count; i++)
            {
                if (cardInformation.code == TUnitList[i].code)
                {
                    TUnitList[i].count++;
                    check = 1;
                    break;
                }
            }
            if (check == 0)
            {
                TUnitList.Add(cardInformation);
            }
        }

        GameObject deckChecker = GameObject.FindWithTag("SystemManager");
        deckChecker.GetComponent<DeckChecker>().deckCheck = deckChecker.GetComponent<DeckChecker>().CheckDeck(GUnitList, NUnitList, TUnitList);

        if (deckChecker.GetComponent<DeckChecker>().deckCheck == false)
        {
            Debug.Log("¡∂∞« æ»µ ");
            return;
        }

        XmlElement newDeck = document.CreateElement("Deck");
        newDeck.SetAttribute("name", deckName);
        newDeck.SetAttribute("code", deckName);
        deck.AppendChild(newDeck);
        document.Save("Assets/Resource/DeckList.xml");

        for (int i = 0; i < GUnitList.Count; i++)
        {
            XmlElement card = xmlDocument.CreateElement("Card");
            card.SetAttribute("name", GUnitList[i].cname);
            card.SetAttribute("nation", GUnitList[i].nation);
            card.SetAttribute("clan", GUnitList[i].clan);
            card.SetAttribute("type", GUnitList[i].type);
            card.SetAttribute("code", GUnitList[i].code);
            card.SetAttribute("count", GUnitList[i].count.ToString());
            GUnit.AppendChild(card);
        }

        for (int i = 0; i < NUnitList.Count; i++)
        {
            XmlElement card = xmlDocument.CreateElement("Card");
            card.SetAttribute("name", NUnitList[i].cname);
            card.SetAttribute("nation", NUnitList[i].nation);
            card.SetAttribute("clan", NUnitList[i].clan);
            card.SetAttribute("type", NUnitList[i].type);
            card.SetAttribute("code", NUnitList[i].code);
            card.SetAttribute("count", NUnitList[i].count.ToString());
            NUnit.AppendChild(card);
        }

        for (int i = 0; i < TUnitList.Count; i++)
        {
            XmlElement card = xmlDocument.CreateElement("Card");
            card.SetAttribute("name", TUnitList[i].cname);
            card.SetAttribute("nation", TUnitList[i].nation);
            card.SetAttribute("clan", TUnitList[i].clan);
            card.SetAttribute("type", TUnitList[i].type);
            card.SetAttribute("code", TUnitList[i].code);
            card.SetAttribute("count", TUnitList[i].count.ToString());
            TUnit.AppendChild(card);
        }

        xmlDocument.Save("Assets/Resource/" + deckName + ".xml");

        GameObject systemManager = GameObject.FindWithTag("SystemManager");
        systemManager.GetComponent<DeckSystem>().ReadDeckInfo();

        GameObject closeView = this.transform.parent.gameObject;
        Destroy(closeView);
        Debug.Log("¿˙¿Â");
    }

    public void DeleteDeck()
    {
        this.view = this.transform.parent.gameObject;
        string target = view.GetComponent<Button>().buttonName;

        DatabaseManager databaseManager = new DatabaseManager();
        string targetPath = databaseManager.GetXmlPath(target);

        XmlDocument document = new XmlDocument();
        document.Load("Assets/Resource/DeckList.xml");

        XmlNode deck = document.SelectSingleNode("DeckList");
        XmlNodeList deckList = deck.SelectNodes("Deck");

        int i;
        for (i = 0; i < deckList.Count; i++)
        {
            if (target == deckList[i].Attributes["name"].Value)
            {
                break;
            }
        }

        deck.RemoveChild(deck.ChildNodes[i]);
        document.Save("Assets/Resource/DeckList.xml");

        File.Delete(targetPath);

        GameObject systemManager = GameObject.FindWithTag("SystemManager");
        systemManager.GetComponent<DeckSystem>().ReadDeckInfo();

        Destroy(view);
    }

    public void OnEdit()
    {
        GameObject deckMake = Instantiate(deckMaker);
        this.view = GameObject.FindWithTag("Canvas");
        deckMake.transform.SetParent(view.transform);
        deckMake.transform.localPosition = Vector3.zero;

        this.view = this.transform.parent.gameObject;
        string target = view.GetComponent<Button>().buttonName;

        deckMake.GetComponent<Button>().buttonName = view.GetComponent<Button>().buttonName;
        deckMake.GetComponent<Button>().path = view.GetComponent<Button>().path;

        List<CardSystem> GUnitList = new List<CardSystem>();
        List<CardSystem> NUnitList = new List<CardSystem>();
        List<CardSystem> TUnitList = new List<CardSystem>();

        int count;

        DatabaseManager databaseManager = new DatabaseManager();
        string targetPath = databaseManager.GetXmlPath(target);

        XmlDocument document = new XmlDocument();
        document.Load(targetPath);

        XmlNode GList = document.SelectSingleNode(target + "/GUnit");
        XmlNodeList GUList = GList.SelectNodes("Card");
        XmlNode NList = document.SelectSingleNode(target + "/NUnit");
        XmlNodeList NUList = NList.SelectNodes("Card");
        XmlNode TList = document.SelectSingleNode(target + "/TUnit");
        XmlNodeList TUList = TList.SelectNodes("Card");

        count = 0;
        for (int i = 0; i < GUList.Count; i++)
        {
            XmlNode temp = GUList[i];
            for (int j = 0; j < int.Parse(temp.Attributes["count"].Value); j++)
            {
                CardSystem GUnit = new CardSystem();
                GUnit.cardName = temp.Attributes["name"].Value;
                GUnit.cardClan = temp.Attributes["clan"].Value;
                GUnit.cardNation = temp.Attributes["nation"].Value;
                GUnit.cardType = temp.Attributes["type"].Value;
                GUnit.cardPath = temp.Attributes["code"].Value;
                GUnitList.Add(GUnit);
                count++;
            }
        }

        count = 0;
        for (int i = 0; i < NUList.Count; i++)
        {
            XmlNode temp = NUList[i];
            for (int j = 0; j < int.Parse(temp.Attributes["count"].Value); j++)
            {
                CardSystem NUnit = new CardSystem();
                NUnit.cardName = temp.Attributes["name"].Value;
                NUnit.cardClan = temp.Attributes["clan"].Value;
                NUnit.cardNation = temp.Attributes["nation"].Value;
                NUnit.cardType = temp.Attributes["type"].Value;
                NUnit.cardPath = temp.Attributes["code"].Value;
                NUnitList.Add(NUnit);
                count++;
            }
        }

        count = 0;
        for (int i = 0; i < TUList.Count; i++)
        {
            XmlNode temp = TUList[i];
            for (int j = 0; j < int.Parse(temp.Attributes["count"].Value); j++)
            {
                CardSystem TUnit = new CardSystem();
                TUnit.cardName = temp.Attributes["name"].Value;
                TUnit.cardClan = temp.Attributes["clan"].Value;
                TUnit.cardNation = temp.Attributes["nation"].Value;
                TUnit.cardType = temp.Attributes["type"].Value;
                TUnit.cardPath = temp.Attributes["code"].Value;
                TUnitList.Add(TUnit);
                count++;
            }
        }

        for (int i = 0; i < GUnitList.Count; i++)
        {
            view = GameObject.FindWithTag("EGUnit");
            deckButton = Instantiate(deckButtonPrefab);
            deckButton.GetComponent<CardInformation>().cname = GUnitList[i].cardName;
            deckButton.GetComponent<CardInformation>().clan = GUnitList[i].cardClan;
            deckButton.GetComponent<CardInformation>().nation = GUnitList[i].cardNation;
            deckButton.GetComponent<CardInformation>().code = GUnitList[i].cardPath;
            deckButton.GetComponent<CardInformation>().type = GUnitList[i].cardType;
            deckButton.transform.SetParent(view.transform);
            deckButton.GetComponent<CardInformation>().LoadDeckCard();
        }

        for (int i = 0; i < NUnitList.Count; i++)
        {
            view = GameObject.FindWithTag("ENUnit");
            deckButton = Instantiate(deckButtonPrefab);
            deckButton.GetComponent<CardInformation>().cname = NUnitList[i].cardName;
            deckButton.GetComponent<CardInformation>().clan = NUnitList[i].cardClan;
            deckButton.GetComponent<CardInformation>().nation = NUnitList[i].cardNation;
            deckButton.GetComponent<CardInformation>().code = NUnitList[i].cardPath;
            deckButton.GetComponent<CardInformation>().type = NUnitList[i].cardType;
            deckButton.transform.SetParent(view.transform);
            deckButton.GetComponent<CardInformation>().LoadDeckCard();
        }

        for (int i = 0; i < TUnitList.Count; i++)
        {
            view = GameObject.FindWithTag("ETUnit");
            deckButton = Instantiate(deckButtonPrefab);
            deckButton.GetComponent<CardInformation>().cname = TUnitList[i].cardName;
            deckButton.GetComponent<CardInformation>().clan = TUnitList[i].cardClan;
            deckButton.GetComponent<CardInformation>().nation = TUnitList[i].cardNation;
            deckButton.GetComponent<CardInformation>().code = TUnitList[i].cardPath;
            deckButton.GetComponent<CardInformation>().type = TUnitList[i].cardType;
            deckButton.transform.SetParent(view.transform);
            deckButton.GetComponent<CardInformation>().LoadDeckCard();
        }
    }

    public void EditDeck()
    {
        this.view = this.transform.parent.gameObject;
        string target = view.GetComponent<Button>().buttonName;

        List<CardInformation> GUnitList = new List<CardInformation>();
        List<CardInformation> NUnitList = new List<CardInformation>();
        List<CardInformation> TUnitList = new List<CardInformation>();

        int check;
        this.view = GameObject.FindWithTag("EGUnit");
        foreach (Transform child in view.transform)
        {
            CardInformation cardInformation = new CardInformation();
            cardInformation.count = 1;
            cardInformation.cname = child.gameObject.GetComponent<CardInformation>().cname;
            cardInformation.clan = child.gameObject.GetComponent<CardInformation>().clan;
            cardInformation.nation = child.gameObject.GetComponent<CardInformation>().nation;
            cardInformation.code = child.gameObject.GetComponent<CardInformation>().code;
            cardInformation.type = child.gameObject.GetComponent<CardInformation>().type;
            check = 0;
            for (int i = 0; i < GUnitList.Count; i++)
            {
                if (cardInformation.code == GUnitList[i].code)
                {
                    GUnitList[i].count++;
                    check = 1;
                    break;
                }
            }
            if (check == 0)
            {
                GUnitList.Add(cardInformation);
            }
        }

        this.view = GameObject.FindWithTag("ENUnit");
        foreach (Transform child in view.transform)
        {
            CardInformation cardInformation = new CardInformation();
            cardInformation.count = 1;
            cardInformation.cname = child.gameObject.GetComponent<CardInformation>().cname;
            cardInformation.clan = child.gameObject.GetComponent<CardInformation>().clan;
            cardInformation.nation = child.gameObject.GetComponent<CardInformation>().nation;
            cardInformation.code = child.gameObject.GetComponent<CardInformation>().code;
            cardInformation.type = child.gameObject.GetComponent<CardInformation>().type;
            check = 0;
            for (int i = 0; i < NUnitList.Count; i++)
            {
                if (cardInformation.code == NUnitList[i].code)
                {
                    NUnitList[i].count++;
                    check = 1;
                    break;
                }
            }
            if (check == 0)
            {
                NUnitList.Add(cardInformation);
            }
        }

        this.view = GameObject.FindWithTag("ETUnit");
        foreach (Transform child in view.transform)
        {
            CardInformation cardInformation = new CardInformation();
            cardInformation.count = 1;
            cardInformation.cname = child.gameObject.GetComponent<CardInformation>().cname;
            cardInformation.clan = child.gameObject.GetComponent<CardInformation>().clan;
            cardInformation.nation = child.gameObject.GetComponent<CardInformation>().nation;
            cardInformation.code = child.gameObject.GetComponent<CardInformation>().code;
            cardInformation.type = child.gameObject.GetComponent<CardInformation>().type;
            check = 0;
            for (int i = 0; i < TUnitList.Count; i++)
            {
                if (cardInformation.code == TUnitList[i].code)
                {
                    TUnitList[i].count++;
                    check = 1;
                    break;
                }
            }
            if (check == 0)
            {
                TUnitList.Add(cardInformation);
            }
        }

        GameObject deckChecker = GameObject.FindWithTag("SystemManager");
        deckChecker.GetComponent<DeckChecker>().deckCheck = deckChecker.GetComponent<DeckChecker>().CheckDeck(GUnitList, NUnitList, TUnitList);

        if (deckChecker.GetComponent<DeckChecker>().deckCheck == false)
        {
            Debug.Log("¡∂∞« æ»µ ");
            return;
        }

        Debug.Log("¡∂∞« µ ");

        DatabaseManager databaseManager = new DatabaseManager();
        string targetPath = databaseManager.GetXmlPath(target);

        File.Delete(targetPath);

        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration("1.0", "utf-8", "yes"));
        XmlNode root = xmlDocument.CreateElement("", target, "");
        xmlDocument.AppendChild(root);
        XmlNode GUnit = xmlDocument.CreateElement("", "GUnit", "");
        XmlNode NUnit = xmlDocument.CreateElement("", "NUnit", "");
        XmlNode TUnit = xmlDocument.CreateElement("", "TUnit", "");
        root.AppendChild(GUnit);
        root.AppendChild(NUnit);
        root.AppendChild(TUnit);

        for (int i = 0; i < GUnitList.Count; i++)
        {
            XmlElement card = xmlDocument.CreateElement("Card");
            card.SetAttribute("name", GUnitList[i].cname);
            card.SetAttribute("nation", GUnitList[i].nation);
            card.SetAttribute("clan", GUnitList[i].clan);
            card.SetAttribute("type", GUnitList[i].type);
            card.SetAttribute("code", GUnitList[i].code);
            card.SetAttribute("count", GUnitList[i].count.ToString());
            GUnit.AppendChild(card);
        }

        for (int i = 0; i < NUnitList.Count; i++)
        {
            XmlElement card = xmlDocument.CreateElement("Card");
            card.SetAttribute("name", NUnitList[i].cname);
            card.SetAttribute("nation", NUnitList[i].nation);
            card.SetAttribute("clan", NUnitList[i].clan);
            card.SetAttribute("type", NUnitList[i].type);
            card.SetAttribute("code", NUnitList[i].code);
            card.SetAttribute("count", NUnitList[i].count.ToString());
            NUnit.AppendChild(card);
        }

        for (int i = 0; i < TUnitList.Count; i++)
        {
            XmlElement card = xmlDocument.CreateElement("Card");
            card.SetAttribute("name", TUnitList[i].cname);
            card.SetAttribute("nation", TUnitList[i].nation);
            card.SetAttribute("clan", TUnitList[i].clan);
            card.SetAttribute("type", TUnitList[i].type);
            card.SetAttribute("code", TUnitList[i].code);
            card.SetAttribute("count", TUnitList[i].count.ToString());
            TUnit.AppendChild(card);
        }

        xmlDocument.Save(targetPath);

        GameObject closeView = this.transform.parent.gameObject;
        Destroy(closeView);
        Debug.Log("¿˙¿Â");
    }

    IEnumerator LoadImage(RawImage image, string path)
    {
        var url = $"https://cf-vanguard.com/wordpress/wp-content/images/cardlist/{path}.png";
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            image.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }
    }
}