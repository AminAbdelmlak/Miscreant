using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject Tutorial;
    private Animator animator;

    void Start()
    {
        animator = Tutorial.GetComponent<Animator>();

        if (Tutorial == null)
        {
            return;
        }
    }

    public void Play()
    {
        animator.SetTrigger("Appear");
        animator.SetBool("Idle", true);
    }

    public void Yes()
    {
        SceneManager.LoadScene(1);
    }

    public void No()
    {
        SceneManager.LoadScene(2);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(2);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
