using System.Collections.Generic;
using Domain;

namespace DatabaseInterface
{
    public interface IEventRepository
    {
        int Add(Event eventModel);
        void Delete(int id);
        void Put(Event eventModel);
        List<Event> Get();
    } 
}
