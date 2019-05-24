using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Entities
{
    public class Order
    {
        public int id { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public DateTime DateTime { get; set; }
        public int KeyId { get; set; }
        public Key Key { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }       
    }
}
