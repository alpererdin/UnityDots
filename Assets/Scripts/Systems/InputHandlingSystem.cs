using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial class InputHandlingSystem : SystemBase
{
    private DOTSDemo _demoBindings;
    private bool isFiring;

    protected override void OnCreate()
    {
        _demoBindings = new DOTSDemo();
        
    }

    protected override void OnStartRunning()
    {
        _demoBindings.Enable();
        _demoBindings.DemoBindings.PlayerFire.performed += PlayerFire_performed;
        _demoBindings.DemoBindings.GetRandomInt.performed += GetRandomInt_performed;
        _demoBindings.DemoBindings.GetRandomFloat.performed += GetRandomFloat_performed;

    }

 

    private void GetRandomInt_performed(InputAction.CallbackContext obj)
    {
        CentralRandomComponent rand = SystemAPI.GetSingleton<CentralRandomComponent>();
        Debug.Log("getint :"+ rand.Rand.NextInt(100));
        SystemAPI.SetSingleton(rand);
    }
    private void GetRandomFloat_performed(InputAction.CallbackContext obj)
    { 
        CentralRandomComponent rand = SystemAPI.GetSingleton<CentralRandomComponent>();
        Debug.Log("getint :"+ rand.Rand.NextFloat(0f,100f));
        SystemAPI.SetSingleton(rand);

    }
    private void PlayerFire_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        isFiring = true;
        
    }
    

    protected override void OnUpdate()
    {
        PlayerMoveInput input;
        if (SystemAPI.TryGetSingleton<PlayerMoveInput>(out input))
        {
            input.PlayerMove = _demoBindings.DemoBindings.PlayerMove.ReadValue<Vector2>();
            input.Firing = isFiring;
            SystemAPI.SetSingleton<PlayerMoveInput>(input);
        }

        isFiring = false;

    }

    protected override void OnStopRunning()
    {
        _demoBindings.DemoBindings.PlayerFire.performed += PlayerFire_performed;
        _demoBindings.Disable();
    }
}
