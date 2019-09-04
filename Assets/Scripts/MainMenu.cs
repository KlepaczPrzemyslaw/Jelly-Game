using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField]
	private NumBar lastGameScore;

	[SerializeField]
	private NumBar recordScore;

	void Start()
	{
		lastGameScore.Value = PlayerPrefs.GetInt(PlayerPrefsKeys.LastGameScore, 0);
		recordScore.Value = PlayerPrefs.GetInt(PlayerPrefsKeys.RecordScore, 0);
	}

	public void LoadGame()
	{
		FindObjectOfType<LoadSceneEffect>().ChangeScene("Game");
	}
}
