#nullable enable

using Configs;
using EnvLevel;
using Locations;
using ScreenInteractions;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main Instance { get; private set; } = null!;

    public IClickManager ClickManager => _clickManager;
    
    public GeneralConfig GeneralConfig => _generalConfig;
    
    [SerializeField]
    private Camera _mainCamera = null!;
    
    [SerializeField]
    private GeneralConfig _generalConfig = null!;
    
    [SerializeField]
    private EnvData _envData = null!;
    
    private ClickManager _clickManager = null!;
    private GameLocation _gameLocation = null!;
    
    private void Awake()
    {
        Instance = this;

        _gameLocation = new GameLocation(_mainCamera, _envData);
        _clickManager = new ClickManager(_mainCamera);
    }

    private void Start()
    {
        _gameLocation.Load();
    }

    private void Update()
    {
        _clickManager.Update();
    }

    private void OnDestroy()
    {
        _gameLocation.Dispose();
    }
}
