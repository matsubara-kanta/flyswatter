using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade
{
    public float FadeOut(Image image)
    {
        //  �t�F�[�h�̐F
        Vector4 color = image.color;
        float alpha = color.w;  //  �s�����x

        if (alpha > 0.0f)
        {
            alpha -= 1.0f * Time.deltaTime;
        }
        else
        {
            alpha = 0.0f;
        }

        color.w = alpha;
        image.color = color;
        return alpha;
    }

    public float FadeIn(Image image)
    {
        //  �t�F�[�h�̐F
        Vector4 color = image.color;
        float alpha = color.w;  //  �s�����x

        if (alpha < 1.0f)
        {
            alpha += 1.0f * Time.deltaTime;
        }
        else
        {
            alpha = 1.0f;
        }

        color.w = alpha;
        image.color = color;
        return alpha;
    }
}
