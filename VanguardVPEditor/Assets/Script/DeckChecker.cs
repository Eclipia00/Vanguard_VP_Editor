using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckChecker : MonoBehaviour
{
    public bool deckCheck;
    
    public bool CheckDeck(List<CardInformation> GUnit, List<CardInformation> NUnit, List<CardInformation> TUnit)
    {
        int count = 0;

        for (int i = 0; i < GUnit.Count; i++)
        {
            if (GUnit[i].count > 4)
            {
                return false;
            }
            count += GUnit[i].count;
        }
        if (count > 16)
        {
            return false;
        }

        count = 0;
        for (int i = 0; i < NUnit.Count; i++)
        {
            if (NUnit[i].count > 4)
            {
                return false;
            }
            count += NUnit[i].count;
        }
        if (count != 34)
        {
            return false;
        }

        count = 0;
        for (int i = 0; i < TUnit.Count; i++)
        {
            if (TUnit[i].count > 4)
            {
                return false;
            }
            count += TUnit[i].count;
        }
        if (count != 16)
        {
            return false;
        }

        string clan = NUnit[0].clan;
        for (int i = 0; i < NUnit.Count; i++)
        {
            if (clan != NUnit[i].clan)
            {
                return false;
            }
        }

        for (int i = 0; i < TUnit.Count; i++)
        {
            if (clan != TUnit[i].clan)
            {
                return false;
            }
        }

        return true;
    }
}
