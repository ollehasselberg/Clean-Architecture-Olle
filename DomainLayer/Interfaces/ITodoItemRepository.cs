using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DomainLayer.Entities;

namespace DomainLayer.Interfaces
{
    // Repository Pattern: definieras i Domain, implementeras i Infrastructure - ApplicationLayer känner bara till detta interface
    public interface ITodoItemRepository
    {
        Task<List<TodoItem>> GetAllAsync();
        Task<TodoItem?> GetByIdAsync(int id);

        Task AddAsync(TodoItem item);
        Task UpdateAsync(TodoItem item);
        Task DeleteAsync(TodoItem item);
    }
}
