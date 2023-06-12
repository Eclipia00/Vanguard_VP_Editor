using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DeckInformation : MonoBehaviour
{
    public RawImage image;
    public GameObject deckCardPrefabs;
    public GameObject deckCardButtons;
    public GameObject deckInfoPrefab;
    
    public void LoadDeckList()
    {
        string name = EventSystem.current.currentSelectedGameObject.GetComponent<Button>().buttonName;
        string path = EventSystem.current.currentSelectedGameObject.GetComponent<Button>().path;
        GameObject mainview = GameObject.FindWithTag("Canvas");
        GameObject deckInfo = Instantiate(deckInfoPrefab);
        deckInfo.transform.SetParent(mainview.transform);
        deckInfo.transform.localPosition = Vector3.zero;
        deckInfo.GetComponent<Button>().path = path;
        deckInfo.GetComponent<Button>().buttonName = name;

        List<CardSystem> GUnitList = new List<CardSystem>();
        List<CardSystem> NUnitList = new List<CardSystem>();
        List<CardSystem> TUnitList = new List<CardSystem>();

        int count;

        XmlDocument document = new XmlDocument();
        DatabaseManager databaseManager = new DatabaseManager();
        path = databaseManager.GetXmlPath(name);
        document.Load(path);

        XmlNode GList = document.SelectSingleNode(name + "/GUnit");
        XmlNodeList GUList = GList.SelectNodes("Card");
        XmlNode NList = document.SelectSingleNode(name + "/NUnit");
        XmlNodeList NUList = NList.SelectNodes("Card");
        XmlNode TList = document.SelectSingleNode(name + "/TUnit");
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

        GameObject view;
        for (int i = 0; i < GUnitList.Count; i++)
        {
            view = GameObject.FindWithTag("GUnit");
            deckCardButtons = Instantiate(deckCardPrefabs);
            image = deckCardButtons.transform.Find("RawImage").gameObject.GetComponent<RawImage>();
            StartCoroutine(LoadImage(image, GUnitList[i].cardPath));
            deckCardButtons.GetComponent<CardInformation>().cname = GUnitList[i].cardName;
            deckCardButtons.GetComponent<CardInformation>().clan = GUnitList[i].cardClan;
            deckCardButtons.GetComponent<CardInformation>().nation = GUnitList[i].cardNation;
            deckCardButtons.GetComponent<CardInformation>().code = GUnitList[i].cardPath;
            deckCardButtons.GetComponent<CardInformation>().type = GUnitList[i].cardType;
            deckCardButtons.transform.SetParent(view.transform);
        }

        for (int i = 0; i < NUnitList.Count; i++)
        {
            view = GameObject.FindWithTag("NUnit");
            deckCardButtons = Instantiate(deckCardPrefabs);
            image = deckCardButtons.transform.Find("RawImage").gameObject.GetComponent<RawImage>();
            StartCoroutine(LoadImage(image, NUnitList[i].cardPath));
            deckCardButtons.GetComponent<CardInformation>().cname = NUnitList[i].cardName;
            deckCardButtons.GetComponent<CardInformation>().clan = NUnitList[i].cardClan;
            deckCardButtons.GetComponent<CardInformation>().nation = NUnitList[i].cardNation;
            deckCardButtons.GetComponent<CardInformation>().code = NUnitList[i].cardPath;
            deckCardButtons.GetComponent<CardInformation>().type = NUnitList[i].cardType;
            deckCardButtons.transform.SetParent(view.transform);
        }

        for (int i = 0; i < TUnitList.Count; i++)
        {
            view = GameObject.FindWithTag("TUnit");
            deckCardButtons = Instantiate(deckCardPrefabs);
            image = deckCardButtons.transform.Find("RawImage").gameObject.GetComponent<RawImage>();
            StartCoroutine(LoadImage(image, TUnitList[i].cardPath));
            deckCardButtons.GetComponent<CardInformation>().cname = TUnitList[i].cardName;
            deckCardButtons.GetComponent<CardInformation>().clan = TUnitList[i].cardClan;
            deckCardButtons.GetComponent<CardInformation>().nation = TUnitList[i].cardNation;
            deckCardButtons.GetComponent<CardInformation>().code = TUnitList[i].cardPath;
            deckCardButtons.GetComponent<CardInformation>().type = TUnitList[i].cardType;
            deckCardButtons.transform.SetParent(view.transform);
        }
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
