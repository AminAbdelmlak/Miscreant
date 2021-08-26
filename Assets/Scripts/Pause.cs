using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject Buttons;
    private Animator animator;
    private bool Paused = false;

    public GameObject Head;
    // Start is called before the first frame update
    void Start()
    {
        animator = Buttons.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Paused == false)
        {
            Paused = true;
            pause();
            PauseMenu.SetActive(true);
            Time.timeScale = 0;

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Paused == true)
        {
            Paused = false;
            Unpause();
            PauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
        Head = GameObject.FindGameObjectWithTag("Head");
        if (Head == null)
        {
            return;
        }


        if (Head.GetComponent<Win>().Won == true)
        {
            SceneManager.LoadScene(3);
        }
    }

    void pause()
    {
        animator.SetTrigger("Appear");
        animator.SetBool("Idle", true);
    }
    void Unpause()
    {
        animator.SetTrigger("Dissappear");
        animator.SetBool("Hidden", true);
    }
    public void Resume()
    {
        Unpause();
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        Paused = false;
    }

    public void Retry()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        Paused = false;
        SceneManager.LoadScene(2);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
