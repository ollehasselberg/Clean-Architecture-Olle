using DomainLayer.Entities;

namespace DomainLayer.Interfaces
{
    public interface ITodoListRepository
    {
        Task<List<TodoList>> GetAllAsync();
        Task<TodoList?> GetByIdAsync(int id);

        Task AddAsync(TodoList list);
    }
}
