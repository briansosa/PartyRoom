using System.Collections.Generic;
using Common.Functional;
using Domain;
using DTO;

namespace Business
{
    public interface IEventService
    {
        int Add(DtoEventRequest eventModel);
        Result Delete(int id);
        Result<int> Put(DtoEventRequest eventModel);
        Result<List<DtoEventResponse>> Get();
    }
}