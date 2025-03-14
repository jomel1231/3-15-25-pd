using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Assign your VideoPlayer
    public Slider progressBar;       // Assign your Slider
    public TextMeshProUGUI  timeStamp;           // Assign your Text for the time display

    private bool isDragging = false;

    void Start()
    {
        // Ensure slider max value matches video length when video is prepared
        videoPlayer.prepareCompleted += (vp) =>
        {
            progressBar.maxValue = (float)vp.length;
        };

        // Start updating progress bar in real-time
        videoPlayer.Play();
    }

    void Update()
    {
        if (!isDragging)
        {
            progressBar.value = (float)videoPlayer.time;
        }

        UpdateTimeStamp();
    }

    public void OnSliderValueChanged(float value)
    {
        isDragging = true;
    }

    public void OnSliderDragEnd()
    {
        videoPlayer.time = progressBar.value;
        isDragging = false;
    }

    private void UpdateTimeStamp()
    {
        int minutes = Mathf.FloorToInt((float)videoPlayer.time / 60);
        int seconds = Mathf.FloorToInt((float)videoPlayer.time % 60);
        int totalMinutes = Mathf.FloorToInt((float)videoPlayer.length / 60);
        int totalSeconds = Mathf.FloorToInt((float)videoPlayer.length % 60);
        timeStamp.text = $"{minutes:00}:{seconds:00} / {totalMinutes:00}:{totalSeconds:00}";
    }
}
