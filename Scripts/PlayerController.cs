using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static TimerManager;

public class PlayerController : MonoBehaviour
{
    public Texture2D defaultTexture;
    public Texture2D hitTexture;
    public Texture2D SdefaultTexture;
    public Texture2D ShitTexture;

    private Vector2 hotSpot;
    Vector3 mousePos;
    [SerializeField] private bool canAttack;
    [SerializeField] private float hitInterval;
    [SerializeField] private float MAX_STRESS = 0.0f;

    public AudioClip flyswatterSE;
    public AudioClip  deadSE;
    AudioSource audioSource;
    // Vector3 mousePos, worldPos;
    public static int score;
    //private int highScore;
    public GameObject score_object = null;
    //public GameObject  highscore_object = null;
    /// <summary>
    /// ?X?g???X?l
    /// </summary>
    private float m_fStressValue = 0.0f;
    [SerializeField]
    private CameraVibration cameraVibration = null;
    private bool m_bStress = false;
    private BoxCollider2D m_boxCollider = null; 
    private CapsuleCollider2D m_circleCollider = null;

    public enum PlayerState
    {
        Normal,
        Irritating
    }

    private bool _MouseDown = false;
    private PlayerState _State;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        score = 0;

        //?z?b?g?X?|?b?g????????????????(Texture??Texture2D)
        hotSpot = new Vector2(defaultTexture.width / 2, 0);
        hotSpot.x += 50;
        hotSpot.y += 70;

        Debug.Log(hotSpot);

        //?J?[?\?????????X
        Cursor.SetCursor(defaultTexture, hotSpot, CursorMode.ForceSoftware);

        canAttack = true;

        //?N???b?N???u
        //hitInterval = 1.0f;
        _State = PlayerState.Normal;

        m_boxCollider = GetComponent<BoxCollider2D>();
        m_circleCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerManager.Instance.IsBegin)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

        switch (_State)
        {
            case PlayerState.Normal:
                m_boxCollider.enabled = true;
                m_circleCollider.enabled = false;
                //transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                if (m_fStressValue > MAX_STRESS)
                {
                    _State = PlayerState.Irritating;
                    m_bStress = true;
                }
                break;
            case PlayerState.Irritating:
                //transform.localScale = new Vector3(10.0f, 10.0f, 1.0f);
                m_fStressValue -= 1.0f * Time.deltaTime;
                m_boxCollider.enabled = false;
                m_circleCollider.enabled = true;
                if (_MouseDown)
                {
                    cameraVibration.Vibe(0.2f, 0.5f);
                }
                if (m_fStressValue < 0.0f)
                {
                    _State = PlayerState.Normal;
                    m_bStress = false;
                }
                break;
        }
       
        // ?E?{?^?????????????u???????s
        if (Input.GetMouseButtonDown(0))
        {
            //?N???b?N???C???^?[?o??????
            if (canAttack)
            {
                if (_State == PlayerState.Normal)
                    Attack();
                else
                    StressAttack();
            }



            if (score_object != null)
            {
                Text score_text = score_object.GetComponent<Text>();
                score_text.text = "SCORE: " + score;
            }
            //Debug.Log("???{?^?????????????????B");
            //Cursor.SetCursor(hitTexture, hotSpot, CursorMode.ForceSoftware);
            //StartCoroutine("SetTexture");

            //Vector2 mousePosition = Input.mousePosition;
            //Debug.Log(mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _MouseDown = false;

            TimerManager.Instance.mouseUp();
        }


        mousePos = Input.mousePosition;
        mousePos.z = 10f;

        var worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        this.transform.position = worldPos;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fly")
        {
            Debug.Log("mult");
            if (_MouseDown)
            {

                Debug.Log("Hit");
                audioSource.PlayOneShot(deadSE, 0.2F);
                Destroy(collision.gameObject);
                score++;
            }
        }
    }

    IEnumerator SetTexture()
    {
        yield return new WaitForSeconds(0.1f);
        Cursor.SetCursor(defaultTexture, hotSpot, CursorMode.ForceSoftware);
    }

    IEnumerator StressSetTexture()
    {
        yield return new WaitForSeconds(0.1f);
        Cursor.SetCursor(SdefaultTexture, hotSpot, CursorMode.ForceSoftware);
    }

    void OnDestroy()
    {
        //texture??null?????????????J?[?\????????
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
    }

    void Attack()
    {
        Debug.Log("???{?^?????????????????B");
        Cursor.SetCursor(hitTexture, hotSpot, CursorMode.ForceSoftware);
        StartCoroutine("SetTexture");

        _MouseDown = true;
        TimerManager.Instance.mouseDown();

        Debug.Log("Click");
        audioSource.PlayOneShot(flyswatterSE, 0.2F);

        canAttack = false;
        Invoke("FinishInterval", hitInterval);
    }

    void StressAttack()
    {
        Debug.Log("???{?^?????????????????B");
        Cursor.SetCursor(ShitTexture, hotSpot, CursorMode.ForceSoftware);
        StartCoroutine("SetTexture");

        _MouseDown = true;
        TimerManager.Instance.mouseDown();

        Debug.Log("Click");
        audioSource.PlayOneShot(flyswatterSE, 0.2F);

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

    /// <summary>
    ///    stress?n?????Z
    /// </summary>
    /// <param name="value">
    /// ???Z??
    /// </param>
    public void AddStressValue(float value)
    {
        m_fStressValue += value;
    }

    public static int getScore() { return score; }

    public bool IsMouseDown
    {
        get { return _MouseDown; }
    }

    public bool IsStress
    {
        get { return m_bStress; }
    }
}
