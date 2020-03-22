using System;

namespace DTO
{
    public class DtoEventResponse
    {
        public string Name { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateFinish { get; set; }
        public TimeSpan Duration => DateFinish.Subtract(DateStart);
    }

    public class DtoEventRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateFinish { get; set; }
    }
}