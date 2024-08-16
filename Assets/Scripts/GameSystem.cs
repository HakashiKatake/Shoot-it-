using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject ballPrefab;

    [SerializeField]
    private SpawnArea spawnArea;

    [SerializeField]
    private LivesControl livesControl;

    [SerializeField]
    private GameObject gameOverScreen;

    [SerializeField]
    private TMP_Text scoreText;

    [SerializeField]
    private TMP_Text finalScoreText;

    private GameObject currentBall;
    private SceneSystem sceneSystem = new SceneSystem();

    private int score = 0;
    private bool isScored = false;
    private bool isGameOver = false;

    private void Awake() {
        sceneSystem.init();
        livesControl.onGameOverEvent += onGameOver;
    }

    public void scored(){
        isScored = true;
        score += 1;
        setScore();
    }

    private void setScore(){
        scoreText.SetText(score.ToString());
    }

    private void setFinalScore(){
        finalScoreText.SetText("Score: " + score.ToString());
    }

    public void startGame(){
        score = 0;
        setScore();
        isGameOver = false;
        spawnBall();        
    }

    private void onGameOver(){
        print("Game Over");
        isGameOver = true;
        setFinalScore();
        gameOverScreen.SetActive(true);
    }

    public Scene getScenePrediction(){
        return sceneSystem.getScenePrediction();
    }

    public PhysicsScene2D getScenePredictionPhysics(){
        return sceneSystem.getScenePredictionPhysics();
    }

    private void respawnBall(){
        if(!isScored)
            livesControl.loseLive();
        
        isScored = false;
        destroyBall();
        spawnBall();
    }

    private void checkGameOver(){
        if(isGameOver) destroyBall();
    }

    private void spawnBall(){
        currentBall = Instantiate(ballPrefab);
        Ball ballCommand = currentBall.GetComponent<Ball>();

        ballCommand.scoredEvent += scored;
        ballCommand.onGroundEvent += respawnBall;
        ballCommand.onGroundEvent += checkGameOver;

        spawnArea.spawnBall(currentBall.transform);
    }

    private void destroyBall(){
        if(!currentBall) return;

        Ball ballCommand = currentBall.GetComponent<Ball>();

        ballCommand.scoredEvent -= scored;
        ballCommand.onGroundEvent -= respawnBall;

        Destroy(currentBall);
        currentBall = null;
    }

    void FixedUpdate()
    {
        sceneSystem.FixedUpdate();
    }
}
