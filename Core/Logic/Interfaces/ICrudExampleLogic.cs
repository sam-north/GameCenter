using Core.Models;
using System.Collections.Generic;

namespace Core.Logic.Interfaces
{
    public interface ICrudExampleLogic
    {
        List<CRUDExample> Get();
        CRUDExample Get(int id);
        CRUDExample Save(CRUDExample modelToSave);
        void Delete(int id);
    }
}
