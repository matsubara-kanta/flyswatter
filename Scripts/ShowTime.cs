using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTime : MonoBehaviour
{
    #region provate filed
    /// <summary>
    /// 時間表示用テキスト（分）
    /// </summary>
    private Text m_timeMinitText = null;
    /// <summary>
    /// 時間表示用テキスト（10の位秒）
    /// </summary>
    private Text m_timeSecondText = null;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        //  --- 表示用テキスト参照
        m_timeMinitText = transform.GetChild(0).GetComponent<Text>(); //  分
        m_timeSecondText = transform.GetChild(2).GetComponent<Text>(); //  10の位秒
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerManager.Instance.IsBegin)
        {
            m_timeMinitText.enabled = false;
            m_timeSecondText.enabled = false;
        }
        else
        {
            m_timeMinitText.enabled = true;
            m_timeSecondText.enabled = true;
        }
        //  タイマー表示
        float time = TimerManager.Instance.GameTime;
        int min = (int)time / 60 - 1;
        if(min < 0) min = 0;
        int tenSec = (int)time % 60;

        string textTimeMinit = min.ToString("F0");
        string textTimeSecond = tenSec.ToString("F0");
        m_timeMinitText.text = textTimeMinit;
        m_timeSecondText.text = textTimeSecond;
    }
}
