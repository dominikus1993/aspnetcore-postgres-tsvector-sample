namespace ToDoList.Api.Core.Model;

public sealed class ToDos
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<ToDoItem>? Items { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}