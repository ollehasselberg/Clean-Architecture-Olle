using DomainLayer.Entities;
using DomainLayer.Interfaces;
using InfrastructureLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Repositories
{
    public class TodoListRepository : ITodoListRepository
    {
        private readonly AppDbContext _context;

        public TodoListRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TodoList>> GetAllAsync()
        {
            return await _context.TodoLists
                .Include(x => x.Items)
                .ToListAsync();
        }

        public async Task<TodoList?> GetByIdAsync(int id)
        {
            return await _context.TodoLists
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(TodoList list)
        {
            _context.TodoLists.Add(list);
            await _context.SaveChangesAsync();
        }
    }
}
