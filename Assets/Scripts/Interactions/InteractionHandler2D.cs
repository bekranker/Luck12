using UnityEngine;
using Zenject;

public class InteractionHandler2D : MonoBehaviour, IInitializable
{
    [SerializeField] private LayerMask _mask;
    [SerializeField] private Camera _camera;

    private GameObject _currentHoverObject;

    public void Initialize()
    {
        print("Interaction Handler 2D Initialized");

    }

    void Update()
    {
        Raycast();
        CheckMouseButtons();
    }

    void Raycast()
    {
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 100f, _mask);

        if (hit.collider != null)
        {
            GameObject hitObject = hit.collider.gameObject;

            if (_currentHoverObject != hitObject)
            {
                if (_currentHoverObject != null)
                {
                    EventManager.Raise(new MouseExitEvent2D(_currentHoverObject, _currentHoverObject.transform.position));
                }

                _currentHoverObject = hitObject;
                EventManager.Raise(new MouseEnterEvent2D(hitObject, hitObject.transform.position));
            }
        }
        else
        {
            if (_currentHoverObject != null)
            {
                EventManager.Raise(new MouseExitEvent2D(_currentHoverObject, _currentHoverObject.transform.position));
                _currentHoverObject = null;
            }
        }
    }

    void CheckMouseButtons()
    {
        if (_currentHoverObject == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            EventManager.Raise(new MouseDownEvent2D(_currentHoverObject, _currentHoverObject.transform.position));
        }

        if (Input.GetMouseButtonUp(0))
        {
            EventManager.Raise(new MouseUpEvent2D(_currentHoverObject, _currentHoverObject.transform.position));
        }
    }
}
