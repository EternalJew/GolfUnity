using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
  public static LevelManager instance;
  [SerializeField] private GameObject ballPrefab;
  [SerializeField] private LevelData[] levelDatas;
  public LevelData[] LevelDatas { get { return levelDatas; } }
  private int shotCount = 0;
  
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
  public void SpawnLevel(int levelIndex)
  {
      Instantiate(levelDatas[levelIndex].levelPrefab, Vector3.zero, Quaternion.identity);
      shotCount = levelDatas[levelIndex].shotLimit;

      GameObject ball = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
  }

  public void ShotTaken()
  {
      if(shotCount > 0)
      {
          shotCount--;
      }
  }
}
