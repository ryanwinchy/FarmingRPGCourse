using UnityEngine;

public class TimeManager : SingletonMonobehaviour<TimeManager>    //Singleton pattern.
{
    private int gameYear = 1;
    private Season gameSeason = Season.Spring;
    private int gameDay = 1;
    private int gameHour = 6;
    private int gameMinute = 30;
    private int gameSecond = 0;
    private string gameDayOfWeek = "Mon";

    bool gameClockPaused = false;

    float gameTick = 0f;    //frame rate independent.


    void Start()
    {
        EventHandler.CallAdvanceGameMinuteEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
    }

    void Update()
    {
        if (!gameClockPaused)
        {
            gameTick += Time.deltaTime;

            if (gameTick >= Settings.SECONDS_PER_GAME_SECOND)
            {
                gameTick -= Settings.SECONDS_PER_GAME_SECOND;

                UpdateGameSecond();

            }
        }



    }


    void UpdateGameSecond()
    {
        gameSecond++;

        if (gameSecond > 59)
        {
            gameSecond = 0;
            gameMinute++;

            if (gameMinute > 59)
            {
                gameMinute = 0;
                gameHour++;

                if (gameHour > 23)
                {
                    gameHour = 0;
                    gameDay++;

                    if (gameDay > 30)
                    {
                        gameDay = 1;

                        int gameSeasonIndex = (int)gameSeason;
                        gameSeasonIndex++;

                        gameSeason = (Season)gameSeasonIndex;

                        if (gameSeasonIndex > 3) //past fourth season of winter (lists start at 0).
                        {
                            gameSeasonIndex = 0;
                            gameSeason = (Season)gameSeasonIndex;

                            gameYear++;

                            if (gameYear > 9999)     //as our UI only supports 4 digits.
                                gameYear = 1;

                            EventHandler.CallAdvanceGameYearEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
                        }

                        EventHandler.CallAdvanceGameSeasonEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
                    }
                    gameDayOfWeek = GetDayOfWeek();

                    EventHandler.CallAdvanceGameDayEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
                }

                EventHandler.CallAdvanceGameHourEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
            }

            EventHandler.CallAdvanceGameMinuteEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);

        }
    }



    private string GetDayOfWeek()
    {
        int totalDays = (int)gameSeason * 30 + gameDay;      //total days since year start. as each season day restarts to 0.
        int dayOfWeek = totalDays % 7;      //divide total by 7, gives us the remainder. This gives us sequence of 7 day weeks. When gets to 8, loops back to 1 basically.

        switch (dayOfWeek)
        {
            case 1:
                return "Mon";
            case 2:
                return "Tue";
            case 3:
                return "Wed";
            case 4:
                return "Thu";
            case 5:
                return "Fri";
            case 6:
                return "Sat";
            case 0:
                return "Sun";
            default:
                return "";
        }
    }



    /// <summary>
    /// Debug control: Advance game time by 1 minute.
    /// </summary>
    /// <returns>A string representing the current game time.</returns>
    public void TestAdvanceGameMinute()
    {
        for (int i = 0; i < 60; i++)
        {
            UpdateGameSecond();      //as we want whole seconds to advance, and all the events to properly fire.
        }
    }


    /// <summary>
    /// Debug control: Advance game time by 1 hour.
    /// </summary>
    /// <returns>A string representing the current game time.</returns>
    public void TestAdvanceGameDay()
    {
        for (int i = 0; i < 86400; i++)
        {
            UpdateGameSecond();
        }
    }


}