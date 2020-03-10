using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Contracts
{
    public interface IEventRepository
    {
        int Add(Event eventModel);
        void Delete(int id);
        int Put(Event eventModel);
        List<Event> GetAll();
    } 
}
