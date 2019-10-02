using System;
using System.ComponentModel.DataAnnotations;

namespace ClassStudent.DTO.RoomDto
{
    public class RoomDto
    {
        public int? Id { get; set; }

        [Range(1, Double.MaxValue)]
        public int Number { get; set; }
    }
}
