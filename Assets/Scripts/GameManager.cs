using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{

    public static GameManager instance = null;
    public bool playerMovement = true;
    public Text lifesText;
    public Text pointsText;
    public Text highScoreText;
    public Text lastScoreText;
    public GameObject player;
    public Transform SpawnPoint;
    public GameObject finalPortal;
    public GameObject gameOverPanel;
    public GameObject fadePanel;
    public int itemsPerLevel;
	public AudioClip[] collectableClips;

    private bool stopMovement = false;
    private int itemsAmount = 0;
    private int playerLifes = 3;
    private int points;
    private int highScore = 0;

	private AudioSource sound;

    public bool StopMovement
    {
        get { return stopMovement; }
    }

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start(){
		sound = GetComponent<AudioSource>();
        EnablePortal(false);
        player.SetActive(false);
        SpawnPlayer();
        highScore = PlayerPrefs.GetInt("Score", 0);

    }

    void Update(){
        lifesText.text = "x "+playerLifes;
        pointsText.text = points.ToString();
        if(itemsAmount==itemsPerLevel){
            EnablePortal(true);         
        }
    }

    public void PlayerMove(bool canMove){
        playerMovement = canMove;
    }

    public void PickItem(){
        itemsAmount++;
		sound.PlayOneShot(collectableClips[1]);
        points += 15;
    }
    public void PickCoin()
    {
        points += 10;
		sound.PlayOneShot(collectableClips[0]);
    }

    public void LoseLife(){
        if(points>10){
            points -= 10;
        }
        if(playerLifes>1){
            playerLifes--;
            KillPlayer();
            Invoke("SpawnPlayer", 1f);
        }else{
            playerLifes = 0;
            FinishGame();
        }
    }

    public void EnablePortal(bool isEnable){
        finalPortal.SetActive(isEnable);      
        stopMovement = isEnable;      
    }
    
    public void SpawnPlayer(){
        player.SetActive(true);
        PlayerMove(true);
        player.transform.position = SpawnPoint.position;
		sound.PlayOneShot(collectableClips[4]);

    }

    public void KillPlayer(){
        PlayerMove(false);
        player.SetActive(false);
      
    }

    public void LevelCompleted(){
        PlayerMove(false);

        sound.PlayOneShot(collectableClips[2]);
        
        fadePanel.SetActive(true);
        if(points>highScore){
            PlayerPrefs.SetInt("Score", points);
            highScore = points;
        }
        highScoreText.text = highScore.ToString();
        lastScoreText.text = points.ToString();
        Invoke("LoadScene", 5f);
    }

    public void FinishGame(){
        KillPlayer();
        stopMovement = true;
        gameOverPanel.SetActive(true);
        //Music Screen
    }

    public void PlayAgain(){
        gameOverPanel.SetActive(false);
        LoadScene();
    }

    public void LoadScene(){
        SceneManager.LoadScene(0);
    }

	
}
