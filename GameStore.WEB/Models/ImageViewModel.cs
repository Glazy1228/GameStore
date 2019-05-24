using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.WEB.Models
{
    public class ImageViewModel
    {
        public int GameId { get; set; }
        public string Position { get; set; }
        public string FileName { get; set; }
        public byte[] ImageData { get; set; }
    }
}