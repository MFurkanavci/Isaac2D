using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextFloor : MonoBehaviour
{
    public void GoToNextFloor()
    {
        int _currentFloor = SceneManager.GetActiveScene().buildIndex;
        int _nextFloor = _currentFloor+1;

        if (_nextFloor < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(_nextFloor);
        }
        else
        {
            DungeonEnd();
        }
    }

    void DungeonEnd()
    {
        Debug.Log("Game Over! Congratulations");
    }
}
