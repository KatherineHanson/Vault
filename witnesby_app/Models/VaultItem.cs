using System;
namespace WitnesbyApp.Models
{
    public class VaultItem
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get { return DateTime.Now; } }
    }
}
