using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Entities
{
    public class Image
    {
        public int id { get; set; }
        public int? GameId { get; set; }
        public Game Game { get; set; }
        public string Position { get; set; }
        public string FileName { get; set; }
        public byte[] ImageData { get; set; }

       
    }
}
