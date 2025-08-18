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
            newPiece.InitPiece(_pieces[i]);
            AddToSpawned(newPiece);
        }
        StartCoroutine(_PieceMoveHandler.RePosPieceCenter(_createdPieces));
    }
    void AddToSpawned(Piece piece)
    {
        _createdPieces.Add(piece);
    }
    public void RemoveFromHand(Piece piece)
    {
        _createdPieces.Remove(piece);
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
    public void BlockAllInteractions()
    {
        foreach (Piece piece in _createdPieces)
        {
            piece.CanInteract = false;
        }
    }
    public void OpenAllInteractions()
    {
        foreach (Piece piece in _createdPieces)
        {
            piece.CanInteract = true;
        }
    }
}