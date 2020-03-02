using Xunit;
using FluentAssertions;
using Moq;
using Business;
using DTO;
using System;
using Domain;
using DatabaseInterface;
using System.Collections.Generic;

namespace BusinessTest
{
    public class EventServiceTest
    {
        public Mock<IEventRepository> IEventRepositoryMock = new Mock<IEventRepository>();

        [Fact]
        public void EventService_Add_Return_1(){
            // Arrange
            DtoEvent dto = new DtoEvent(){
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
        public void EventService_GetAllEventForRepository()
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
            IEventRepositoryMock.Setup(p => p.Get()).Returns(listEvents);
            EventService eventService = new EventService(IEventRepositoryMock.Object);
            // Act
            List<DtoEventResponse> listDtos = eventService.Get();
            // Assert
            listDtos.Should().BeEquivalentTo(listResponse);
        }
    }
}
