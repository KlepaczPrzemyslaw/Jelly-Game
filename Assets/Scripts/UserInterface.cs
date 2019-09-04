using UnityEngine;

public class UserInterface : MonoBehaviour
{
	[SerializeField]
	private NumBar timeCounter;

	[SerializeField]
	private NumBar scoreCounter;

	void Awake()
	{
		var gameManager = FindObjectOfType<GameManager>();

		gameManager.OnRemainingTimeChanged +=
			time => timeCounter.Value = (int) time;

		gameManager.OnScoreChanged +=
			score => scoreCounter.Value = score;
	}
}
