using UnityEngine;
using System.Collections;

public class WinCheckBehaviorScript : MonoBehaviour {

	public int levelMultiplier = 1;   //The level multiplier. Used for calculating the score.

	private float gameTime = 0f;
	private ArrayList winningPositionObjects = new ArrayList();   //The actual winning position objects.
	private ArrayList BlockObjects = new ArrayList();   //The actual block objects.
	private bool levelComplete = false;   //Has the level been completed
	private int totalNumberOfSlides = 0;   //The total number of slides done on all blocks

	//retrieve all winning position game objects
	void Start()
	{
		Object[] objects = FindObjectsOfType(typeof(GameObject));

		foreach (GameObject obj in objects)
		{
			if (obj.tag == "WinningPosition")
			{
				winningPositionObjects.Add(obj);
			}
			else if (obj.tag == "Block")
			{
				BlockObjects.Add(obj);
			}
		}
	}

	//Update is run every frame.
	void Update()
	{
		if (HasLevelCompleted())
		{
			if (totalNumberOfSlides == 0)
			{
				foreach (GameObject obj in BlockObjects)
				{
					totalNumberOfSlides += obj.GetComponent<BlockBehaviorScript>().GetNumberOfSlides();
				}
			}

			Debug.Log("Level Completed = true");
			Debug.Log("Time used = " + Mathf.Floor(gameTime) + " Seconds");
			Debug.Log("Total Number Of Slides = " + totalNumberOfSlides);
			Debug.Log("Level Score = " + CalculateLevelScore());
		}
		else
		{
			gameTime += Time.deltaTime;
		}
	}

	/**
	 * Check how many of the blocks, which are in position.
	 * If all the blocks is in position the level is completed.
	 * 
	 * True is returned when level is completed.
	 */
	bool HasLevelCompleted()
	{
		int numberInPosition = 0;
		
		foreach (GameObject obj in winningPositionObjects)
		{
			if (obj.GetComponent<WinPositionBehaviorScript>().GetHasBlock())
			{
				numberInPosition++;
			}
		}

		//If all blocks are on all winning positions, level has been completed
		if (numberInPosition == winningPositionObjects.Count)
		{
			levelComplete = true;
		}

		return levelComplete;
	}

	/**
	 * Calculate the level score
	 */
	int CalculateLevelScore()
	{
		int score = Mathf.RoundToInt((1000 - gameTime - totalNumberOfSlides) * levelMultiplier);

		//If the score has a negative value, set the score to zero
		if (score < 0)
		{
			score = 0;
		}

		return score;
	}

}
