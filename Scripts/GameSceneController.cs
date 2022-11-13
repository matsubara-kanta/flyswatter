using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static TimerManager;

public class GameSceneController : SceneControllerBase
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        //  このシーンを有効にする
        m_bActive = true;
        //  ゲームの進行状態を「開始」にする
        m_GameStateEnum = GameState.STATE_BEGIN;
        //  タイマー計測開始
        TimerManager.Instance.IsStop = false;
    }

    // Update is called once per frame
    new void Update()
    {

        //  タイマーが０になったらゲーム終了
        if (TimerManager.Instance.GameTime < 0.0f)
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
                //  タイマー計測再開
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
                if (!m_bFadeIn)
                {
                    SceneManager.LoadScene("ResultScene");
                }
                break;
        }
    }
}
