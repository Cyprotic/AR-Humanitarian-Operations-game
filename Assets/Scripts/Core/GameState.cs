using UnityEngine;
 

public class GameState : MonoBehaviour
{
    public enum EGameState { Pregame, Loading, Intro, Game, Choice, Postgame, GameOver }

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
			
			// Voice over
			
		};

		StateMachine[EGameState.Game].onUpdate = () =>
		{
			if(GameManager.Instance.instantiatedNumber != null)
			{
				SetState(EGameState.Choice);
			}
		};

		StateMachine[EGameState.Choice].onEnter = prev =>
		{
			GameManager.Instance.optionsSetup.gameObject.SetActive(true);
		};

		StateMachine[EGameState.Choice].onExit = next =>
		{
			Destroy(GameManager.Instance.instantiatedNumber);
			GameManager.Instance.optionsSetup.gameObject.SetActive(false);
		}; 

        StateMachine[EGameState.Postgame].onEnter = prev =>
        {
	        if (GameManager.Instance.NumbersList.Count == 0)
		        SetState(EGameState.GameOver);
			GameManager.Instance.SpawnNumber();
        };

		StateMachine[EGameState.Postgame].onUpdate = () =>
		{
			if(GameManager.Instance.instantiatedNumber != null)
			{
				SetState(EGameState.Choice);
			}
		};

		StateMachine[EGameState.Postgame].onExit = next =>
		{
			
		};
		StateMachine[EGameState.GameOver].onEnter = prev =>
		{
			Debug.Log("GAME IS DONE");
		};

		StateMachine[EGameState.GameOver].onUpdate = () =>
		{
			
		};

		StateMachine[EGameState.GameOver].onExit = next =>
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
