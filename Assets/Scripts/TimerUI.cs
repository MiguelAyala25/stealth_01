using UnityEngine;
using TMPro; 

public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI timerText; 

    private void Update()
    {
        if (GameManager.Instance != null)
        {
            //F2 number decimals shown
            timerText.text = GameManager.Instance.time.ToString("F2");
        }
    }
}