using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTime : MonoBehaviour
{
    #region provate filed
    /// <summary>
    /// ���ԕ\���p�e�L�X�g�i���j
    /// </summary>
    private Text m_timeMinitText = null;
    /// <summary>
    /// ���ԕ\���p�e�L�X�g�i10�̈ʕb�j
    /// </summary>
    private Text m_timeSecondText = null;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        //  --- �\���p�e�L�X�g�Q��
        m_timeMinitText = transform.GetChild(0).GetComponent<Text>(); //  ��
        m_timeSecondText = transform.GetChild(2).GetComponent<Text>(); //  10�̈ʕb
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
        //  �^�C�}�[�\��
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
