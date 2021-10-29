using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [Header("Respawns")]
    public GameObject ball;

    [Header("Flippers")]
    public float flipForce;
    public float flipGravity;

    public List<Collider2D> leftFlips = new List<Collider2D>();
    public List<Collider2D> rightFlips = new List<Collider2D>();

    [Header("UI")]
    public GameObject pauseScreen;
    public GameObject optionScreen;
    public Image optionImage;
    public GameObject startScreen;

    public float pauseAlpha;

    private bool paused;

    private void Start()
    {
        Time.timeScale = 0f;
    }

    void Update()
    {
        if (Input.GetButton("FlipLeft"))
        {
            Flip(1, leftFlips);
        }
        
        if (Input.GetButton("FlipRight"))
        {
            Flip(-1, rightFlips);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Quit();
            }
            else
            {
                if (paused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }

    public void StartFunc()
    {
        Time.timeScale = 1;
    }


    void Flip(int forceMult, List<Collider2D> results)
    {
        foreach (Collider2D col in results)
        {
            col.attachedRigidbody.AddTorque(forceMult * flipForce);
        }
    }
    

    public void Pause()
    {
        Time.timeScale = 0f;
        
        pauseScreen.SetActive(true);
        
        paused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;

        pauseScreen.SetActive(false);
        optionScreen.SetActive(false);

        paused = false;
    }

    public void OptionColour()
    {
        if (paused)
        {
            optionImage.color = new Color(0, 0, 0, pauseAlpha/255f);
        }
        else
        {
            optionImage.color = new Color(0, 0, 0, 1);
        }
    }

    public void BackFromOption()
    {
        if (paused)
        {
            pauseScreen.SetActive(true);
        }
        else
        {
            startScreen.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
