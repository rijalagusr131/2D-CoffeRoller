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

    [Header("UI")]
    public GameObject toggleMuteOn;
    private bool isMute;

    public GameObject pauseMenu;
    public bool isPause;

    public bool isTutorial;

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
                //UserDataManager.Remove();
                UserDataManager.Load();
                isMute = ShowIsSoundMuted();
                CheckIsMute();
                CheckMuteButton();
                AudioManager.instance.Play("Main Music");
            break;
            case CanvasType.PlayScene :
                UserDataManager.Load();
                if(character == null){
                    character = FindObjectOfType<CharMoveController>();
                }
                isPause = false;
                isMute = ShowIsSoundMuted();
                CheckIsMute();
                CheckMuteButton();
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

            //tutorial
            if (isTutorial == true)
            {
                Time.timeScale = 0;
            }
            else if (isPause == true && isTutorial != true)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            }
            else
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }

            // game over
            if (character.transform.position.y < fallPositionY){
                GameOver();
            }

            

            diamondText.text = numberOfDiamond.ToString();
        }
    }

    public void MuteToggle(){
        isMute = !isMute;
        CheckIsMute();
        CheckMuteButton();
        SetSoundMuted(isMute);
    }

    public void CheckIsMute(){
        if (isMute == false){
            AudioListener.volume = 0;
        }
        else{
            AudioListener.volume = 1;
        }
    }

    public void CheckMuteButton(){
        toggleMuteOn.SetActive(isMute);
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

    public bool ShowIsSoundMuted(){
        return UserDataManager.Progress.IsSoundMuted;
    }

    public void SetSoundMuted(bool value){
        UserDataManager.Progress.IsSoundMuted = value;
        UserDataManager.Save();
    }

    public void AddTutorialDone()
    {
        UserDataManager.Progress.IsTutorialDone = true;
        UserDataManager.Save();
    }

    public bool ShowIsTutorialDone()
    {
        return UserDataManager.Progress.IsTutorialDone;
    }

    public void PauseControl(){
        isPause = !isPause;
    }

}
