using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum CanvasType{
    MainMenu,
    PlayScene,
}

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    [Header("CANVAS CONTROLLER")]
    public CanvasType type;

    [Header("Scoring")]
    public ScoreController score;
    public float scoringRatio;
    private float lastPositionX;

    [Header("GameOver")]
    public GameObject gameOverScreen;
    public float fallPositionY;

    [Header("Camera")]
    public CameraMove gameCamera;

    public int numberOfDiamond;
    public Text diamondText;

    [Header("Character")]
    private CharMoveController character;

    private void Start() {
        SwithCanvas();
    }

    private void Update() {
        UIUpdate();
    }

    private void SwithCanvas(){
        switch(type){
            case CanvasType.MainMenu :
                AudioManager.instance.Play("Main Music");
            break;
            case CanvasType.PlayScene :
                if(character == null){
                    character = FindObjectOfType<CharMoveController>();
                }
                numberOfDiamond = 0;
            break;
            default :
            break;
        }
    }

    private void UIUpdate(){
        if(type == CanvasType.PlayScene){
            // calculate score
            int distancePassed = Mathf.FloorToInt(character.transform.position.x - lastPositionX);
            int scoreIncrement = Mathf.FloorToInt(distancePassed / scoringRatio);

            if(scoreIncrement > 0){
                score.IncreaseCurrentScore(scoreIncrement);
                lastPositionX += distancePassed;
            }

            // game over
            if(character.transform.position.y < fallPositionY){
                GameOver();
            }

            diamondText.text = numberOfDiamond.ToString();
        }
    }

    public void GameOver()
    {
        // stop camera movement
        gameCamera.enabled = false;

        // show gameover
        gameOverScreen.SetActive(true);

        // disable this too
        character.enabled = false;
    }

    public void LoadScene(string menu){
		SceneManager.LoadScene(menu);
	}
}
