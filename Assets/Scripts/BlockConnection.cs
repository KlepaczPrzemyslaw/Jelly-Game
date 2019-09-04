using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockConnection : MonoBehaviour
{
	private List<Block> _connectedBlocks = new List<Block>();
	private LineRenderer _lineRenderer;
	private Board _board;

	private BlockColor? _currentColor;

	public event Action<int> OnConnection;

	void Awake()
	{
		_lineRenderer = GetComponent<LineRenderer>();
		_board = FindObjectOfType<Board>();
	}

	void Start()
	{

	}

	void Update()
	{
		if (Input.GetMouseButtonUp(0))
			FinishConnection();
	}

	public void Connect(Block block)
	{
		if (!Input.GetMouseButton(0)) return;
		if (_connectedBlocks.Contains(block)) return;

		if (!_currentColor.HasValue)
			_currentColor = block.CurrentColor;

		if (_currentColor != block.CurrentColor) return;

		if (_connectedBlocks.Count >= 1 &&
			!_connectedBlocks.Last().IsNoighbour(block))
			return;

		block.isConnected = true;
		_connectedBlocks.Add(block);

		RefreshConnection();
	}

	private void FinishConnection()
	{
		_connectedBlocks
			.ForEach(block => block.isConnected = false);

		if (_connectedBlocks.Count >= 3)
		{
			if (OnConnection != null)
				OnConnection.Invoke(_connectedBlocks.Count);

			_board.RemoveConnectedBlocks(_connectedBlocks);
			_board.RefreshBlocks();
		}

		_connectedBlocks.Clear();
		_currentColor = null;
		RefreshConnection();
	}

	private void RefreshConnection()
	{
		var points = _connectedBlocks
			.Select(block => _board.GetBlockPosition(block.X, block.Y))
			.Select(pos => (Vector3)pos + Vector3.back * 2f)
			.ToArray();

		_lineRenderer.positionCount = points.Length;
		_lineRenderer.SetPositions(points);
	}
}
