using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private float fxDuration = .5f;
    public Animator transition;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (sceneName!=null)
            {
                transition.SetTrigger("Start");
                Invoke(nameof(loadLvl), fxDuration);
            }
        }
    }
    private void loadLvl()
    {
        SceneManager.LoadScene(sceneName);
    }
}
