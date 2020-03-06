using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Contracts
{
    public interface IEventRepository
    {
        int Add(Event eventModel);
        void Delete(int id);
        void Put(Event eventModel);
        List<Event> Get();
    } 
}
