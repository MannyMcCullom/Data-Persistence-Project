using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.PlayerLoop;

public class MenuBackground : MonoBehaviour {
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public void Start()
    {
        Simulate();
    }

    public void BreakBricks()
    {
        GameObject[] allBricks = GameObject.FindGameObjectsWithTag("Brick");
        foreach (GameObject b in allBricks)
        {
            Destroy(b);
        }
    }

    public void Simulate()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
            }
        }

        float randomDirection = Random.Range(-1.0f, 1.0f);
        Vector3 forceDir = new Vector3(randomDirection, 1, 0);
        forceDir.Normalize();

        Ball.transform.SetParent(null);
        Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);

        Invoke("BreakBricks", 15.0f);
        Invoke("Simulate", 16.0f);
    }
}
