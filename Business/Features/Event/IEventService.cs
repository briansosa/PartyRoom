using System.Collections.Generic;
using Domain;
using DTO;

namespace Business
{
    public interface IEventService
    {
        int Add(DtoEventRequest eventModel);
        void Delete(int id);
        int Put(DtoEventRequest eventModel);
        List<DtoEventResponse> Get();
    }
}