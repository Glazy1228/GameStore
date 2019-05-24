using GameStore.BLL.Interfaces;
using Ninject.Modules;

namespace GameStore.WEB.Util
{
    public class OrderModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IServices>().To<GameStore.BLL.Services.Services>();            
        }
    }
}