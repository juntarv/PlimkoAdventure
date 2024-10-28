using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayScene());
    }

    private IEnumerator DelayScene()
    {
        yield return 2f;
        SceneManager.LoadScene("First Menu");
    }
}
