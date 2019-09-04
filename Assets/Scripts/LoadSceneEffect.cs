using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class LoadSceneEffect : MonoBehaviour
{
	private Image _whiteBlend;

	void Start()
	{
		_whiteBlend = CreateBlend();
		StartCoroutine(FadeInCoroutine());
	}

	public void ChangeScene(string sceneName)
	{
		StartCoroutine(FadeOutCoroutine(sceneName));
	}

	private Image CreateBlend()
	{
		var obj = new GameObject();
		obj.transform.parent = transform;

		var rectTransform = obj.AddComponent<RectTransform>();
		rectTransform.anchorMin = Vector2.zero;
		rectTransform.anchorMax = Vector2.one;
		rectTransform.anchoredPosition = Vector2.zero;
		rectTransform.sizeDelta = Vector2.zero;

		obj.AddComponent<CanvasRenderer>();

		var image = obj.AddComponent<Image>();
		image.color = Color.white;

		obj.SetActive(false);

		return image;
	}

	private IEnumerator FadeInCoroutine()
	{
		_whiteBlend.gameObject.SetActive(true);

		while (_whiteBlend.color.a > 0f)
		{
			_whiteBlend.color -= new Color(0f, 0f, 0f, 1f) / 0.9f * Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}

		_whiteBlend.gameObject.SetActive(false);
	}

	private IEnumerator FadeOutCoroutine(string sceneName)
	{
		_whiteBlend.gameObject.SetActive(true);

		while (_whiteBlend.color.a <= 1f)
		{
			_whiteBlend.color += new Color(0f, 0f, 0f, 1f) / 0.9f * Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}

		_whiteBlend.gameObject.SetActive(false);
		SceneManager.LoadScene(sceneName);
	}
}
