using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rest_api_m1.Domains.Repositories
{
    public interface IUnityOfWork
    {
        Task CompleteAsync();
    }
}
