using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public enum Game_Status
    {
        Ready,
        InGame,
        GameOver
    }
    private Game_Status status;
    public GameObject ReadyPanel;
    public GameObject GamePanel;
    public GameObject GameOverPanel;
    public ObstacleSystem obstacleSystem;
    public Player player;
    public TextMeshProUGUI uiScore;
    public TextMeshProUGUI uiScoreend;
    public TextMeshProUGUI uiScorebest;
    public int score = 0;
    public int bestscore = 0;
    public int Score
    {
        get { return score; }
        set
        {
            this.score = value;
            this.uiScore.text = this.score.ToString();
            this.uiScoreend.text = this.score.ToString();
            this.uiScorebest.text = this.bestscore.ToString();
        }
    }
    void Start()
    {
        this.ReadyPanel.SetActive(true);
        Status = Game_Status.Ready;
        this.player.OnDeath += Player_Ondeath;
        this.player.OnScore = OnPlayerScore;
    }
    void OnPlayerScore(int score)
    {
        Score += score;
    }
    public void Init()
    {
        player.idle();
        player.transform.position = new Vector3(-2, -0.2f, 0);
        player.death = false;
        Score = 0;
    }
    private Game_Status Status
    {
        get { return status; }
        set { this.status = value;
             this.updateUI();
        }
    }
    public void StartGame()
    {
        this.Status = Game_Status.InGame;
        obstacleSystem.StartRun();
        player.fly();
    }
    private void Player_Ondeath()
    {
        this.Status = Game_Status.GameOver;
        this.obstacleSystem.Stop();
    }
    public void updateUI()
    {
        this.ReadyPanel.SetActive(this.Status == Game_Status.Ready);
        this.GamePanel.SetActive(this.Status == Game_Status.InGame);
        this.GameOverPanel.SetActive(this.Status == Game_Status.GameOver);
    }
    public void Restart ()
    {
        Init();
        this.Status = Game_Status.Ready;
        this.obstacleSystem.Init();
    }
}
