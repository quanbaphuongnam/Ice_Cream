using IceCream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Services
{
    public interface ServiceAccountService
    {
      
        List<ServiceAccount> FindAll();
        public ServiceAccount Create(ServiceAccount serviceAccount);
        void Delete(int id);
        ServiceAccount FindAccId(int AccId);

    }
}
