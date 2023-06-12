using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    string dataPath;
    
    public string GetXmlPath(string path)
    {
        dataPath = "Assets/Resource/XML/" + path + ".xml";
        return dataPath;
    }
}
