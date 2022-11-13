using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    [SerializeField] private float coolTime = 10.0f;
    [SerializeField] private float effectTime = 2.0f;
    [SerializeField] private bool canUseSkill;
    [SerializeField] private float timeScale = 0.1f;

    public AudioClip spraySE;
    AudioSource audioSource;

    private void Start()
    {
        canUseSkill = true;
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (canUseSkill)
            {
                UseSkill();
            }
        }
    }

    public void UseSkill()
    {
        /*
        GameObject[] flys = GameObject.FindGameObjectsWithTag("Fly");
        foreach (GameObject fly in flys)
        {
            FlyBehaviour flyBehaviour = fly.GetComponent<FlyBehaviour>();
            Debug.Log(flyBehaviour);

            Destroy(fly);
        }*/

        //Time.timeScale = timeScale;
        audioSource.PlayOneShot(spraySE, 0.2F);
        canUseSkill = false;

        var instance = TimerManager.Instance;

        instance.requestSkill(timeScale);

        Invoke("FinishCoolTime", coolTime);
        Invoke("FinishEffectTime", effectTime);
    }

    public void FinishCoolTime()
    {
        canUseSkill = true;


    }

    public void FinishEffectTime()
    {
        var instance = TimerManager.Instance;

        instance.finishSkill();

        //Time.timeScale = 1.0f;
    }

    public bool getCanUseSkill()
    {
        return canUseSkill;
    }

    public float getCoolTime()
    {
        return coolTime;
    }
}
