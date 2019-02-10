using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

    public static int EnemiesAlive = 0;

    public Wave[] waves;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private float timeBetweenWaves = 10f;

    private float countdown = 5f;

    [SerializeField]
    private Text waveCountdownTimer;

    private int waveIndex = 0;

	void Update () {

        if(EnemiesAlive > 0)
        {
            return;
        }

		if(countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownTimer.text = string.Format("{0:00.00}", countdown);
	}


    IEnumerator SpawnWave()
    {
        PlayerStats.rounds++;

        Wave wave = waves[waveIndex];

        for (int i = 0; i < wave.count1; i++)
        {
            SpawnEnemy1(wave.enemy1);
            yield return new WaitForSeconds(1f / wave.rate1);
        }

        for (int i = 0; i < wave.count2; i++)
        {
            SpawnEnemy2(wave.enemy2);
            yield return new WaitForSeconds(1f / wave.rate2);
        }

        for (int i = 0; i < wave.count3; i++)
        {
            SpawnEnemy3(wave.enemy3);
            yield return new WaitForSeconds(1f / wave.rate3);
        }

        waveIndex++;

        if(waveIndex == waves.Length)
        {
            Debug.Log("Niveau Terminé ! Bravo !");
            this.enabled = false;   
        }
    }

    void SpawnEnemy1(GameObject enemy1)
    {
        Instantiate(enemy1, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;
    }

    void SpawnEnemy2(GameObject enemy2)
    {
        Instantiate(enemy2, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;
    }

    void SpawnEnemy3(GameObject enemy3)
    {
        Instantiate(enemy3, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;
    }
}
