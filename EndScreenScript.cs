using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class EndScreenScript : MonoBehaviour
{
    //GameOver Screen Info
    [Header("GameOver Screen Info")]
    [SerializeField] GameObject GameOverScreen;
    [SerializeField] private TextMeshProUGUI LGPText;
    string longestPlayedGame;
    float timeInLPG;

    //Stats Screen Info
    [Header("Stats Screen Info")]
    [SerializeField] GameObject StatsScreen;
    [SerializeField] private TextMeshProUGUI HideSeekStatsText;
    [SerializeField] private TextMeshProUGUI PlatformingStatsText;
    [SerializeField] private TextMeshProUGUI SimonSaysStatsText;
    [SerializeField] private TextMeshProUGUI DrawingStatsText;
    [SerializeField] private TextMeshProUGUI OtherStatsText;

    private Tuple<int, float, float> HideSeekStats;
    private Tuple<int, float, float> PlatformingStats;
    private Tuple<int, float, float> SimonSaysStats;
    private Tuple<int, float, float> DrawingStats;
    private Tuple<int, int, float> OtherStats;

    //Credits Screen Info
    [Header("Credits Screen Info")]
    [SerializeField] GameObject CreditsScreen;

    // Start is called before the first frame update
    void Start()
    {
        GetLGPandTime();

        HideSeekStats = new Tuple<int, float, float>(StaticVar.numOfHideSeekGames, StaticVar.HighScoreInHideSeek, StaticVar.TimeInHideSeek);
        PlatformingStats = new Tuple<int, float, float>(StaticVar.numOfPlatformingGames, StaticVar.HighScoreInPlatforming, StaticVar.TimeInPlatforming);
        SimonSaysStats = new Tuple<int, float, float>(StaticVar.numOfSimonSaysGames, StaticVar.HighScoreInSimonSays, StaticVar.TimeInSimonSays);
        DrawingStats = new Tuple<int, float, float>(StaticVar.numOfDrawingGames, StaticVar.HighScoreInDrawing, StaticVar.TimeInDrawing);
        OtherStats = new Tuple<int, int, float>(StaticVar.numOfRandomGames, StaticVar.numOfExits, StaticVar.TimeInMainMenu);

        SetStatsToText();

        GoToGameOverScreen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetLGPandTime()
    {
        longestPlayedGame = "";
        timeInLPG = 0;

        if (StaticVar.TimeInHideSeek > timeInLPG)
        {
            longestPlayedGame = "Hide&Seek";
            timeInLPG = StaticVar.TimeInHideSeek;
        }

        if (StaticVar.TimeInPlatforming > timeInLPG)
        {
            longestPlayedGame = "Platforming";
            timeInLPG = StaticVar.TimeInPlatforming;
        }

        if (StaticVar.TimeInSimonSays > timeInLPG)
        {
            longestPlayedGame = "Simon Says";
            timeInLPG = StaticVar.TimeInSimonSays;
        }

        if (StaticVar.TimeInDrawing > timeInLPG)
        {
            longestPlayedGame = "Drawing";
            timeInLPG = StaticVar.TimeInDrawing;
        }

        if (StaticVar.TimeInMainMenu > timeInLPG)
        {
            longestPlayedGame = "Main Menu";
            timeInLPG = StaticVar.TimeInMainMenu;
        }
    }

    void SetStatsToText()
    {
        float LGPminutes = TotalTimeToMinutesOrSeconds(timeInLPG, true);
        float LGPseconds = TotalTimeToMinutesOrSeconds(timeInLPG, false);
        LGPText.text = string.Format("{0}\n{1:0} minutes & {2:0} seconds", longestPlayedGame, LGPminutes, LGPseconds);

        float HaSBTminutes = TotalTimeToMinutesOrSeconds(HideSeekStats.Item2, true);
        float HaSBTseconds = TotalTimeToMinutesOrSeconds(HideSeekStats.Item2, false);
        float HaSTTminutes = TotalTimeToMinutesOrSeconds(HideSeekStats.Item3, true);
        float HaSTTseconds = TotalTimeToMinutesOrSeconds(HideSeekStats.Item3, false);
        HideSeekStatsText.text = string.Format("Games: {0}\nBest Time: {1:00}:{2:00}\nTotal Time: {3:00}:{4:00}",
                                                HideSeekStats.Item1, HaSBTminutes, HaSBTseconds, HaSTTminutes, HaSTTseconds);

        float PBTminutes = TotalTimeToMinutesOrSeconds(PlatformingStats.Item2, true);
        float PBTseconds = TotalTimeToMinutesOrSeconds(PlatformingStats.Item2, false);
        float PTTminutes = TotalTimeToMinutesOrSeconds(PlatformingStats.Item3, true);
        float PTTseconds = TotalTimeToMinutesOrSeconds(PlatformingStats.Item3, false);
        PlatformingStatsText.text = string.Format("Games: {0}\nBest Time: {1:00}:{2:00}\nTotal Time: {3:00}:{4:00}",
                                                PlatformingStats.Item1, PBTminutes, PBTseconds, PTTminutes, PTTseconds);

        float SSTTminutes = TotalTimeToMinutesOrSeconds(SimonSaysStats.Item3, true);
        float SSTTseconds = TotalTimeToMinutesOrSeconds(SimonSaysStats.Item3, false);
        SimonSaysStatsText.text = string.Format("Games: {0}\nHighScore: {1}\nTotal Time: {2:00}:{3:00}", 
                                                SimonSaysStats.Item1, SimonSaysStats.Item2, SSTTminutes, SSTTseconds);

        float DTTminutes = TotalTimeToMinutesOrSeconds(DrawingStats.Item3, true);
        float DTTseconds = TotalTimeToMinutesOrSeconds(DrawingStats.Item3, false);
        DrawingStatsText.text = string.Format("Games: {0}\nPrompts: {1}\nTotal Time: {2:00}:{3:00}", 
                                                DrawingStats.Item1, DrawingStats.Item2, DTTminutes, DTTseconds);


        float OTTminutes = TotalTimeToMinutesOrSeconds(OtherStats.Item3, true);
        float OTTseconds = TotalTimeToMinutesOrSeconds(OtherStats.Item3, false);
        OtherStatsText.text = string.Format("Random Games: {0}\nExits: {1}\nTime in Main Menu: {2:00}:{3:00}", 
                                                OtherStats.Item1, OtherStats.Item2, OTTminutes, OTTseconds);
    }

    float TotalTimeToMinutesOrSeconds(float time, bool min)
    {
        if (time > (15 * 60))
        { time = 15 * 60; }


        if (min)
        { return Mathf.FloorToInt(time / 60);}

        else
        { return Mathf.FloorToInt(time % 60);}
        
    }


    public void GoToGameOverScreen()
    {
        GameOverScreen.SetActive(true);
        StatsScreen.SetActive(false);
        CreditsScreen.SetActive(false);
    }

    public void GoToStatsScreen()
    {
        GameOverScreen.SetActive(false);
        StatsScreen.SetActive(true);
        CreditsScreen.SetActive(false);
    }

    public void GoToCreditsScreen()
    {
        GameOverScreen.SetActive(false);
        StatsScreen.SetActive(false);
        CreditsScreen.SetActive(true);
    }


    public void RestartGame()
    {
        StaticVar.GameActive = StaticVar.inMainMenu = StaticVar.inHideSeek =
            StaticVar.inPlatforming = StaticVar.inSimonSays = StaticVar.inDrawing = false;

        StaticVar.numOfRandomGames = StaticVar.numOfHideSeekGames = StaticVar.numOfSimonSaysGames =
            StaticVar.numOfPlatformingGames = StaticVar.numOfDrawingGames = StaticVar.numOfExits = 0;

        StaticVar.TotalTime = StaticVar.TimeInMainMenu = StaticVar.TimeInHideSeek =
            StaticVar.TimeInPlatforming = StaticVar.TimeInSimonSays = StaticVar.TimeInDrawing =

        StaticVar.HighScoreInHideSeek = StaticVar.HighScoreInSimonSays =
            StaticVar.HighScoreInPlatforming = StaticVar.HighScoreInDrawing = 0f;

        SceneManager.LoadScene("Menu");
    }

   

}
