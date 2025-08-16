using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _threshold = 0.05f;

    // Yönleri ve numaraları tanımla (örnek: 6 yüz)
    [SerializeField] private Transform _side1;
    [SerializeField] private Transform _side2;
    [SerializeField] private Transform _side3;
    [SerializeField] private Transform _side4;
    [SerializeField] private Transform _side5;
    [SerializeField] private Transform _side6;
    public bool _showResult;

    void Start()
    {
        _rb.rotation = Random.rotation;
    }
    void Update()
    {
        if (_rb.linearDamping >= .2f)
        {
            _showResult = false;
        }
    }
    public bool IsMoving() =>
        _rb.angularVelocity.magnitude > _threshold ||
        _rb.linearVelocity.magnitude > _threshold;

    public void ForceMe()
    {
        _rb.AddForce(Vector3.one * Random.Range(-1, 1) * 10, ForceMode.Impulse);
    }
    public int GetNumber()
    {
        if (IsMoving()) return 0;
        Transform[] sides = new Transform[] { _side1, _side2, _side3, _side4, _side5, _side6 };
        int bestIndex = 0;
        float maxDot = -1f;

        for (int i = 0; i < sides.Length; i++)
        {
            // Zarın yukarı yönü ile yüz yönünü karşılaştır
            float dot = Vector3.Dot(Vector3.back, sides[i].forward);
            if (dot > maxDot)
            {
                maxDot = dot;
                bestIndex = i;
            }
        }

        return bestIndex + 1; // 1-6 arası sayı
    }

}
