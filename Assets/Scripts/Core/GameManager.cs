using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ARPlaneManager))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public static GameState State => Instance._gameState;
    [SerializeField] private GameState _gameState;

    public List<GameObject> NumbersList = new();
    
    public ARPlaneManager arPlaneManager;

    private void Awake()
    {
        arPlaneManager = GetComponent<ARPlaneManager>();  
    }

    private void Start()
    {
        Debug.Log("GameManager started");
        Instance = GetComponent<GameManager>();
        
        _gameState.SetState(GameState.EGameState.Game);
    }
    
    public void SpawnNumber(ARPlanesChangedEventArgs plane)
    {
        int randomNumber = Random.Range(0, NumbersList.Count - 1);
        GameObject number = NumbersList[randomNumber];
        
        foreach (var p in plane.added)
        {
            var transformNumber = p.transform;
            GameObject instantiatedNumber = Instantiate(number, transformNumber.position, transformNumber.rotation);
            // if (Camera.main != null)
            //     instantiatedNumber.transform.LookAt(Camera.main.transform);
            
            // Project cameraForward onto the plane, to get our target.
            Vector3 yTarget = Camera.main.transform.forward - (transform.forward * Vector3.Dot(Camera.main.transform.forward, transform.forward));
	
            // Find the needed rotation to rotate y to y-target
            Quaternion desiredRotation = Quaternion.LookRotation(transform.forward, yTarget);
	 
            // Apply that rotation
            instantiatedNumber.transform.rotation = desiredRotation;
            instantiatedNumber.transform.Rotate(90,0,0);
            
            if (instantiatedNumber != null)
            {
                arPlaneManager.planesChanged -= SpawnNumber;
                break;
            }
        }

        NumbersList.Remove(number);
    }
}
