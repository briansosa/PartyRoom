using Xunit;
using FluentAssertions;
using Moq;
using Business;
using DTO;
using System;
using Domain;
using DatabaseInterface;

namespace BusinessTest
{
    public class EventServiceTest
    {
        public Mock<IEventRepository> IEventRepositoryMock = new Mock<IEventRepository>();

        [Fact]
        public void EventService_Add_1(){
            // Arrange
            DtoEvent dto = new DtoEvent(){
                Name = "Pepe",
                DateStart = DateTime.Now,
                DateFinish = DateTime.Now.AddDays(1)  
            };
            // Event eventData = new Event(){
            //     Name = "Pepe",
            //     DateStart = DateTime.Now,
            //     DateFinish = DateTime.Now.AddDays(1) 
            // };
            IEventRepositoryMock.Setup(p => p.Add(It.IsAny<Event>())).Returns(1);
            EventService eventServiceTest = new EventService(IEventRepositoryMock.Object);

            // Act
            int id = eventServiceTest.Add(dto);
            // Assert
            id.Should().Equals(12);
        }
    }
}
