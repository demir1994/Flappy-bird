using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject spawnableEnvironmentPrefab;

    public FlappyController player;

    public int flappy_Points;
    public bool AddressableSpawning;

    public Text earnedPointsTxt;
    public Text earnedPointsTxtRes;
    public AudioSource pointFX;

    public GameObject SpawnArea;

    public void EarnPoint(int numberOfPoints)
    {
        pointFX.Play();
        flappy_Points += numberOfPoints;
        earnedPointsTxt.text = flappy_Points.ToString();
        earnedPointsTxtRes.text = flappy_Points.ToString();
    }
    public void LosePoint(int numberOfPoints)
    {
        flappy_Points -= numberOfPoints;
        earnedPointsTxt.text = flappy_Points.ToString();
        earnedPointsTxtRes.text = flappy_Points.ToString();
    }

    private void Awake()
    {
        //GenerateObjects();

        if (instance == null)
        {
            instance = this;
        }
    }

    public void GenerateObjects()
    {
        if (!AddressableSpawning)
        {
            Instantiate(spawnableEnvironmentPrefab, SpawnArea.transform);
        }
        else
        {
            AsyncOperationHandle<GameObject> asyncOperationHandle =
                Addressables.LoadAssetAsync<GameObject>("GamePrefabs/SpawnableGameObjects");

            asyncOperationHandle.Completed += AsyncOperationHandle_Completed;
        }

    }

    private void AsyncOperationHandle_Completed(AsyncOperationHandle<GameObject> asyncOperationHandle)
    {
        if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Instantiate(asyncOperationHandle.Result);
        }
        else
        {
            Debug.LogWarning("Failed to load addressable!");
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
