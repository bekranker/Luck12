using UnityEngine;
using Zenject;

public class InteractionHandler3D : MonoBehaviour, IInitializable
{
    [SerializeField] private LayerMask _mask;
    [SerializeField] private Camera _camera;

    private GameObject _currentHoverObject;

    public void Initialize() { }

    void Update()
    {
        Raycast();
        CheckMouseButtons();
    }

    void Raycast()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, _mask))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (_currentHoverObject != hitObject)
            {
                if (_currentHoverObject != null)
                {
                    EventManager.Raise(new MouseExitEvent3D(_currentHoverObject, hit.point));

                }

                _currentHoverObject = hitObject;
                EventManager.Raise(new MouseEnterEvent3D(hitObject, hit.point));
            }
        }
        else
        {
            if (_currentHoverObject != null)
            {
                EventManager.Raise(new MouseExitEvent3D(_currentHoverObject, _currentHoverObject.transform.position));
                _currentHoverObject = null;
            }
        }
    }

    void CheckMouseButtons()
    {
        // MouseDown için obje gerekli
        if (_currentHoverObject != null && Input.GetMouseButtonDown(0))
        {
            EventManager.Raise(new MouseDownEvent3D(_currentHoverObject, _currentHoverObject.transform.position));
        }

        // MouseUp her zaman gönderilir
        if (Input.GetMouseButtonUp(0))
        {
            // Sadece null değilse transform pozisyonunu al
            Vector3 pos = _currentHoverObject != null ? _currentHoverObject.transform.position : Vector3.zero;
            EventManager.Raise(new MouseUpEvent3D(_currentHoverObject, pos));

            // MouseUp sonrası hover sıfırlanır
            _currentHoverObject = null;
        }
    }

}
