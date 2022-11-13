using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static TimerManager;

public class CursorView : MonoBehaviour
{
    public Texture2D defaultTexture;
    public Texture2D hitTexture;
    private Vector2 hotSpot;
    Vector3 mousePos;
    [SerializeField] private bool canAttack;
    [SerializeField] private float hitInterval;

    public enum Player
    {
        Normal,
        Irritating
    }

    // Start is called before the first frame update
    void Start()
    {

         //ホットスポットを画像中央に設定(TextureはTexture2D)
        hotSpot = new Vector2(defaultTexture.width / 2 , 0);
        hotSpot.x += 50;
        hotSpot.y += 70;

        Debug.Log(hotSpot);

        //カーソル画像変更
        Cursor.SetCursor(defaultTexture, hotSpot, CursorMode.ForceSoftware);

        canAttack = true;

        hitInterval = 1.0f;
    }

    private void Update()
    {
        
        if (TimerManager.Instance.IsBegin)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

        // 右ボタンが押された瞬間に実行
        if (Input.GetMouseButtonDown(0))
        {
            //クリックのインターバル調整
            if (canAttack)
            {
                Attack();
            }

            //Debug.Log("左ボタンが押されました。");
            //Cursor.SetCursor(hitTexture, hotSpot, CursorMode.ForceSoftware);
            //StartCoroutine("SetTexture");

            //Vector2 mousePosition = Input.mousePosition;
            //Debug.Log(mousePosition);
        }

        mousePos = Input.mousePosition;
        mousePos.z = 10f;

        var worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        this.transform.position = worldPos;
    }

    IEnumerator SetTexture()
    {
        yield return new WaitForSeconds(0.1f);
        Cursor.SetCursor(defaultTexture, hotSpot, CursorMode.ForceSoftware);
    }

    void OnDestroy()
    {
        //textureをnullにすると元のカーソルに戻る
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
    }

    void Attack()
    {
        Debug.Log("左ボタンが押されました。");
        Cursor.SetCursor(hitTexture, hotSpot, CursorMode.ForceSoftware);
        StartCoroutine("SetTexture");

        canAttack = false;
        Invoke("FinishInterval", hitInterval);
    }

    void FinishInterval()
    {
        canAttack = true;
    }

    public bool getcanAttack()
    {
        return canAttack;
    }
}
