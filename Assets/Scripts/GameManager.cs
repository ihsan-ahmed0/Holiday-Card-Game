using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    public int currentPoints = 0;
    public int goalPoints = 100;
    public float goalPointIncrements = 1.5f;
    public int turn = 0;
    public GameObject loseCanvas;
    public TMP_Text currentPointsText;
    public TMP_Text goalPointsText; 


    // Start is called before the first frame update
    void Start()
    {
        reset();
        updatePoints();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            addPoints(10);
            updatePoints();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            multiplyPoints(1.5f);
            updatePoints();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            endTurn();
            updatePoints();
        }
    }

    public void addPoints(int x)
    {
        currentPoints += x;
    }

    public void multiplyPoints(float x)
    {
        currentPoints = Mathf.CeilToInt(currentPoints * x);
    }

    public void reset()
    {
        currentPoints = 0;
        goalPoints = 100;
        goalPointIncrements = 1.5f;
        turn = 0;
        loseCanvas.SetActive(false);
    }

    void endTurn()
    {
        if (currentPoints >= goalPoints)
        {
            currentPoints = 0;
            goalPoints = Mathf.CeilToInt(goalPoints * goalPointIncrements);
            Debug.Log("You have enough points to move on! New point goal: " + goalPoints);
            turn += 1;
        }
        else
        {
            // Implement game over
            loseCanvas.SetActive(true);
            Time.timeScale = 0f;
        }

    }

    void updatePoints()
    {
        currentPointsText.text = "Points: " + currentPoints;
        goalPointsText.text = "Goal: " + goalPoints;
    }
}
