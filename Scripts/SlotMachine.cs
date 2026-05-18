
using UnityEngine;
using TMPro;
using System.Collections;

public class SlotMachine : MonoBehaviour
{
    public Reel reel1;
    public Reel reel2;
    public Reel reel3;

    public TMP_Text coinsText;
    public TMP_Text resultText;

    private int coins = 100;

    public Lever lever;

    private bool isSpinning = false;

    public AudioSource audioSource;

    public AudioClip spinSound;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip leverSound;

    public void SpinAll()
    {
        if (isSpinning)
            return;

        isSpinning = true;

        StartCoroutine(SpinReels());
    }

    IEnumerator SpinReels()
    {
        

        resultText.text = "";
        StartCoroutine(lever.PullLever());

        // LEVER SOUND
        audioSource.PlayOneShot(leverSound);

        // SPIN SOUND
        audioSource.clip = spinSound;
        audioSource.loop = true;
        audioSource.Play();

        // START ALL REELS TOGETHER
        reel1.StartSpin();
        reel2.StartSpin();
        reel3.StartSpin();

        // STOP ONE BY ONE
        yield return StartCoroutine(reel1.StopSpin(1f));

        yield return StartCoroutine(reel2.StopSpin(1.5f));

        yield return StartCoroutine(reel3.StopSpin(2f));

        // STOP SPIN SOUND
        audioSource.Stop();

        CheckWin();

       

        isSpinning = false;
    }

    void CheckWin()
    {
        if (reel1.reelImage.sprite ==
            reel2.reelImage.sprite &&
            reel2.reelImage.sprite ==
            reel3.reelImage.sprite)
        {
            Sprite winningSprite = reel1.reelImage.sprite;

            if (winningSprite == reel1.symbols[0])
            {
                resultText.text = "777 JACKPOT! +100";
                coins += 100;
            }
            else if (winningSprite == reel1.symbols[1])
            {
                resultText.text = "CHERRY WIN! +50";
                coins += 50;
            }
            else if (winningSprite == reel1.symbols[2])
            {
                resultText.text = "BELL WIN! +25";
                coins += 25;
            }
            else
            {
                resultText.text = "BAR WIN! +10";
                coins += 10;
            }

            audioSource.PlayOneShot(winSound);
        }
        else
        {
            resultText.text = "TRY AGAIN";
            coins -= 5;

            audioSource.PlayOneShot(loseSound);
        }

        coinsText.text = "Coins: " + coins;
    }
}