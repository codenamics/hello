using System.Threading.Tasks;
using API.Entity;

namespace API.Interfaces
{
    public interface IListRepository
    {
     
        Task<List> GetListAsync (int id);
        void UpdateList (List list);
        void DeleteList (List list);
        
    }
}