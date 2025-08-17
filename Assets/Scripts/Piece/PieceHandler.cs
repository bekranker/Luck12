using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PieceHandler : MonoBehaviour, IInitializable
{


    [Header("----Props")]
    [SerializeField] private List<PieceData> _pieces = new();
    [SerializeField] private Piece _piecePrefab;



    [Inject] private PieceMoveHandler _PieceMoveHandler;
    public List<Piece> _createdPieces = new();


    public void Initialize()
    {
        print("Piece Handler Initialized");
        Spawn();
    }


    void Spawn()
    {
        for (int i = 0; i < _pieces.Count; i++)
        {
            var newPiece = Instantiate(_piecePrefab, transform.position, Quaternion.identity);
            newPiece.InitPiece(this, _pieces[i], _PieceMoveHandler);
            AddToSpawned(newPiece);
        }
        StartCoroutine(_PieceMoveHandler.RePosPiece(_createdPieces));
    }
    void AddToSpawned(Piece piece)
    {
        _createdPieces.Add(piece);
    }
    public Piece SelectPiece(int number)
    {
        foreach (Piece piece in _createdPieces)
        {
            if (piece.GetData().Number == number)
                return piece;
        }
        return null;
    }

}