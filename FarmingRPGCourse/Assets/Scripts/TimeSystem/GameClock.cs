using TMPro;
using UnityEngine;

public class GameClock : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI dateText;
    [SerializeField] TextMeshProUGUI seasonText;
    [SerializeField] TextMeshProUGUI yearText;

    private void OnEnable()
    {
        EventHandler.OnAdvanceGameMinuteEvent += OnAdvanceGameMinute_UpdateGameTime;
    }

    private void OnDisable()
    {
        EventHandler.OnAdvanceGameMinuteEvent -= OnAdvanceGameMinute_UpdateGameTime;
    }

    private void OnAdvanceGameMinute_UpdateGameTime(int gameYear, Season gameSeason, int gameDay, string gameDayOfWeek, int gameHour, int gameMinute, int gameSecond)
    {
        //Update time

        gameMinute = gameMinute - (gameMinute % 10);    //display in 10 minute intervals.

        string ampm = "";
        string minute;

        if (gameHour >= 12)
        {
            ampm = " pm";
        }
        else
        {
            ampm = " am";
        }

        if (gameHour >= 13)          //12 hr clock, but will say pm.
        {
            gameHour = gameHour -= 12;
        }

        if (gameMinute < 10)      //put 0 in front of single digit minutes.
        {
            minute = "0" + gameMinute.ToString();
        }
        else
        {
            minute = gameMinute.ToString();
        }

        string time = gameHour.ToString() + " : " + minute + ampm;   

        timeText.text = time;
        dateText.text = gameDayOfWeek + ". " + gameDay.ToString();
        seasonText.text = gameSeason.ToString();
        yearText.text = "Year " + gameYear.ToString();
    }
}
