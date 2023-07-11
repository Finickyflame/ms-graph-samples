using ConsoleApp;
using Microsoft.Graph.Models;
using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.TokenCacheProviders.InMemory;

HostApplicationBuilder builder = Host.CreateApplicationBuilder();

// Register services needed for Microsoft.Graph to work properly
builder.Services.AddHttpClient();
builder.Services.AddTokenAcquisition();
builder.Services.AddInMemoryTokenCaches();
builder.Services.AddOptions<MicrosoftIdentityApplicationOptions>().Bind(builder.Configuration.GetSection("AzureAd"));
builder.Services.AddMicrosoftGraph(options =>
{
    // All calls will be on behalf of the calling service itself and not on behalf of a user
    options.RequestAppToken = true;
});

// Custom service registered
builder.Services.AddScoped<CustomGraphService>();


IHost host = builder.Build();

// Classes that depends on GraphServiceClient must be retrieved inside a scope
await using AsyncServiceScope scope = host.Services.CreateAsyncScope();

var client = scope.ServiceProvider.GetRequiredService<CustomGraphService>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();


UserCollectionResponse? users = await client.GetUsers();
foreach (User user in users!.Value)
{
    logger.LogInformation("User found: (UserId: {UserId}, DisplayName: {UserDisplayName})", user.Id, user.DisplayName);
}

// If you wish to retrieve a specific user:
// User? user = await client.GetUser(userId: "c8b0aab8-70d2-4ee8-bf0e-eb59a6430382");
// logger.LogInformation("User found: (UserId: {UserId}, DisplayName: {UserDisplayName})", user.Id, user.DisplayName);


TodoTaskListCollectionResponse? todoTaskLists = await client.GetUserTodoTaskLists("c8b0aab8-70d2-4ee8-bf0e-eb59a6430382");
foreach (TodoTaskList todoTaskList in todoTaskLists!.Value)
{
    logger.LogInformation("Todo Task List found: (Id: {TodoTaskListId}, DisplayName: {TodoTaskListName})", todoTaskList.Id, todoTaskList.DisplayName);
}

// If you wish to create a task for a specific user and list:
// var todoTask = new TodoTask
// {
//     Title = "My new task"
// };
// TodoTask? result = await client.CreateTodoTask(userId: "c8b0aab8-70d2-4ee8-bf0e-eb59a6430382", todoListId: "fdc481f3-fd73-40b8-96b9-60e6f5cbb263", todoTask);
// logger.LogInformation("TodoTask Created: {Id}", result?.Id);