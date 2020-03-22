using System.Collections.Generic;
using Common.Functional;
using Domain.Entities;

namespace Domain.Contracts
{
    public interface IEventRepository
    {
        int Add(Event eventModel);
        Result Delete(int id);
        Result<int> Put(Event eventModel);
        Result<List<Event>> GetAll();
    } 
}
