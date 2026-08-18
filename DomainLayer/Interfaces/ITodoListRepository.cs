using DomainLayer.Entities;

namespace DomainLayer.Interfaces
{
    // Repository Pattern för TodoList, samma upplägg som ITodoItemRepository
    public interface ITodoListRepository
    {
        Task<List<TodoList>> GetAllAsync();
        Task<TodoList?> GetByIdAsync(int id);

        Task AddAsync(TodoList list);
    }
}
