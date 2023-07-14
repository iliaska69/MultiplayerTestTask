using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private bool loggingIsEnable;
    
    private IInputting _inputDevice;
    
    public static InputManager instance = null;
    
    public delegate void PlayerTryShoot();
    public event PlayerTryShoot playerTryShootEvent;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    public void RegisterInputDevice(IInputting inputDevice)
    {
        if(_inputDevice != null) Log("Device already exist! Device updated!");
        _inputDevice = inputDevice;
    }
    
    public Vector2 GetMovementDirection()
    {
        if (_inputDevice == null) return Vector2.zero;
        var movementDirection = _inputDevice.GetInputVector();
        return movementDirection;
    }

    public void HandleShoot()
    {
        playerTryShootEvent?.Invoke();
    }

    public void Update()
    {
        
    }

    private void Log(string message)
    {
        if(loggingIsEnable) Debug.Log(message);
    }
}
