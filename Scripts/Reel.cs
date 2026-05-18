using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Reel : MonoBehaviour
{
    private bool isSpinning = false;

    public Sprite[] symbols;
    public Image reelImage;

    public RectTransform topSymbol;
    public RectTransform bottomSymbol;

    public IEnumerator Spin()
    {
        while (isSpinning)
        {
            int randomIndex = Random.Range(0, symbols.Length);

            reelImage.sprite = symbols[randomIndex];

            topSymbol.localPosition += new Vector3(0, -2, 0);
            bottomSymbol.localPosition += new Vector3(0, 2, 0);

            yield return new WaitForSeconds(0.05f);

            topSymbol.localPosition += new Vector3(0, 2, 0);
            bottomSymbol.localPosition += new Vector3(0, -2, 0);

            yield return new WaitForSeconds(0.05f);
        }
    }

    public void StartSpin()
    {
        isSpinning = true;
        StartCoroutine(Spin());
    }

    public IEnumerator StopSpin(float delay)
    {
        yield return new WaitForSeconds(delay);

        isSpinning = false;

        int randomIndex = Random.Range(0, symbols.Length);
        reelImage.sprite = symbols[randomIndex];
    }
}