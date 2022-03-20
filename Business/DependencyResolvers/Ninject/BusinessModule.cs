using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Ninject.Modules;

namespace Business.DependencyResolvers.Ninject
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<INoteDal>().To<EfNoteDal>().InSingletonScope();
            Bind<INoteService>().To<NoteManager>().InSingletonScope();

            Bind<IFolderDal>().To<EfFolderDal>().InSingletonScope();
            Bind<IFolderService>().To<FolderManager>().InSingletonScope();
        }
    }
}