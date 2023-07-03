using UnityEngine;
 

public class GameState : MonoBehaviour
{
    public enum EGameState { Pregame, Loading, Intro, Game, Outro, Postgame }

    public EGameState Previous { get; set; }
    public EGameState Current { get; set; }
    
    
    protected StateMachine<EGameState> StateMachine = new();
    
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
		Debug.Log("GameStateStarted");
		StateMachine[EGameState.Pregame].onEnter = prev =>
		{
			Debug.Log("Pregame");
			
		};
		
		StateMachine[EGameState.Pregame].onUpdate = () =>
		{
			
		};

		StateMachine[EGameState.Pregame].onExit = next =>
		{
			Debug.Log("here?");
		};

		StateMachine[EGameState.Loading].onEnter = prev =>
		{
			Debug.Log("Loading");
			
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
			Debug.Log("Game");
			GameManager.Instance.arPlaneManager.planesChanged += GameManager.Instance.SpawnNumber;
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
