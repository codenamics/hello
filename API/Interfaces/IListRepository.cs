using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entity;

namespace API.Interfaces
{
    public interface IListRepository
    {
        Task<List> GetListAsync (Guid id);
        void UpdateList (List list); 
    
        void DeleteList (List list);
        
    }
}