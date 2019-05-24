using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.DTO
{
    public class ImageDTO
    {
        public int GameId { get; set; }
        public string Position { get; set; }
        public string FileName { get; set; }
        public byte[] ImageData { get; set; }
    }
}
