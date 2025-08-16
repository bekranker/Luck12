using UnityEngine;

public class TiltEffect2D : MonoBehaviour
{
    [SerializeField] private float _tiltAmount = 35f;     // Dönüş açısı
    [SerializeField] private float _smoothSpeed = 5f;     // Yumuşatma hızı
    [SerializeField] private float _hoverDistance = 1.1f; // Mouse yakınlık mesafesi

    [SerializeField] private Quaternion _originalRotation;
    [SerializeField] private Camera _mainCamera;

    void Start()
    {
        _originalRotation = transform.rotation;
        _mainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 mouseWorldPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = transform.position.z; // Aynı derinlikte karşılaştır

        float distance = Vector2.Distance(mouseWorldPos, transform.position);

        if (distance < _hoverDistance)
        {
            Vector3 direction = mouseWorldPos - transform.position;

            // Normalize ederek 0–1 arası değerle çalış
            float tiltX = -direction.y * _tiltAmount;
            float tiltY = direction.x * _tiltAmount;

            Quaternion targetRotation = Quaternion.Euler(tiltX, tiltY, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _smoothSpeed);
        }
        else
        {
            // Mouse uzakta: orijinal rotasyona dön
            transform.rotation = Quaternion.Lerp(transform.rotation, _originalRotation, Time.deltaTime * _smoothSpeed);
        }
    }
}