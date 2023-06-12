using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    public GameObject systemManager;

    // Start is called before the first frame update
    void Start()
    {
        systemManager.GetComponent<UIManager>().OnCardSystem();
    }
}
