using GameStore.BLL.DTO;
using System.Collections.Generic;

namespace GameStore.BLL.Interfaces
{
    public interface IImageEditor
    {
        IEnumerable<ImageDTO> GetImages();
        IEnumerable<ImageDTO> GetGameImagesById(int? id);
        void DeleteImage(int? id);
        void AddImage(ImageDTO image);
        void UpdateImage(ImageDTO image);
        void Dispose();
    }
}
