#nullable enable

using Configs;
using Data;
using EnvLevel;
using Locations;
using MergeLogic;
using ScreenInteractions;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main Instance { get; private set; } = null!;

    public IClickManager ClickManager => _clickManager;
    
    public GeneralConfig GeneralConfig => _generalConfig;
    
    public MergeManager MergeManager => _mergeManager;
    
    [SerializeField]
    private Camera _mainCamera = null!;
    
    [SerializeField]
    private GeneralConfig _generalConfig = null!;
    
    [SerializeField]
    private EnvData _envData = null!;
    
    [SerializeField]
    private CubeSpawner _cubeSpawner = null!;
    
    [SerializeField]
    private CubeView _cubeViewPrefab = null!;
    
    private ClickManager _clickManager = null!;
    private MergeManager _mergeManager = null!;
    private GameLocation _gameLocation = null!;
    
    private void Awake()
    {
        Instance = this;
        var cubeStorage = new CubeSceneStorage();
        _mergeManager = new MergeManager(cubeStorage);
        _gameLocation = new GameLocation(_mainCamera, _envData, _cubeSpawner, _cubeViewPrefab, cubeStorage);
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
