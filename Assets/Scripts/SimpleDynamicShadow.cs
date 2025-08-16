using UnityEngine;

public class SimpleDynamicShadow : MonoBehaviour
{
    [SerializeField] private GameObject _shadowPrefab;
    [SerializeField] private Vector3 _shadowOffset;
    [SerializeField] private float _shadowScale;

    private GameObject shadowInstance;

    void Start()
    {
        shadowInstance = Instantiate(_shadowPrefab, transform);
        shadowInstance.transform.localPosition = _shadowOffset;
        shadowInstance.transform.localScale = Vector3.one * _shadowScale;
    }

    void Update()
    {
        if (shadowInstance == null) return;
        shadowInstance.transform.position = transform.position + _shadowOffset;
        Quaternion rot = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
        shadowInstance.transform.rotation = rot;
    }
}