using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Button soundButton; // Ses açma/kapama düğmesi
    public Button musicButton; // Müzik açma/kapama düğmesi
    public Sprite soundOnSprite; // Ses açıkken gösterilecek ikon
    public Sprite soundOffSprite; // Ses kapalıyken gösterilecek ikon
    public Sprite musicOnSprite; // Müzik açıkken gösterilecek ikon
    public Sprite musicOffSprite; // Müzik kapalıyken gösterilecek ikon

    private bool soundOn = true; // Başlangıçta ses açık olsun
    private bool musicOn = true; // Başlangıçta müzik açık olsun

    void Start()
    {
        // Başlangıçta ses durumuna göre düğme ikonunu ayarla
        UpdateButtonIcon(soundButton, soundOn ? soundOnSprite : soundOffSprite);
        // Başlangıçta müzik durumuna göre düğme ikonunu ayarla
        UpdateButtonIcon(musicButton, musicOn ? musicOnSprite : musicOffSprite);

        // Düğmelerin tıklama olaylarına abone ol
        soundButton.onClick.AddListener(ToggleSound);
        musicButton.onClick.AddListener(ToggleMusic);
    }

    // Ses durumunu tersine çevir
    void ToggleSound()
    {
        soundOn = !soundOn;
        UpdateButtonIcon(soundButton, soundOn ? soundOnSprite : soundOffSprite);

        // Ses kaynağını bul
        AudioSource soundSource = GetComponentInChildren<AudioSource>();

        // Ses açık ise, sesi etkinleştir, değilse devre dışı bırak
        if (soundSource != null)
        {
            soundSource.enabled = soundOn;
        }
        else
        {
            Debug.LogError("Ses kaynağı bulunamadı!");
        }
    }

    // Müzik durumunu tersine çevir
    void ToggleMusic()
    {
        musicOn = !musicOn;
        UpdateButtonIcon(musicButton, musicOn ? musicOnSprite : musicOffSprite);

        // Müzik kaynağını bul
        AudioSource musicSource = GameObject.FindWithTag("Music").GetComponent<AudioSource>();

        // Müzik açık ise, müziği etkinleştir, değilse devre dışı bırak
        if (musicSource != null)
        {
            musicSource.enabled = musicOn;
        }
        else
        {
            Debug.LogError("Müzik kaynağı bulunamadı!");
        }
    }

    // Düğme ikonunu güncelle
    void UpdateButtonIcon(Button button, Sprite sprite)
    {
        // Düğme ikonunu duruma göre ayarla
        Image buttonImage = button.GetComponent<Image>();
        buttonImage.sprite = sprite;
    }
}
