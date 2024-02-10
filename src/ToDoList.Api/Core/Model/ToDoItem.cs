namespace ToDoList.Api.Core.Model;

public sealed class ToDoItem
{
    public DateTimeOffset CreatedAt { get; set; }
    public string Name { get; set; }
    public bool IsComplete { get; set; }
    public DateTimeOffset? CompletedAt { get; set; }
}