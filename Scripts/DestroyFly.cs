using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DestroyFly : MonoBehaviour
{
    public AudioClip flyswatterSE;
    AudioSource audioSource;
    // Vector3 mousePos, worldPos;
    public static int score;
    //private int highScore;
    public GameObject score_object = null;
    //public GameObject  highscore_object = null;



    private bool _MouseDown = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        score = 0;
    }

    void Update()
    {
        /*
        mousePos = Input.mousePosition;
        mousePos.z = 10f;
        worldPos = Camera.main.ScreenToWorldPoint(mousePos);
       this.transform.position = worldPos;
        Debug.Log(transform.position.x + " , " + transform.position.y);
        */
        if (Input.GetMouseButtonDown(0))
        {
            _MouseDown = true;
            Debug.Log("Click");
            audioSource.PlayOneShot(flyswatterSE);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _MouseDown = false;
        }

        if (score_object != null)
        {
            Text score_text = score_object.GetComponent<Text>();
            score_text.text = "SCORE: " + score;
        }

        //Text highscore_text = highscore_object.GetComponent<Text>();
        //highscore_text.text = "HIGHSCORE:" + score;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fly")
        {
            Debug.Log("mult");
            if (_MouseDown)
            {

                Debug.Log("Hit");
                Destroy(collision.gameObject);
                score++;
            }
        }
    }

    public static int getScore() { return score; }


}
