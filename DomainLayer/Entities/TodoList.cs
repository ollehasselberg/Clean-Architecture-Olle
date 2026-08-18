using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class TodoList
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        // Navigation property (relation)
        public List<TodoItem> Items { get; set; } = new();
    }
}
