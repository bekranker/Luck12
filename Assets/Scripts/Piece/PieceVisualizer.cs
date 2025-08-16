using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;

public class PieceVisualizer : MonoBehaviour
{
    [Header("----DoTween Props")]
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private float _spawnDuration;
    [SerializeField, Range(0, 10)] private float _distance = 1f; // Hücre aralığı
    [SerializeField] private Ease _ease = Ease.OutQuad;
    [SerializeField] private Vector2 _startPos = Vector2.zero; // Grid başlangıcı
    [SerializeField, Min(1)] private int _columns = 5; // Satır başına düşen sütun sayısı
    [SerializeField] private Transform _offset; // SAHNEDE SEÇİLEBİLİR OFFSET

    public IEnumerator RePosPiece(List<Piece> pieces)
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
            Vector3 worldTargetPos = (_offset != null)
                ? _offset.position + (Vector3)(localPositions[i] - gridCenter)
                : (Vector3)(localPositions[i] - gridCenter);
            pieces[i].transform
                            .DOMove(worldTargetPos, _duration)
                            .SetEase(_ease)
                            .WaitForCompletion();
            yield return new WaitForSeconds(_spawnDuration);
        }
    }
}