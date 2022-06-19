using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image BlackImage;
    [SerializeField] private float ImageAlpha;


    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo()
    {
        StartCoroutine(FadeOut());

    }

    IEnumerator FadeIn()
    {
        ImageAlpha = 1;

        while (ImageAlpha>0)
        {
            ImageAlpha -= Time.fixedDeltaTime;
            BlackImage.color = new Color(0, 0, 0, ImageAlpha );
            yield return new WaitForSeconds(0);
        }
    }

    IEnumerator FadeOut()
    {
        ImageAlpha = 0;

        while (ImageAlpha < 0)
        {
            ImageAlpha += Time.fixedDeltaTime;
            BlackImage.color = new Color(0, 0, 0, ImageAlpha);
            yield return new WaitForSeconds(0);//yield return null
        }

        SceneManager.LoadScene("02");
    }

    
}
