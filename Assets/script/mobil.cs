using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class mobil : MonoBehaviour
{
    [SerializeField] private LevelSelectionManager _levelSelectionManager;
    [SerializeField] private TextMeshProUGUI _countdownText;
    public float speed = 3f;
    private Vector2 moveDirection = Vector2.up; // Arah gerak awal
    private bool isStopping = false;
    private bool isBelokKanan = false;
    private bool isBelokKiri = false;
    private bool isStarted = false; // <- Tambahan: belum mulai sampai tombol ditekan

    public string nextSceneKiri = "SceneKiri";
    public string nextSceneKanan = "SceneKanan";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isStarted && !isStopping)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);
        }
    }

    IEnumerator CountdownStart(float duration)
    {
        _countdownText.gameObject.SetActive(true);
        _countdownText.SetText(3.ToString());
        yield return new WaitForSeconds(duration);
        _countdownText.SetText(2.ToString());
        yield return new WaitForSeconds(duration);
        _countdownText.SetText(1.ToString());
        yield return new WaitForSeconds(duration);
        _countdownText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TurnpointKiri"))
        {
            _levelSelectionManager.InitializeBelokKiri();
            StartCoroutine(BerhentiDiBelokan());
        }

        if (other.CompareTag("TurnpointKanan"))
        {
            _levelSelectionManager.InitializeBelokKanan();
            StartCoroutine(BerhentiDiBelokan());
        }
    }

    IEnumerator BerhentiDiBelokan()
    {
        isStopping = true;

        _countdownText.gameObject.SetActive(true);
        _countdownText.SetText(10.ToString());
        yield return new WaitForSeconds(1);
        _countdownText.SetText(9.ToString());
        yield return new WaitForSeconds(1);
        _countdownText.SetText(8.ToString());
        yield return new WaitForSeconds(1);
        _countdownText.SetText(7.ToString());
        yield return new WaitForSeconds(1);
        _countdownText.SetText(6.ToString());
        yield return new WaitForSeconds(1);
        _countdownText.SetText(5.ToString());
        yield return new WaitForSeconds(1);
        _countdownText.SetText(4.ToString());
        yield return new WaitForSeconds(1);
        _countdownText.SetText(3.ToString());
        yield return new WaitForSeconds(1);
        _countdownText.SetText(2.ToString());
        yield return new WaitForSeconds(1);
        _countdownText.SetText(1.ToString());
        yield return new WaitForSeconds(1);
        _countdownText.gameObject.SetActive(false);

        if (!isBelokKanan)
        {
            StartCoroutine(CountdownStart(1));
            isStopping = false;
            isBelokKanan = true;
        }
        _levelSelectionManager.TurnOffRecordContainer();
    }

    //  Fungsi ini dipanggil dari tombol
    public void MulaiGame()
    {
        isStarted = true;
        StartCoroutine(CountdownStart(0.8f));
    }
    public void BelokKanan()
    {
        StartCoroutine(CountdownStart(3F));
        isBelokKanan = true;
        transform.rotation = Quaternion.Euler(0, 0, -90); // putar mobil ke kanan
        moveDirection = Vector2.right; // arah gerak ke kanan
        isStopping = false; // lanjut jalan
        _levelSelectionManager.TurnOffRecordContainer(); // langsung hilangkan UI
    }

    public void BelokKiri()
    {
        StartCoroutine(CountdownStart(1.8F));
        isBelokKiri = true;
        transform.rotation = Quaternion.Euler(0, 0, 90); // putar mobil ke kiri
        moveDirection = Vector2.left; // arah gerak ke kiri
        isStopping = false; // lanjut jalan
        _levelSelectionManager.TurnOffRecordContainer(); // langsung hilangkan UI
    }
}
