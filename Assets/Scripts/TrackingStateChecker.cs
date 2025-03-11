using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TrackingStateChecker : MonoBehaviour
{
    [SerializeField] InputActionProperty trackingStateInput;
    [SerializeField] InputActionProperty moveInput;

    [SerializeField] bool inputIsEnabled;
    [SerializeField] int currentTrackingStateValue;

    TrackingStates m_CurrentTrackingState = TrackingStates.Position | TrackingStates.Rotation;


    private void Start()
    {
        var action = trackingStateInput.action;
        var moveAction = moveInput.action;
        if (action == null) return;

        print($"Setting Tracking State");
        action.performed += OnTrackingStatePerformed;
        action.canceled += OnTrackingStateCanceled;

        if (moveAction == null) return;

        moveAction.performed += OnMove;
    }
    private void Update()
    {
        inputIsEnabled = trackingStateInput.action.enabled;
        currentTrackingStateValue = trackingStateInput.action.ReadValue<int>();
    }

    void OnTrackingStatePerformed(InputAction.CallbackContext context)
    {
        //This never seems to run
        m_CurrentTrackingState = (TrackingStates)context.ReadValue<int>();

        print($"Tracking State: {m_CurrentTrackingState}");
    }

    void OnTrackingStateCanceled(InputAction.CallbackContext context)
    {
        m_CurrentTrackingState = TrackingStates.None;
        print($"Tracking State Canceled...");
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        print($"Move Value: {context.ReadValue<Vector2>()}");
    }   
}
[Flags]
enum TrackingStates
{
    /// <summary>
    /// Position and rotation are not valid.
    /// </summary>
    None,

    /// <summary>
    /// Position is valid.
    /// See <c>InputTrackingState.Position</c>.
    /// </summary>
    Position = 1 << 0,

    /// <summary>
    /// Rotation is valid.
    /// See <c>InputTrackingState.Rotation</c>.
    /// </summary>
    Rotation = 1 << 1,
}
