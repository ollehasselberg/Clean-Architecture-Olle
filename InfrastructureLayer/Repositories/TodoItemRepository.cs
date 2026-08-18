using DomainLayer.Entities;
using DomainLayer.Interfaces;
using InfrastructureLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Repositories
{
    // Konkret implementation av ITodoItemRepository, pratar direkt med EF Core / AppDbContext
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly AppDbContext _context;

        public TodoItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TodoItem>> GetAllAsync()
        {
            return await _context.TodoItems
                .Include(x => x.TodoList)
                .ToListAsync();
        }

        public async Task<TodoItem?> GetByIdAsync(int id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task AddAsync(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TodoItem item)
        {
            _context.TodoItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TodoItem item)
        {
            _context.TodoItems.Remove(item);
            await _context.SaveChangesAsync();
        }

    }
}