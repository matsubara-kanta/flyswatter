using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVibration : MonoBehaviour
{
    #region private filed
    /// <summary>
    /// 振動させるカメラ
    /// </summary>
    private Camera m_camera;
    /// <summary>
    /// リセット座標
    /// </summary>
    Vector3 m_resetPos = Vector3.zero;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        //  メインカメラ参照
        m_camera = Camera.main;
        //  リセットする座標の設定
        m_resetPos = m_camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vibe(0.25f, 0.1f);
        //}
    }

    /// <summary>
    /// カメラを振動させる
    /// </summary>
    /// <param name="vibeTime">
    /// 振動させる時間
    /// </param>
    /// <param name="magnitude">
    /// 倍率
    /// </param>
    public void Vibe(float vibeTime,float magnitude)
    {
        StartCoroutine(TakeVibe(vibeTime, magnitude));
    }

    private IEnumerator TakeVibe(float viveTime, float magnitude)
    {
        //  --- カメラを振動させる
        Vector3 pos = m_camera.transform.position;   //  カメラ座標参照
        float elapsedTime = 0.0f;   //  経過時間

        //  経過時間が振動時間よりも小さい間ループ
        while (elapsedTime < viveTime)
        {
            float x = pos.x + Random.Range(-1.0f, 1.0f) * magnitude;    //  ランダム振動ｘ
            float y = pos.y + Random.Range(-1.0f, 1.0f) * magnitude;    //  ランダム振動ｙ
            Vector3 viveVec = Vector3.one;  //  ランダム振動ｘ、ｙの依り代
            viveVec.x *= x; 
            viveVec.y *= y;
            viveVec.z = pos.z;

            //  ランダムに変化する座標を代入してカメラを振動させる
            m_camera.transform.position = viveVec;

            //  経過時間カウント
            elapsedTime += 1.0f *Time.deltaTime;
            yield return null;  //  1フレーム待つ
        }

        //  最後に座標をリセットさせる
        m_camera.transform.position = m_resetPos;
    }


}
