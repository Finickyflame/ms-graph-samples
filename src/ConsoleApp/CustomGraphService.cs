using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace ConsoleApp;

/// <summary>
/// A facade for the <see cref="GraphServiceClient"/> to simplifies the calls we want to use.
/// </summary>
public sealed class CustomGraphService
{
    private readonly GraphServiceClient _client;
    private readonly ILogger<CustomGraphService> _logger;

    public CustomGraphService(GraphServiceClient client, ILogger<CustomGraphService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<User?> GetUser(string userId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("GetUser({UserId})", userId);
        try
        {
            return await _client.Users[userId].GetAsync(cancellationToken: cancellationToken);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "GetUser failed");
            throw;
        }
    }
    
    public async Task<UserCollectionResponse?> GetUsers(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("GetUsers()");
        try
        {
            return await _client.Users.GetAsync(cancellationToken: cancellationToken);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "GetUsers failed");
            throw;
        }
    }

    public async Task<TodoTaskListCollectionResponse?> GetUserTodoTaskLists(string userId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("GetTodoLists({UserId})", userId);
        try
        {
            return await _client.Users[userId].Todo.Lists.GetAsync(cancellationToken: cancellationToken);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "GetUserTodoTaskLists failed");
            throw;
        }
    }

    public async Task<TodoTask?> CreateTodoTask(string userId, string todoListId, TodoTask todoTask, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("CreateTodoTask({UserId}, {TodoListId})", userId, todoListId);
        try
        {
            return await _client.Users[userId].Todo.Lists[todoListId].Tasks.PostAsync(todoTask, cancellationToken: cancellationToken);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "CreateTodoTask failed");
            throw;
        }
    }
}