using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniHttpServer.Server.Common
{
    public interface IServiceCollection
    {
        IServiceCollection Add<TService, TImplementation>() where TService : class
        where TImplementation : TService;

        IServiceCollection Add<TService>()
            where TService : class;

        TService Get<TService>() where TService : class;

        object CreateInstance(Type serviceType);
    }
}
