using UnityEngine;
using UnityEngine.XR.ARFoundation;
 
[RequireComponent(typeof(ARPlaneManager))]
public class GameState : MonoBehaviour
{
    public enum EGameState { Pregame, Loading, Intro, Game, Outro, Postgame }

    public EGameState Previous { get; set; }
    public EGameState Current { get; set; }
    
    
    protected StateMachine<EGameState> StateMachine = new();
    
    private ARPlaneManager _arPlaneManager;

    [SerializeField] private GameObject prefab;

    private void Awake()
    {
	    _arPlaneManager = GetComponent<ARPlaneManager>();  
    }

	// private IEnumerator GoBackToMainMenu()
	// {
	// 	Task task = GameManager.Instance.Runner.Shutdown();
	// 	while (!task.IsCompleted)
	// 	{
	// 		yield return null;
	// 	}
	// 	SceneManager.LoadScene("Menu");
 //        UIScreen.activeScreen.BackTo(InterfaceManager.Instance.mainScreen);
 //        UIScreen.Focus(InterfaceManager.Instance.mainScreen);
	// 	//UIScreen.BackToInitial();
	// }

	private void Start()
	{
		StateMachine[EGameState.Pregame].onEnter = prev =>
		{
			Debug.Log("Pregame");
			if (prev == EGameState.Postgame)
			{
				//Menu?
			}
			_arPlaneManager.planesChanged += SpawnNumber;
		};

		StateMachine[EGameState.Pregame].onExit = next =>
		{
			Debug.Log("here?");
		};

		StateMachine[EGameState.Loading].onEnter = prev =>
		{
			
		};

		StateMachine[EGameState.Loading].onUpdate = () =>
		{
			
		};

		StateMachine[EGameState.Loading].onExit = next =>
		{
			
		};

		StateMachine[EGameState.Intro].onEnter = prev =>
		{
			
		};

		StateMachine[EGameState.Game].onEnter = prev =>
		{
			
		};

		StateMachine[EGameState.Game].onUpdate = () =>
		{
			
		};

		StateMachine[EGameState.Outro].onEnter = prev =>
		{
			
		};

		StateMachine[EGameState.Outro].onExit = next =>
		{
			
		}; 

        StateMachine[EGameState.Postgame].onEnter = prev =>
        {
           
        };

		StateMachine[EGameState.Postgame].onUpdate = () =>
		{
			
		};

		StateMachine[EGameState.Postgame].onExit = next =>
		{
			
		};
	}
	
	private void SpawnNumber(ARPlanesChangedEventArgs plane)
	{
		foreach (var p in plane.added)
		{
			Debug.Log("plane" + p);
			GameObject obj = Instantiate(prefab, p.transform.position, p.transform.rotation);
			Debug.Log("obj" + obj);
			if (obj != null)
			{
				_arPlaneManager.planesChanged -= SpawnNumber;
				break;
			}
		}
	}
	
	private void FixedUpdate()
	{
		StateMachine.Update(Current, Previous);
	}

	public void SetState(EGameState st)
	{
		if (Current == st) return;
		Debug.Log($"Set State to {st}");
		Previous = Current;
		Current = st;
	}
}
