using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsPlayerRunning { get; set; }

    public float time;
    public float initialTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        time = initialTime;
    }

    private void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            time = initialTime;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}