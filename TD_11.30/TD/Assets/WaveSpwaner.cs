using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpwaner : MonoBehaviour {

    public Transform enemyPrefab;

    public Transform spwanPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    public Text waveCountdownText;

    private int waveIndex = 0;

     void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpwanWave());
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpwanWave()
    {
        waveIndex++;
        PlayerStats.Rounds++;

        for (int i = 0; i < waveIndex; i++)
        {
            SpwanEnemy();
            yield return new WaitForSeconds(0.5f);

        }

        
    }

    void SpwanEnemy()
    {
        Instantiate(enemyPrefab, spwanPoint.position, spwanPoint.rotation);
    }
}
