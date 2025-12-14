
public class SettingsMenu : MonoBehaviour
{
    //public Slider volumeSlider; // Слайдер громкости
   // public AudioSource audioSource; // Источник звука в игре

    //private void Start()
    //{
        // Загружаем сохраненную громкость (по умолчанию 1.0)
       // float savedVolume = PlayerPrefs.GetFloat("Volume", 1.0f);
       // audioSource.volume = savedVolume;
       // volumeSlider.value = savedVolume;
   // }

   // public void SetVolume(float volume)
  //  {
      //  audioSource.volume = volume;
       // PlayerPrefs.SetFloat("Volume", volume); // Сохраняем громкость
  //  }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("menu");
    }
}

