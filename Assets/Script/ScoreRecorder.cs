using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour {
	private float score;

	public float getScore()
	{
		return score;
	}

	public void Record(GameObject disk)
	{
		score += (100 - disk.GetComponent<DiskData>().size *(20 - disk.GetComponent<DiskData>().speed));

		//根据颜色加分
		Color c = disk.GetComponent<DiskData>().color;
		switch (c.ToString())
		{
		case "red":
			score += 50;
			break;
		case "green":
			score += 40;
			break;
		case "blue":
			score += 30;
			break;
		case "yellow":
			score += 10;
			break;
		}
	}

	public void Reset()
	{
		score = 0;
	}
}

