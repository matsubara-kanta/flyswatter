using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStress : MonoBehaviour
{
    [SerializeField]
    private float m_fStressValue = 0.0f;
    #region private filed
    private PlayerController m_controller;
    private 
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        //  プレイヤーコントローラーの取得
        m_controller = transform.parent.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (m_controller.IsMouseDown)
        {
            if (!m_controller.IsStress)
                m_controller.AddStressValue(m_fStressValue);
        }
    }
}
