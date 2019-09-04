using System.Linq;
using UnityEngine;

public class Block : MonoBehaviour
{
	private SpriteRenderer _spriteRenderer;
	private Board _board;
	private Vector3 _targetposition;
	private BlockConnection _blockConnection;

	[SerializeField]
	private BlockType[] blockTypes;

	public bool isConnected = false;
	public int X { get; private set; }
	public int Y { get; private set; }
	public BlockColor CurrentColor { get; private set; }

	void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_board = FindObjectOfType<Board>();
		_blockConnection = FindObjectOfType<BlockConnection>();
	}

	void Start()
	{
		CurrentColor = GetRandoColor();
		SetSprite();
	}

	void Update()
	{
		UpdatePosition();
		UpdateScale();
		UpdateColor();
	}

	public void Configure(int x, int y)
	{
		X = x;
		Y = y;

		_targetposition = _board.GetBlockPosition(x, y);
		isConnected = false;
	}

	public void PlaceOnTargetPosition()
	{
		transform.localPosition = _targetposition;
		transform.localScale = Vector3.zero;
		_spriteRenderer.color = new Color(1f, 1f, 1f, 0f);

	}

	public bool IsNoighbour(Block currentBlock)
	{
		if (Mathf.Abs(X - currentBlock.X) > 1f)
			return false;

		if (Mathf.Abs(Y - currentBlock.Y) > 1f)
			return false;

		return true;
	}

	private void UpdateColor()
	{
		var targetColor = isConnected ?
			new Color(1f, 1f, 1f, 0.5f) :
			Color.white;
		_spriteRenderer.color = Color.Lerp(
			_spriteRenderer.color,
			targetColor,
			Time.deltaTime * 5f);
	}

	private void UpdateScale()
	{
		var targetScale = isConnected ? 
			0.8f * _board.BlockSize : 
			1f * _board.BlockSize;
		transform.localScale = Vector3.Lerp(
			transform.localScale,
			targetScale * Vector3.one,
			Time.deltaTime * 5f);
	}

	private void UpdatePosition()
	{
		transform.localPosition = Vector3.Lerp(
			transform.localPosition,
			_targetposition,
			Time.deltaTime * 5f);
	}

	private BlockColor GetRandoColor()
	{
		return (BlockColor) Random.Range(0, blockTypes.Length);
		//var values = System.Enum.GetValues(typeof(BlockColor));
		//var index = Random.Range(0, values.Length);
		//return (BlockColor) values.GetValue(index);
	}

	private void SetSprite()
	{
		_spriteRenderer.sprite =
			blockTypes.First(obj => obj.Color == CurrentColor).Sprite;
		_spriteRenderer.color = Color.white;
	}

	private void OnMouseEnter()
	{
		_blockConnection.Connect(this);
	}

	private void OnMouseDown()
	{
		_blockConnection.Connect(this);
	}
}

public enum BlockColor
{
	Red,
	Green,
	Blue,
	Yellow,
	Magenta,
	Gray
}

[System.Serializable]
public class BlockType
{
	public BlockColor Color;
	public Sprite Sprite;
}