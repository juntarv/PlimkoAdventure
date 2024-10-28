using System.Collections;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public ParticleSystem destructionParticles; // ������ ������ ��� ��������������� ��� ����������� ������
    public Transform playerStartPosition; // ������� ��� ����������� ������
    private Vector3 initialPlayerPosition; // �������� ������� ������
    public AudioClip audioClip;
    private ParticleSystem instantiatedParticles; // ������ �� ��������� ��������� ������

    private void Start()
    {
        // ��������� �������� ������� ������ ��� ������� ����
        initialPlayerPosition = playerStartPosition.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���������, �������� �� ������, � ������� ��������� ������������, �������
        if (collision.gameObject.CompareTag("Player"))
        {
            // ��������� �������� ��� ����������� ������
            StartCoroutine(MovePlayerAfterParticles(collision.transform, initialPlayerPosition, destructionParticles));

            // ��������� ���������� ������ ������
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.DecrementLife();
            }

            // ��������� Rigidbody ������ �� ���������� ������
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
        // ��������� �������
        if (destructionParticles != null)
        {
            // ������� ������� � ������ ���������� Z
            var instantiatedParticles = Instantiate(destructionParticles, collisionTransform.position, Quaternion.identity);
            if (audioClip != null)
            {
                AudioSource.PlayClipAtPoint(audioClip, transform.position);
            }
            // ������ ���������� Z
            var particlePosition = instantiatedParticles.transform.position;
            particlePosition.z = 76f;
            particlePosition.y = -45f;// ����� �� ������ ������ �������� ���������� Z
            instantiatedParticles.transform.position = particlePosition;

            instantiatedParticles.Play();

            // ���� ���������� ������
            yield return new WaitForSeconds(instantiatedParticles.main.duration);
        }
        else
        {
            // ���� 1 �������
            yield return new WaitForSeconds(1f);
        }

        // ���������� ������ �� �������� �������
        collisionTransform.position = initialPlayerPosition;
    }


}




