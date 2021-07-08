using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
  public static LevelManager instance;
  [SerializeField] private GameObject ballPrefab;
  [SerializeField] private LevelData[] levelDatas;
  public LevelData[] LevelDatas { get { return levelDatas; } }
  private int shotCount = 0, maxShotCount = 0;
  
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
  }
  /*Fast Level Spawn*/
  // private void Start()
  // {
  //     SpawnLevel(GameManager.singleton.currentLevelIndex);
  // }
  public void SpawnLevel(int levelIndex)
  {
      Instantiate(levelDatas[levelIndex].levelPrefab, Vector3.zero, Quaternion.identity);
      shotCount = levelDatas[levelIndex].shotLimit;
      maxShotCount = shotCount;

      GameObject ball = Instantiate(ballPrefab, Vector3.up * 0.5f, Quaternion.identity);

      GameManager.singleton.gameStatus = GameStatus.PLAYING;
      UIManager.instance.ShotText.text = shotCount + "/" + maxShotCount;
  }

  public void ShotTaken()
  {
      if(shotCount > 0)
      {
          shotCount--;
          UIManager.instance.ShotText.text = shotCount + "/" + maxShotCount;
          if(shotCount <= 0)
          {
            LevelFaield();
          }
      }
  }
  public void LevelComplete()
  {
    if(GameManager.singleton.gameStatus == GameStatus.PLAYING)
    {
      GameManager.singleton.gameStatus = GameStatus.COMPLETED;
      if(GameManager.singleton.currentLevelIndex < levelDatas.Length)
      {
        GameManager.singleton.currentLevelIndex++;
      }
      else
      {
        GameManager.singleton.currentLevelIndex = 0;
      }
      UIManager.instance.GameResult();
    }
  }
  public void LevelFaield()
  {
      if(GameManager.singleton.gameStatus == GameStatus.PLAYING)
      {
      GameManager.singleton.gameStatus = GameStatus.FAILED;
      UIManager.instance.GameResult();
    }
  }
}
