using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Lever : MonoBehaviour
{
    public Sprite leverUp;
    public Sprite leverDown;

    private Image leverImage;
    private Vector3 startPos;

    void Start()
    {
        leverImage = GetComponent<Image>();
        startPos = transform.localPosition;
    }

    public void LeverDown()
    {
        leverImage.sprite = leverDown;

        transform.localPosition = startPos + new Vector3(0, -40, 0);
    }

    public void LeverUp()
    {
        leverImage.sprite = leverUp;

        transform.localPosition = startPos;
    }

    public IEnumerator PullLever()
    {
        LeverDown();

        yield return new WaitForSeconds(0.05f);

        LeverUp();
    }
}