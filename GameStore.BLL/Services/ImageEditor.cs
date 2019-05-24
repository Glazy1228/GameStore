using GameStore.BLL.DTO;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System.Collections.Generic;
using AutoMapper;

namespace GameStore.BLL.Services
{
    public class ImageEditor : IImageEditor
    {
        IUnitOfWork rep { get; set; }

        public ImageEditor(IUnitOfWork uow)
        {
            rep = uow;
        }

        public void Dispose()
        {
            rep.Dispose();
        }

        public void DeleteImage(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Не установлено id изображения", "");
            }
            var image = rep.Images.Get((int)id);//проверить без (int)
            if (image == null)
            {
                throw new ValidationException("Изображение не найдена", "");
            }
            rep.Images.Delete(image.GameId);
            rep.Save();
        }

        public void AddImage(ImageDTO image)
        {
            rep.Images.Create(new Image
            {
                GameId = image.GameId,
                FileName = image.FileName,
                ImageData = image.ImageData,
                Position = image.Position
            });
            rep.Save();
        }

        public void UpdateImage(ImageDTO imageDTO)
        {
            var image = rep.Images.Get(imageDTO.GameId);
            if (image == null)
            {
                throw new ValidationException("Изображение не найдена", "");
            }
            rep.Images.Update(new Image
            {
                GameId = imageDTO.GameId,
                FileName = imageDTO.FileName,
                ImageData = imageDTO.ImageData,
                Position = imageDTO.Position
            });
            rep.Save();
        }

        /// <summary>
        /// Поиск изображений по GameId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<ImageDTO> GetGameImagesById(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id изображения", "");
            var image = rep.Images.Find(i=> i.GameId == id.Value);
            if (image == null)
                throw new ValidationException("Изображение не найден", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Image, ImageDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Image>, List<ImageDTO>>(image);
        }

        public IEnumerable<ImageDTO> GetImages()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Image, ImageDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Image>, IEnumerable<ImageDTO>>(rep.Images.GetAll());
        }
    }
}
