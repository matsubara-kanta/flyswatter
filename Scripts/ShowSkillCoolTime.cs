using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSkillCoolTime : MonoBehaviour
{
    [SerializeField] SkillController skillController;
    [SerializeField] Text coolTimeText;
    [SerializeField] GameObject canNotUseImage;
    float coolTime;
    float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        coolTime = skillController.getCoolTime();
        canNotUseImage.SetActive(false);
        coolTimeText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (!skillController.getCanUseSkill())
        {
            canNotUseImage.SetActive(true);
            elapsedTime += Time.deltaTime;
            coolTimeText.text = (coolTime - elapsedTime).ToString("f1");
        }
        else
        {
            coolTime = skillController.getCoolTime();
            canNotUseImage.SetActive(false);
            coolTimeText.text = "";
            elapsedTime = 0.0f;
        }
    }
}
