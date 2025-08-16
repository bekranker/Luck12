using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PieceHandler : MonoBehaviour, IInitializable
{


    [Header("----Props")]
    [SerializeField] private List<PieceData> _pieces = new();
    [SerializeField] private Piece _piecePrefab;



    [Inject] private PieceVisualizer _pieceVisualizer;
    private List<Piece> _createdPieces = new();



    public List<Piece> GetPieces() => _createdPieces;


    public void Initialize()
    {
        Spawn();
    }


    void Spawn()
    {
        for (int i = 0; i < _pieces.Count; i++)
        {
            var newPiece = Instantiate(_piecePrefab, transform.position, Quaternion.identity);
            newPiece.InitPiece(this, _pieces[i], _pieceVisualizer);
            AddToSpawned(newPiece);
        }
        StartCoroutine(_pieceVisualizer.RePosPiece(_createdPieces));
    }
    void AddToSpawned(Piece piece)
    {
        _createdPieces.Add(piece);
    }
}