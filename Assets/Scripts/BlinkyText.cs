using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlinkyText : MonoBehaviour
{
    public TextMeshProUGUI text;
    
    void Start()
    {
        StartBlinking();
    }

    private IEnumerator Blink()
    {
        while (text)
        {
            switch (text.color.a.ToString())
            {
                case "0":
                    text.color = new Color(text.color.r, text.color.b, text.color.g, 1);
                    yield return new WaitForSeconds(0.5f);
                    break;
                case "1":
                    text.color = new Color(text.color.r, text.color.b, text.color.g, 0);
                    yield return new WaitForSeconds(0.4f);
                    break;
            }
        }
    }

    void StartBlinking()
    {
        StopCoroutine("Blink");
        StartCoroutine("Blink");
    }
}
