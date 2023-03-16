using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image ui;

    private Transform _transform;

    public void Start()
    {
        if (UnityEngine.Camera.main != null) _transform = UnityEngine.Camera.main.transform;
    }

    public void Update()
    {
        ui.transform.LookAt(_transform);
    }
}
