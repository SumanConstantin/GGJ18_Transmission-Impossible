using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBar : MonoBehaviour {

    public GameObject heart1, heart2, heart3;

    private const uint heartCountMax = 3;
    private uint heartCount = heartCountMax;

    public void AddOneHeart()
    {
        if (heartCount == heartCountMax)
            return;

        heartCount++;
        UpdateVisuals();
    }

    public void RemoveOneHeart()
    {
        if (heartCount < 1)
            return;

        heartCount--;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        for(int i=1; i<4; i++)
        {
            GameObject heartGO = (GameObject)this.GetType().GetField("heart"+i).GetValue(this);
            heartGO.SetActive(heartCount >= i);
        }
    }
}
