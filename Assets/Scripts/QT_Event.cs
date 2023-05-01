using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QT_Event : MonoBehaviour
{
    public float fillAmount = 0;

    public float timeThreshold = 0;

    public bool eventSuccess= false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fillAmount += 0.1f;
        }

        timeThreshold += Time.deltaTime;
        if (timeThreshold > .1)
        {
            timeThreshold = 0;
            fillAmount -= .02f;
        }

        if (fillAmount < 0)
        {
            fillAmount = 0;
        }

        if (fillAmount >= 1)
        {
            eventSuccess = true;
            
        }

        GetComponent<Image>().fillAmount = fillAmount;

    }
}
