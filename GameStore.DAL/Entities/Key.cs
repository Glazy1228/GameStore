using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Entities
{
    public class Key
    {       
        public int Id { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public string GameKey { get; set; }
        public bool Active { get; set; }

        
    }
}
