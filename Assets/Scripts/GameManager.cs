using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private float gameDuration = 60f;

	private float _remainingTime;
	public float RemainingTime
	{
		get
		{
			return _remainingTime;
		}
		private set
		{
			_remainingTime = value;

			if (_remainingTime <= 0f)
				OnEndGame();

			if (OnRemainingTimeChanged != null)
				OnRemainingTimeChanged.Invoke(_remainingTime);
		}
	}

	private int _score;
	public int Score
	{
		get
		{
			return _score;
		}
		private set
		{
			_score = value;

			if (OnScoreChanged != null)
				OnScoreChanged.Invoke(_score);
		}
	}

	public event Action<float> OnRemainingTimeChanged;
	public event Action<int> OnScoreChanged;

	void Awake()
	{
		FindObjectOfType<BlockConnection>().OnConnection += UpdateScore;
	}

	void Start()
	{
		RemainingTime = gameDuration;
		Score = 0;
		StartCoroutine(TimeCounterCoroutine());
	}

	private IEnumerator TimeCounterCoroutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(1f);
			RemainingTime -= 1f;
		}
	}

	private void OnEndGame()
	{
		PlayerPrefs.SetInt(PlayerPrefsKeys.LastGameScore, Score);
		var recordScore = PlayerPrefs.GetInt(PlayerPrefsKeys.RecordScore, 0);

		if (Score > recordScore)
			PlayerPrefs.SetInt(PlayerPrefsKeys.RecordScore, Score);

		FindObjectOfType<LoadSceneEffect>().ChangeScene("Menu");
	}

	private void UpdateScore(int length)
	{
		Score += length * length;
	}
}
