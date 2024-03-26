using System;
namespace ClientObserver.Models.Interfaces.Connection;

/// <summary>
/// Delegate definition for a connection step. Must return a boolean indicating the success of the step.
/// </summary>
public delegate Task<bool> ConnectionStep();

/// <summary>
/// Delegate definition for a disconnection step. Must return a boolean indicating the success of the step.
/// </summary>
public delegate Task<bool> DisconnectionStep();

public interface IConnectionService
{
    // Properties and methods that implementing classes must provide.
    //BaseClientModel ClientModel { get; set; }
    Task<bool> InitializeConnection();
    Task<bool> Authenticate();
    Task<bool> FinalizeConnection();

    Task<bool> DisconnectFromClient();

    Task<bool> ConnectAsync();
    Task DisconnectAsync();

    // Use these methods or properties to expose the steps.
    // Note: Implementing classes will have to define how these are structured.
    ConnectionStep[] ConnectionSteps { get; }
    DisconnectionStep[] DisconnectionSteps { get; }
}

