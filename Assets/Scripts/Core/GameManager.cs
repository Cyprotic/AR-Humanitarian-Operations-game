using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public static GameState State => Instance._gameState;
    [SerializeField] private GameState _gameState;

    private void Start()
    {
        Instance = GetComponent<GameManager>();
        
        _gameState.SetState(GameState.EGameState.Pregame);
    }
}
