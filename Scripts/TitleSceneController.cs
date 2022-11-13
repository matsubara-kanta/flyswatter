using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneController : SceneControllerBase
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
        if (!m_bFadeOut && Input.GetMouseButtonDown(0))
        {
            m_bActive = false;
        }
        base.Update();

        //  �Q�[���̐i�s��Ԃɂ���ď�����ς���
        switch (m_GameStateEnum)
        {
            case GameState.STATE_FADE_OUT:
                //  �^�C�}�[�v����~
                //TimerManager.Instance.IsStop = true;
                break;
            case GameState.STATE_BEGIN:
                //  �^�C�}�[�v���J�n
                //TimerManager.Instance.IsStop = false;
                break;
            case GameState.STATE_PROGRESS:
                //if (m_bStop) //  ��~���Ă���
                //    //  �^�C�}�[�v����~
                //    TimerManager.Instance.IsStop = true;
                //else
                //    //  �^�C�}�[�v���ĊJ
                //    TimerManager.Instance.IsStop = false;
                break;
            case GameState.STATE_END:
                //  �^�C�}�[�v���ĊJ
                //TimerManager.Instance.IsStop = true;
                if (!m_bFadeIn)
                    SceneManager.LoadScene("GameScene");
                break;
        }
    }
}
