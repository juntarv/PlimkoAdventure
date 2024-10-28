using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int pointsPerBounce = 10;
    private bool alreadyScored = false; // Переменная для отслеживания начисления очков за столкновение
    public AudioClip audioClip;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !alreadyScored)
        {
            // Получаем компонент PlayerStats объекта Player
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                // Вызываем метод для начисления очков
                playerStats.IncrementScore(pointsPerBounce);
                alreadyScored = true; // Помечаем, что очки уже начислены за это столкновение
                if (audioClip != null)
                {
                    AudioSource.PlayClipAtPoint(audioClip, transform.position);
                }
            }
        }
    }

    // Сброс alreadyScored при разлете шара игрока
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            alreadyScored = false;
        }
    }
}

