using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    // "Många"-sidan av relationen: varje TodoItem hör till exakt en TodoList
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public bool IsDone { get; set; }

        // FK
        public int TodoListId { get; set; }

        // Navigation
        public TodoList TodoList { get; set; } = null!;
    }
}
