using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.BLL.Services
{
    public class GameEditor: IGameEditor
    {
        IUnitOfWork rep { get; set; }

        public GameEditor(IUnitOfWork uow)
        {
            rep = uow;
        }

        public void Dispose()
        {
            rep.Dispose();
        }
        #region Game
        public void UpdateGame(GameDTO gameDTO)
        {
            var game = rep.Games.Get(gameDTO.Id);
            if (game == null)
            {
                throw new ValidationException("Игра не найдена", "");
            }
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<GameDTO, Game>()).CreateMapper();
            rep.Games.Update(mapper.Map<GameDTO, Game>(gameDTO));
            rep.Save();
        }

        public void DeleteGame(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Не установлено id игры", "");
            }
            var game = rep.Games.Get(id.Value);
            if (game == null)
            {
                throw new ValidationException("Игра не найдена", "");
            }
            rep.Games.Delete(game.Id);
            rep.Save();
        }

        public void AddGame(GameDTO game)
        {
            rep.Games.Create(new Game
            {
                Name = game.Name,
                Description = game.Description,
                Genre = game.Genre,
                Platform = game.Platform,
                Price = game.Price,
                SystemRequirement = game.SystemRequirement
            });
            rep.Save();
        }
        
        public IEnumerable<GameDTO> Search(string name)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Game, GameDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Game>, List<GameDTO>>(rep.Games.Search(name));
        }
        #endregion

        #region GameKey
        /// <summary>
        /// поиск ключей к определенной игре
        /// </summary>
        /// <param name="id">Id игры</param>
        /// <returns></returns>
        public IEnumerable<KeyDTO> GetKeysByGameId(int id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Key,KeyDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Key>,List<KeyDTO>>(rep.Keys.GetAll().Where(k => k.GameId == id));
        }

        public void AddKey(KeyDTO keyDTO)
        {
            var mapper = new MapperConfiguration(cfg=> cfg.CreateMap<KeyDTO,Key>()).CreateMapper();
            var key = mapper.Map<KeyDTO, Key>(keyDTO);
            rep.Keys.Create(key);
            rep.Save();
        }

        public void DeleteKey(int id)
        {
            rep.Keys.Delete(id);
            rep.Save();
        }

        /// <summary>
        /// Смена состаяния ключа на не активный
        /// </summary>
        /// <param name="keyId"></param>
        public void KeyActivity(int keyId)
        {
            Key key = rep.Keys.GetAll().Where(k=> k.Id == keyId).First();
            key.Active = true;
            rep.Keys.Update(key);
        }
        #endregion
    }
}
