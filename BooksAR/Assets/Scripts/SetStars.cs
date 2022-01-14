using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStars : MonoBehaviour
{
    public GameObject StarsGO;

    public void SetStarsMethod(int stars)
    {
        int i = 0;
        Debug.Log(stars);
        foreach (Transform child in StarsGO.transform)
        {
            if (i <= stars)
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
