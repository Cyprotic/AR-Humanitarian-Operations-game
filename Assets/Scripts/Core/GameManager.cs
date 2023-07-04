using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ARPlaneManager))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public static GameState State => Instance._gameState;
    [SerializeField] private GameState _gameState;
    public OptionsSetup optionsSetup;

    public List<GameObject> NumbersList = new();
    public GameObject instantiatedNumber;

    public ARPlaneManager arPlaneManager;
    private Camera _camera;
    private ARPlane _arPlane;

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    private void Start()
    {
        Debug.Log("GameManager started");
        arPlaneManager = GetComponent<ARPlaneManager>();  
        _camera = Camera.main;  
        
        
        _gameState.SetState(GameState.EGameState.Game);
    }
    
    public void SpawnNumber(ARPlanesChangedEventArgs plane)
    {
        arPlaneManager.planesChanged -= SpawnNumber;
        int randomNumber = Random.Range(0, NumbersList.Count - 1);
        GameObject number = NumbersList[randomNumber];
        
        foreach (var p in plane.added)
        {
            var transformNumber = p.transform;
            instantiatedNumber = Instantiate(number, transformNumber.position, transformNumber.rotation);
            
            // Project cameraForward onto the plane, to get our target.
            Vector3 yTarget = Camera.main.transform.forward - (transform.forward * Vector3.Dot(Camera.main.transform.forward, transform.forward));
	
            // Find the needed rotation to rotate y to y-target
            Quaternion desiredRotation = Quaternion.LookRotation(transform.forward, yTarget);
	 
            // Apply that rotation
            instantiatedNumber.transform.rotation = desiredRotation;
            instantiatedNumber.transform.Rotate(90,0,0);
            _arPlane = p;
        }
        NumbersList.Remove(number);
    }
    
    public void SpawnNumber()
    {
        int randomNumber = Random.Range(0, NumbersList.Count - 1);
        GameObject number = NumbersList[randomNumber];
        
        var transformNumber = _arPlane.transform;
        instantiatedNumber = Instantiate(number, transformNumber.position, transformNumber.rotation);
        
        // Project cameraForward onto the plane, to get our target.
        Vector3 yTarget = _camera.transform.forward - (transform.forward * Vector3.Dot(_camera.transform.forward, transform.forward));

        // Find the needed rotation to rotate y to y-target
        Quaternion desiredRotation = Quaternion.LookRotation(transform.forward, yTarget);
 
        // Apply that rotation
        instantiatedNumber.transform.rotation = desiredRotation;
        instantiatedNumber.transform.Rotate(90,0,0);
            
        NumbersList.Remove(number);
    }
}
