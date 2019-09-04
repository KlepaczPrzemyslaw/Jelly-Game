using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum TextDirection
{
	Left,
	Right,
	Center
}

public class NumBar : MonoBehaviour
{
	private List<GameObject> _digitsList = new List<GameObject>();

	[SerializeField]
	private Sprite[] sprites;

	[SerializeField]
	private TextDirection textDirection;

	private int _value = 0;
	public int Value
	{
		get
		{
			return _value;
		}
		set
		{
			_value = value;
			RefreshNumber();
		}
	}

	private void RefreshNumber()
	{
		RemoveCurrentDigits();

		var digits = Value
			.ToString()
			.Select(c => int.Parse(c.ToString()))
			.ToArray();

		for (int i = 0; i < digits.Length; i++)
		{
			var position = CalculatePosition(i, digits.Length);
			var digit = CreateDigit(position, digits[i]);
			_digitsList.Add(digit);
		}
	}

	private void RemoveCurrentDigits()
	{
		_digitsList.ForEach(number => Destroy(number));
		_digitsList.Clear();
	}

	private GameObject CreateDigit(Vector3 position, int value)
	{
		var digit = new GameObject();
		digit.transform.parent = transform;
		digit.transform.localPosition = position;

		var sprite = sprites[value];
		digit.AddComponent<SpriteRenderer>().sprite = sprite;

		return digit;
	}

	private Vector3 CalculatePosition(int index, int numberOfDigits)
	{
		var basePosition = 0f;

		switch (textDirection)
		{
			case TextDirection.Left:
				basePosition = index;
				break;
			case TextDirection.Right:
				basePosition = index - numberOfDigits + 1;
				break;
			case TextDirection.Center:
				basePosition = index - numberOfDigits / 2f + 0.5f;
				break;
			default:
				break;
		}

		return Vector3.right * basePosition * 0.65f;
	}
}
