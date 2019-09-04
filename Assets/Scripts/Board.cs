using System;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
	[SerializeField]
	private GameObject blockPrefab;

	[SerializeField]
	[Range(3, 8)]
	private int width = 5;

	[SerializeField]
	[Range(3, 10)]
	private int height = 6;

	[SerializeField]
	[Range(0.5f, 5)]
	private float gridSize = 2f;

	[SerializeField]
	[Range(0.5f, 5)]
	public float BlockSize = 1f;

	public Block[,] Blocks { get; private set; }

	void Start()
	{
		GenerateBoard();
	}

	void Update()
	{

	}

	public Vector2 GetBlockPosition(int x, int y)
	{
		var basePosition = new Vector2(
			x - width / 2f + 0.5f,
			y - height / 2f + 0.5f);
		return basePosition * gridSize;
	}

	public void RemoveConnectedBlocks(List<Block> connectedBlocks)
	{
		connectedBlocks.ForEach(b => Blocks[b.X, b.Y] = null);
		connectedBlocks.ForEach(b => Destroy(b.gameObject));
	}

	public void RefreshBlocks()
	{
		for (int x = 0; x < width; x++)
		{
			int h = 0;

			for (int y = 0; y < height; y++)
			{
				if (Blocks[x, y] == null) continue;

				Blocks[x, h] = Blocks[x, y];
				Blocks[x, h].Configure(x, h);

				h++;
			}

			for (int y = h; y < height; y++)
			{
				Blocks[x, y] = GenerateBlock(x, y);
				Blocks[x, y].PlaceOnTargetPosition();
			}
		}
	}

	private void GenerateBoard()
	{
		Blocks = new Block[width, height];

		for (int x = 0; x < width; x++)
			for (int y = 0; y < height; y++)
				Blocks[x, y] = GenerateBlock(x, y);
	}

	private Block GenerateBlock(int x, int y)
	{
		var obj = Instantiate(blockPrefab);
		obj.transform.parent = transform;
		obj.transform.position = Vector3.zero;
		obj.transform.localScale = Vector3.one * BlockSize;

		var block = obj.GetComponent<Block>();
		block.Configure(x, y);
		return block;
	}
}
