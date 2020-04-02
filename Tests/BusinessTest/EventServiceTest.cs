using Xunit;
using FluentAssertions;
using Moq;
using Business;
using DTO;
using System;
using Domain.Entities;
using Domain.Contracts;
using System.Collections.Generic;
using Common.Functional;

namespace BusinessTest
{
    public class EventServiceTest
    {
        public Mock<IEventRepository> IEventRepositoryMock = new Mock<IEventRepository>();

        [Fact]
        public void Add_Return_1(){
            // Arrange
            DtoEventRequest dto = new DtoEventRequest(){
                Name = "Pepe",
                DateStart = DateTime.Now,
                DateFinish = DateTime.Now.AddDays(1)  
            };
            IEventRepositoryMock.Setup(p => p.Add(It.IsAny<Event>())).Returns(1);
            EventService eventServiceTest = new EventService(IEventRepositoryMock.Object);

            // Act
            int id = eventServiceTest.Add(dto);
            // Assert
            id.Should().Be(1);
        }

        [Fact]
        public void Get_GetAllEventForRepositorySuccess()
        {
            // Arrange
            DateTime start = DateTime.Now;
            DateTime finish = DateTime.Now.AddDays(1);
            List<Event> listEvents = new List<Event>();
            listEvents.Add(new Event(){
                Name = "Pepe",
                DateStart = start,
                DateFinish = finish
            });
            List<DtoEventResponse> listResponse = new List<DtoEventResponse>();
            listResponse.Add(new DtoEventResponse 
                {
                    Name = "Pepe",
                    DateStart = start,
                    DateFinish = finish
                });
            var resultList = Result.SuccessWithReturnValue(listEvents);
            IEventRepositoryMock.Setup(p => p.GetAll()).Returns(resultList);
            EventService eventService = new EventService(IEventRepositoryMock.Object);
            // Act
            Result<List<DtoEventResponse>> response = eventService.Get();
            // Assert
            if (response.IsSuccess)
                response.Value.Should().BeEquivalentTo(listResponse);
        }

        [Fact]
        public void Delete_CheckCallDeleteMethodFromRepository()
        {
            // Arrange
            int id = 3;
            IEventRepositoryMock.Setup(p => p.Delete(It.IsAny<int>()));
            EventService eventService = new EventService(IEventRepositoryMock.Object);
            // Act
            eventService.Delete(id);
            // Assert
            IEventRepositoryMock.Verify(m => m.Delete(It.IsAny<int>()), Times.Once);
        }
    }
}
