using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    public MainManager Manager;

    private void OnCollisionEnter(Collision other)
    {
        if (SceneManager.GetActiveScene().name == "main")
        {
            Destroy(other.gameObject);
            Manager.GameOver();
        }
        
    }
}
