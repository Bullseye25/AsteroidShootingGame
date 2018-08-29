using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    private int m_timer = 0;
    private Text m_timerDisplay;

    // Use this for initialization
    void Start()
    {
        m_timerDisplay = GameObject.Find("Timer").GetComponent<Text>();

        StartCoroutine(Counter());
    }

    IEnumerator Counter()
    {
        m_timer++;

        if (m_timer <= 3)
        {
            m_timerDisplay.text = m_timer.ToString();
        }
        else
        {
            m_timerDisplay.text = "GO!";
        }

        yield return new WaitForSeconds(1);
        StartCoroutine(Counter());
    }

}