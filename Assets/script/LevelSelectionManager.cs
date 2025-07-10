using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelSelectionManager : MonoBehaviour
{
    [Header("Component Reference")]
    [SerializeField] private TextMeshProUGUI _containerText;
    [SerializeField] private GameObject _recordContainer;
    [SerializeField] private LevelSelectionSpeechManager _levelSelectionSpeechManager;
    [SerializeField] private mobil _mobil;

    public bool IsAnswering = false;


    public void InitializeBelokKiri()
    {
        IsAnswering = true;
        _containerText.SetText("Katakan \"belok kiri\" jika ingin memulai game");
        _recordContainer.SetActive(true);
        StartCoroutine(OnDelayTurnOnSpeechRecog());
    }

    public void InitializeBelokKanan()
    {
        IsAnswering = true;
        _containerText.SetText("Katakan \"belok kanan\" jika ingin memulai game");
        _recordContainer.SetActive(true);
        StartCoroutine(OnDelayTurnOnSpeechRecog());
    }

    IEnumerator OnDelayTurnOnSpeechRecog()
    {
        yield return new WaitForSeconds(0.5F);
        _levelSelectionSpeechManager.ButtonClick();

        IsAnswering = true;

    }

    public void TurnOffRecordContainer()
    {
        _recordContainer.SetActive(false);
    }

    public void OnBelokKiri()
    {
        Debug.Log("Mobil belok kiri");
        _mobil.BelokKiri();
    }

    public void OnBelokKanan()
    {
        
        Debug.Log("Mobil belok kanan");
        _mobil.BelokKanan();
    }
}
