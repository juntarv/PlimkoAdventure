using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int pointsPerBounce = 10;
    private bool alreadyScored = false; // ���������� ��� ������������ ���������� ����� �� ������������
    public AudioClip audioClip;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !alreadyScored)
        {
            // �������� ��������� PlayerStats ������� Player
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                // �������� ����� ��� ���������� �����
                playerStats.IncrementScore(pointsPerBounce);
                alreadyScored = true; // ��������, ��� ���� ��� ��������� �� ��� ������������
                if (audioClip != null)
                {
                    AudioSource.PlayClipAtPoint(audioClip, transform.position);
                }
            }
        }
    }

    // ����� alreadyScored ��� ������� ���� ������
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            alreadyScored = false;
        }
    }
}

