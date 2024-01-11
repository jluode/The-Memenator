using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 0;
    private bool timeIsRunning;
    public TMP_Text timerText;
    [SerializeField] GameObject start;
    [SerializeField] GameObject finish;

    private void Start()
    {
        timeIsRunning = false;
    }

    private void Update()
    {
        if (timeIsRunning)
        {
            timeRemaining += Time.deltaTime;
            DisplayTime(timeRemaining);
        }
    }

    public void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1; // Adjust this value as needed
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliseconds = (timeToDisplay % 1) * 1000;

        timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == start)
        {
            StartTimer();
        }
        else if (other.gameObject == finish)
        {
            StopTimer();
        }
    }

    private void StartTimer()
    {
        timeIsRunning = true;
        StartCoroutine(UpdateTimer());
    }

    private void StopTimer()
    {
        timeIsRunning = false;
        StopCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (timeIsRunning)
        {
            yield return null;
            DisplayTime(timeRemaining);
        }
    }
}

