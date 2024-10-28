using System.Collections;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public ParticleSystem destructionParticles; // Объект частиц для воспроизведения при уничтожении игрока
    public Transform playerStartPosition; // Позиция для возрождения игрока
    private Vector3 initialPlayerPosition; // Исходная позиция игрока
    public AudioClip audioClip;
    private ParticleSystem instantiatedParticles; // Ссылка на созданный экземпляр частиц

    private void Start()
    {
        // Сохраняем исходную позицию игрока при запуске игры
        initialPlayerPosition = playerStartPosition.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, является ли объект, с которым произошло столкновение, игроком
        if (collision.gameObject.CompareTag("Player"))
        {
            // Запускаем корутину для перемещения игрока
            StartCoroutine(MovePlayerAfterParticles(collision.transform, initialPlayerPosition, destructionParticles));

            // Уменьшаем количество жизней игрока
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.DecrementLife();
            }

            // Отключаем Rigidbody игрока до следующего свайпа
            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                playerRigidbody.velocity = Vector2.zero;
                playerRigidbody.angularVelocity = 0f;
                playerRigidbody.bodyType = RigidbodyType2D.Static;
            }
        }
    }
    IEnumerator MovePlayerAfterParticles(Transform collisionTransform, Vector3 initialPlayerPosition, ParticleSystem destructionParticles)
    {
        // Запускаем частицы
        if (destructionParticles != null)
        {
            // Создаем частицы с учетом координаты Z
            var instantiatedParticles = Instantiate(destructionParticles, collisionTransform.position, Quaternion.identity);
            if (audioClip != null)
            {
                AudioSource.PlayClipAtPoint(audioClip, transform.position);
            }
            // Задаем координату Z
            var particlePosition = instantiatedParticles.transform.position;
            particlePosition.z = 76f;
            particlePosition.y = -45f;// Здесь вы можете задать желаемую координату Z
            instantiatedParticles.transform.position = particlePosition;

            instantiatedParticles.Play();

            // Ждем завершения частиц
            yield return new WaitForSeconds(instantiatedParticles.main.duration);
        }
        else
        {
            // Ждем 1 секунду
            yield return new WaitForSeconds(1f);
        }

        // Перемещаем игрока на исходную позицию
        collisionTransform.position = initialPlayerPosition;
    }


}




