using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script starts the music when the player enters the trigger and stops it when the player exits the trigger
public class MusicTrigger : MonoBehaviour
{
    public AudioSource musicSource;  // Перетащи сюда аудиофайл из инспектора

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что это игрок вошел в зону
        if (other.CompareTag("Player"))
        {
            musicSource.Play();  // Включаем музыку
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Останавливаем музыку, когда игрок выходит из зоны
        if (other.CompareTag("Player"))
        {
            musicSource.Stop();
        }
    }
}


