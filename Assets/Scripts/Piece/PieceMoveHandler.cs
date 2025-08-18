using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;
using Zenject;

public class PieceMoveHandler : MonoBehaviour, IInitializable
{
    [Header("----Components")]
    [SerializeField] private Transform _selectedOffset;
    [Header("----DoTween Props")]
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private float _raiseDuration = 0.2f;
    [SerializeField] private float _raiseAmount = 0.5f;
    [SerializeField] private float _spawnDuration;
    [SerializeField, Range(0, 10)] private float _distance = 1f; // Hücre aralığı
    [SerializeField] private Ease _ease = Ease.OutQuad;
    [SerializeField] private Ease _easeToHand = Ease.OutQuad;
    [SerializeField] private Ease _raiseUp = Ease.OutQuad;
    [SerializeField] private Vector2 _startPos = Vector2.zero; // Grid başlangıcı
    [SerializeField, Min(1)] private int _columns = 5; // Satır başına düşen sütun sayısı
    [SerializeField] private Transform _offset; // SAHNEDE SEÇİLEBİLİR OFFSET

    [Header("----SELECTED DoTween Props")]
    [SerializeField] private float _scaleAmount;
    [SerializeField] private float _scaleDuration;

    public IEnumerator RePosPieceCenter(List<Piece> pieces)
    {
        yield return StartCoroutine(RePosPieceIE(pieces, _offset));
    }
    public IEnumerator RePosPieceSelected(List<Piece> pieces)
    {
        yield return StartCoroutine(RePosPieceIE(pieces, _selectedOffset));
    }
    /// <summary>
    /// Re position all pieces
    /// </summary>
    /// <param name="pieces">piece list</param>
    /// <returns>it is a Coroutine</returns>
    public IEnumerator RePosPieceIE(List<Piece> pieces, Transform offset)
    {
        if (pieces == null || pieces.Count == 0) yield break;

        int totalPieces = pieces.Count;
        List<Vector2> localPositions = new List<Vector2>(totalPieces);

        // Pozisyonları hesapla
        for (int i = 0; i < totalPieces; i++)
        {
            int col = i % _columns;
            int row = i / _columns;

            Vector2 pos = _startPos + new Vector2(col * _distance, -row * _distance);
            localPositions.Add(pos);
        }

        // Grid merkezini bul
        float minX = float.MaxValue, maxX = float.MinValue;
        float minY = float.MaxValue, maxY = float.MinValue;

        foreach (var pos in localPositions)
        {
            if (pos.x < minX) minX = pos.x;
            if (pos.x > maxX) maxX = pos.x;
            if (pos.y < minY) minY = pos.y;
            if (pos.y > maxY) maxY = pos.y;
        }

        Vector2 gridCenter = new Vector2(
            (minX + maxX) / 2f,
            (minY + maxY) / 2f
        );

        // Taşıma
        int loopCount = Mathf.Min(pieces.Count, localPositions.Count);
        for (int i = 0; i < loopCount; i++)
        {
            pieces[i].CanInteract = false;
            Vector3 worldTargetPos = (offset != null)
                ? offset.position + (Vector3)(localPositions[i] - gridCenter)
                : (Vector3)(localPositions[i] - gridCenter);
            pieces[i].transform
                            .DOMove(worldTargetPos, _duration)
                            .SetEase(_ease)
                            .OnComplete(() => pieces[i].CanInteract = true)
                            .WaitForCompletion();
            pieces[i].GridPosition = worldTargetPos;
            yield return new WaitForSeconds(_spawnDuration);
        }
    }
    /// <summary>
    /// Moving piece to hand
    /// </summary>
    /// <param name="piece"></param>
    /// <returns>the move Tween</returns>
    public Tween ToHand(Piece piece)
    {
        piece.CanInteract = false;
        DOTween.Kill(piece.transform);
        return piece.transform.DOMove(transform.position, _duration).SetEase(_easeToHand).OnComplete(() => piece.CanInteract = true);
    }
    public Tween RaiseUp(Piece piece)
    {
        DOTween.Kill(piece.transform);
        piece.transform.position = piece.GridPosition;
        return piece.transform.DOMoveY(piece.GridPosition.y + _raiseAmount, _raiseDuration).SetEase(_raiseUp);
    }
    public Tween Drop(Piece piece)
    {
        DOTween.Kill(piece.transform);
        piece.transform.position = piece.GridPosition;
        return piece.transform.DOMoveY(piece.GridPosition.y, _raiseDuration).SetEase(_raiseUp);
    }
    public Tween Drop(Piece piece, float amount)
    {
        DOTween.Kill(piece.transform);
        return piece.transform.DOMoveY(piece.transform.position.y - amount, _raiseDuration).SetEase(_raiseUp);
    }
    public Tween Reject(Piece piece)
    {
        DOTween.Kill(piece.transform);
        return piece.transform.DOShakePosition(.2f, .2f);
    }
    public Tween Punch(Piece piece)
    {
        DOTween.Kill(piece.transform);
        return piece.transform.DOPunchScale(Vector2.one * _scaleAmount, _scaleDuration);
    }
    public void Initialize()
    {
        print("Piece Move Handler Initialized");

    }
}