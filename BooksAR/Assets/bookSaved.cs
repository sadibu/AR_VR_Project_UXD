using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bookSaved : MonoBehaviour
{
    public float timer;
    private void OnEnable()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5)
        {
            this.gameObject.SetActive(false);
        }
    }
}
