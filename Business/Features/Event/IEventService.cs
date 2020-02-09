using System.Collections.Generic;
using Domain;
using DTO;

namespace Business
{
    public interface IEventService
    {
        int Add(DtoEvent eventModel);
        void Delete(int id);
        void Put(DtoEvent eventModel);
        List<DtoEventResponse> Get();
    }
}