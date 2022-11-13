using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    #region instance
    /// <summary>
    /// インスタンス
    /// </summary>
    private static TimerManager m_Instance = null;

    /// <summary>
    /// インスタンス参照用
    /// </summary>
    public static TimerManager Instance
    {
        get { return m_Instance; }
    }

    #endregion

    #region serialize filed
    [Header("開始時間（秒）"),Tooltip("開始時間（秒）")]
    [SerializeField]
    private float m_fStartTimeBase = 0.0f;
    [Header("ゲーム時間（秒）"), Tooltip("ゲーム時間（秒）")]
    [SerializeField]
    private float m_fGameTimeBase = 0.0f;
    [Header("タイムスケール"),Tooltip("タイムスケール（時間の進む速さ）")]
    [SerializeField]
    private float m_fTimeScale = 1.0f;
    #endregion

    #region property

    public bool SkillMode => m_bSkillMode;

    public float SkillSlowRate => m_SkillSlowRate;

    public bool Click => m_Click;

    #endregion

    #region private filed
    /// <summary>
    /// 変更用の開始時間
    /// </summary>
    private float m_fStartTime = 0.0f;
    /// <summary>
    /// 変更用のゲーム時間
    /// </summary>
    private float m_fGameTime = 0.0f;
    /// <summary>
    /// 開始直後かどうかのフラグ
    /// </summary>
    private bool m_bBegin = false;
    /// <summary>
    /// 停止フラグ
    /// </summary>
    private bool m_bStop = false;

    private bool m_bSkillMode = false;

    private float m_SkillSlowRate = 0f;

    private bool m_Click = false;

    #endregion

    private void Awake()
    {
        if (!m_Instance)
        {
            m_Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_bSkillMode = false;
        //  スタート時間初期化（設定した時間）
        m_fStartTime = m_fStartTimeBase;
        //  ゲーム時間初期化
        m_fGameTime = m_fGameTimeBase;
        //  ゲーム開始直後のフラグOFF
        m_bBegin = true;

        m_SkillSlowRate = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        // --- ゲームの時間を計測する
        float currentTime = m_fGameTime;    //  現在時間
        if (m_bBegin)
        {
            currentTime = m_fStartTime;
        }
        //print(currentTime);

        if (!m_bStop)
        {
            //  現在時間が０より大きい
            if (currentTime > 0.0f)
            {
                //  ゲーム時間カウント（減算）
                currentTime -= m_fTimeScale * Time.deltaTime;
                if (m_bBegin)
                {
                    //  スタート時間に代入
                    m_fStartTime = currentTime;
                }
                else
                {
                    //  ゲーム時間に代入
                    m_fGameTime = currentTime;
                }
            }
            else if (m_bBegin)
            {
                m_bBegin = false;
            }
        }
    }

    public void requestSkill(float requestTimeScale)
    {
        m_bSkillMode = true;

        m_SkillSlowRate = requestTimeScale;
    }

    public void finishSkill()
    {
        m_SkillSlowRate = 1f;
        m_bSkillMode = false;
    }

    public void mouseDown()
    {
        m_Click = true;
    }

    public void mouseUp()
    {
        m_Click = false;
    }

    #region propety
    /// <summary>
    /// 現在時間参照用
    /// </summary>
    public float GameTime
    {
        get { return m_fGameTime; }
    }

    /// <summary>
    /// スタート時間参照用
    /// </summary>
    public float StartTime
    {
        get { return m_fStartTime; }
    }

    /// <summary>
    /// 停止フラグ制御用
    /// </summary>
    public bool IsStop
    {
        set { m_bStop = value; }
    }
    /// <summary>
    /// ゲーム開始直後かどうかのフラグ制御用
    /// </summary>
    public bool IsBegin
    {
        get { return m_bBegin; }
        set { m_bBegin = value; }
    }
    #endregion
}
