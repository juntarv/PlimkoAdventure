using System.Collections;
using UnityEngine;

public class FinishObject : MonoBehaviour
{
    public ParticleSystem winParticles; // ��������� ������ ������
    public AudioClip audioClip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // �������� ��������� PlayerStats ������� Player
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                if (audioClip != null)
                {
                    AudioSource.PlayClipAtPoint(audioClip, transform.position);
                }


                // �������� ����� ��� ��������� ������
                playerStats.WinLevel();
            }
        }
    }

}

