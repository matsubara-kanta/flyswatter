using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultSceneController : SceneControllerBase
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        //  ���̃V�[����L���ɂ���
        m_bActive = true;
        m_GameStateEnum = GameState.STATE_BEGIN;
    }

    // Update is called once per frame
    new void Update()
    {
        bool IsCrick = Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1);

        //  �^�C�}�[���O�ɂȂ�����Q�[���I��
        if (TimerManager.Instance.GameTime < 0.0f || IsCrick && !m_bFadeOut)
        {
            m_bActive = false;
        }
        base.Update();

        //  �Q�[���̐i�s��Ԃɂ���ď�����ς���
        switch (m_GameStateEnum)
        {
            case GameState.STATE_FADE_OUT:
                //  �^�C�}�[�v����~
                TimerManager.Instance.IsStop = true;
                break;
            case GameState.STATE_BEGIN:
                //  �^�C�}�[�v���J�n
                TimerManager.Instance.IsStop = false;
                break;
            case GameState.STATE_PROGRESS:
                if (m_bStop) //  ��~���Ă���
                    //  �^�C�}�[�v����~
                    TimerManager.Instance.IsStop = true;
                else
                    //  �^�C�}�[�v���ĊJ
                    TimerManager.Instance.IsStop = false;
                break;
            case GameState.STATE_END:
                //  �^�C�}�[�v���ĊJ
                //TimerManager.Instance.IsStop = true;
                if (!m_bFadeIn)
                    SceneManager.LoadScene("TitleScene");
                break;
        }
    }
}
