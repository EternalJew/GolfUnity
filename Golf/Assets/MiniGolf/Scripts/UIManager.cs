using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private Image powerImage;
    [SerializeField] private Text shotText;
    [SerializeField] private GameObject mainMenu, gameMenu, gameOverPanel, retryBtn, nextBtn;
    [SerializeField] private GameObject lvlBtnPrefab, container;
    public Image PowerImage { get { return powerImage; } }
    public Text ShotText { get { return shotText; } }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        powerImage.fillAmount = 0;
    }
    private void Start()
    {
        if(GameManager.singleton.gameStatus == GameStatus.NONE)
        {
            CreateLevelButtons();
        }
        else if(GameManager.singleton.gameStatus == GameStatus.FAILED || 
        GameManager.singleton.gameStatus == GameStatus.COMPLETED)
        {
            mainMenu.SetActive(false);
            gameMenu.SetActive(true);
            LevelManager.instance.SpawnLevel(GameManager.singleton.currentLevelIndex);
        }
    }
    private void CreateLevelButtons()
    {
        for(int i = 0; i < LevelManager.instance.LevelDatas.Length; i++)
        {
            GameObject lvlBtn = Instantiate(lvlBtnPrefab, container.transform);
            lvlBtn.transform.GetChild(0).GetComponent<Text>().text = "" + (i + 1);
            Button btn = lvlBtn.GetComponent<Button>();

            btn.onClick.AddListener(() => OnClick(btn));
        }
    }
    public void OnClick(Button btn)
    {
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
        GameManager.singleton.currentLevelIndex = btn.transform.GetSiblingIndex();
        LevelManager.instance.SpawnLevel(GameManager.singleton.currentLevelIndex);
    }
    public void GameResult()
    {
        switch (GameManager.singleton.gameStatus)
        {
            case GameStatus.FAILED:
                break;
            case GameStatus.COMPLETED:
                break;
        }
    }
}
