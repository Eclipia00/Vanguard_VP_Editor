using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class CardInformation : MonoBehaviour
{
    public RawImage image;
    public string cname;
    public string clan;
    public string nation;
    public string text;
    public string code;
    public string type;
    public int count;
    public TextMeshProUGUI unitName;
    public TextMeshProUGUI unitClan;
    public TextMeshProUGUI unitNation;
    public TextMeshProUGUI unitText;

    public void LoadCard()
    {
        StartCoroutine(LoadImage(image, code));
        unitName.text = cname;
        unitClan.text = clan;
        unitNation.text = nation;
        unitText.text = text;
        this.gameObject.transform.position = Vector3.zero;
    }

    public void LoadDeckCard()
    {
        image = this.transform.Find("RawImage").gameObject.GetComponent<RawImage>();
        StartCoroutine(LoadImage(image, code));
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
