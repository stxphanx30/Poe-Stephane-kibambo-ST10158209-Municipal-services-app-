
using System;

namespace Municipal_services_app.Models
{
    public class SearchTerm
    {
        public int Id { get; set; }
        public string Term { get; set; } = null!;
        public int Count { get; set; }
        public DateTime LastSearched { get; set; }
    }
}
