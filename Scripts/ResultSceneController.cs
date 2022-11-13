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
        //  このシーンを有効にする
        m_bActive = true;
        m_GameStateEnum = GameState.STATE_BEGIN;
    }

    // Update is called once per frame
    new void Update()
    {
        bool IsCrick = Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1);

        //  タイマーが０になったらゲーム終了
        if (TimerManager.Instance.GameTime < 0.0f || IsCrick && !m_bFadeOut)
        {
            m_bActive = false;
        }
        base.Update();

        //  ゲームの進行状態によって処理を変える
        switch (m_GameStateEnum)
        {
            case GameState.STATE_FADE_OUT:
                //  タイマー計測停止
                TimerManager.Instance.IsStop = true;
                break;
            case GameState.STATE_BEGIN:
                //  タイマー計測開始
                TimerManager.Instance.IsStop = false;
                break;
            case GameState.STATE_PROGRESS:
                if (m_bStop) //  停止している
                    //  タイマー計測停止
                    TimerManager.Instance.IsStop = true;
                else
                    //  タイマー計測再開
                    TimerManager.Instance.IsStop = false;
                break;
            case GameState.STATE_END:
                //  タイマー計測再開
                //TimerManager.Instance.IsStop = true;
                if (!m_bFadeIn)
                    SceneManager.LoadScene("TitleScene");
                break;
        }
    }
}
