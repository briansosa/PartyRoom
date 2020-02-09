using System;

namespace DTO
{
    public class DtoEventResponse
    {
        public string Name { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateFinish { get; set; }
    }

    public class DtoEvent
    {
        public string Name { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateFinish { get; set; }
    }
}