using System.Collections;
using UnityEngine;

public class FinishObject : MonoBehaviour
{
    public ParticleSystem winParticles; // Публичный объект частиц
    public AudioClip audioClip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Получаем компонент PlayerStats объекта Player
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                if (audioClip != null)
                {
                    AudioSource.PlayClipAtPoint(audioClip, transform.position);
                }


                // Вызываем метод для обработки победы
                playerStats.WinLevel();
            }
        }
    }

}

