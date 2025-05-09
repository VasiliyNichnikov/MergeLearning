#nullable enable

using ScreenInteractions;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main Instance { get; private set; } = null!;

    public IClickManager ClickManager => _clickManager;
    
    [SerializeField]
    private Camera _mainCamera = null!;
    
    private ClickManager _clickManager = null!;
    
    private void Awake()
    {
        Instance = this;
        
        _clickManager = new ClickManager(_mainCamera);
    }

    private void Update()
    {
        _clickManager.Update();
    }
}
