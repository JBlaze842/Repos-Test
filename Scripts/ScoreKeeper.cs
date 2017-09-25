using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public static int score;
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
        Reset();
       
    }
    
    //parameter stating it needs an interger to be passed in to run
	public void Score(int points)
    {
        score += points;
        text.text ="Score: " + score.ToString();
    }

    public static void Reset()
    {
        score = 0;
        
    }

}
