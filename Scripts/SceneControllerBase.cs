using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneControllerBase : MonoBehaviour
{
    #region define
    /// <summary>
    /// ゲームの状態
    /// </summary>
    public enum GameState
    {
        /// <summary>
        /// フェードアウト
        /// </summary>
        STATE_FADE_OUT,
        /// <summary>
        /// 開始
        /// </summary>
        STATE_BEGIN,
        /// <summary>
        /// 進行中
        /// </summary>
        STATE_PROGRESS,
        /// <summary>
        /// 終了
        /// </summary>
        STATE_END,
    }
    #endregion

    #region serialize field
    [Header("ゲームの進行状態"), Tooltip("ゲームの進行状態")]
    [SerializeField]
    protected GameState m_GameStateEnum = GameState.STATE_BEGIN;
    /// <summary>
    /// フェードイメージ
    /// </summary>
    [Header("フェードイメージ")]
    [SerializeField]
    private Image m_fadeImage = null;
    #endregion

    #region protected filaed
    /// <summary>
    /// 停止フラグ
    /// </summary>
    protected bool m_bStop = false;
    /// <summary>
    /// このシーンがアクティブかどうか
    /// </summary>
    protected bool m_bActive = true;
    /// <summary>
    /// フェードスクリプト
    /// </summary>
    protected Fade m_fade = null;
    /// <summary>
    /// フェードインフラグ
    /// </summary>
    protected bool m_bFadeIn;
    /// <summary>
    /// フェードアウトフラグ
    /// </summary>
    protected bool m_bFadeOut;
    protected float m_fAlpha = 0.0f;
    #endregion
    // Start is called before the first frame update
    protected void Start()
    {
        m_fade = new Fade();
        m_bFadeOut = true;
        m_bFadeIn = false;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (!m_bActive)
        {
            m_bFadeIn = true;
        }
        if (m_bFadeOut)
        {
            m_fAlpha = m_fade.FadeOut(m_fadeImage);
            if (m_fAlpha < 0.0f)
                m_bFadeOut = false;
        }
        if (m_bFadeIn)
        {
            m_fAlpha = m_fade.FadeIn(m_fadeImage);
            if (m_fAlpha > 1.0f)
                m_bFadeIn = false;
        }

        //  シーンの状態変化
        if (m_bFadeOut)
        {
            m_GameStateEnum = GameState.STATE_FADE_OUT;
        }
        else if (m_bActive)
        {
            if (TimerManager.Instance.IsBegin)
                m_GameStateEnum = GameState.STATE_BEGIN;
            else
                m_GameStateEnum = GameState.STATE_PROGRESS;
        }
        else
        {
            m_GameStateEnum = GameState.STATE_END;
        }
    }
}
