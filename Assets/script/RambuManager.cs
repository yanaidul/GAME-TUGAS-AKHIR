using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RambuManager : Singleton<RambuManager>
{
    [SerializeField] private List<Sprite> _rambuSpriteList = new List<Sprite>();
    [SerializeField] private SpriteRenderer _rambu;
    [SerializeField] private Animator _rambuAnimator;
    [SerializeField] private TextMeshProUGUI _countdownText;

    private void Start()
    {
        _rambu.gameObject.SetActive(false);
        _rambuAnimator.Play("Idle");

        StartCoroutine(RambuAppearDelay()); //For Test only
    }

    public void ChangeRambuSpriteByIndex(int index)
    {
        if (index >= 0 && index < _rambuSpriteList.Count)
        {
            _rambu.sprite = _rambuSpriteList[index];
        }
        else
        {
            Debug.LogError($"[RambuManager] Index sprite rambu ({index}) di luar batas! Total: {_rambuSpriteList.Count}");
        }
    }

    public void OnRambuStartAppear()
    {
        _rambu.gameObject.SetActive(true);
        _rambuAnimator.Play("Goes To Car");
        StartCoroutine(RoadAnimationStopDelay());
    }

    public void OnRambuWalkPast()
    {
        _rambuAnimator.Play("Go Past The Car");
        RoadManager.GetInstance().OnPlayRoadAnimation();
    }

    IEnumerator RoadAnimationStopDelay()
    {
        yield return new WaitForSeconds(0.2f);
        RambuQuestionManager.GetInstance().InitializeQuestion();
        RoadManager.GetInstance().OnStopRoadAnimation();
    }

    public void OnCorrectAnswer()
    {
        if (RambuQuestionManager.GetInstance().IsLastQuestion())
        {
            Debug.Log("Tidak ada rambu lagi, game selesai.");
            GameManager1.GetInstance().ShowEndGame(); // tampilkan panel akhir
            return; // JANGAN LANJUT KE RAMBU BERIKUTNYA
        }

        OnRambuWalkPast();
        StartCoroutine(RambuAppearDelay());
    }

    IEnumerator RambuAppearDelay()
    {
        _countdownText.gameObject.SetActive(true);
        _countdownText.SetText(3.ToString());
        yield return new WaitForSeconds(1);
        _countdownText.SetText(2.ToString());
        yield return new WaitForSeconds(1);
        _countdownText.SetText(1.ToString());
        yield return new WaitForSeconds(1);
        _countdownText.gameObject.SetActive(false);
        OnRambuStartAppear();
    }
}
