using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public void SceneStart(){
        SceneManager.LoadScene("Backgroun1(with home)");
    }
}
