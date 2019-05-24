using GameStore.BLL.DTO;
using System.Collections.Generic;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Interfaces
{
    public interface IGameEditor 
    {
        void KeyActivity(int keyId);
        void UpdateGame(GameDTO game);
        void DeleteGame(int? id);
        void AddGame(GameDTO game);
        IEnumerable<GameDTO> Search(string name);
        IEnumerable<KeyDTO> GetKeysByGameId(int id);
        void AddKey(KeyDTO keyDTO);
        void DeleteKey(int id);
        void Dispose();
    }
}
