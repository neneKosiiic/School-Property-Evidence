using System;

namespace SchoolPropertyEvidence.Models
{
    public class RoomWithCountsDto
    {
        public int Id { get; set; }
        public string RoomName { get; set; } = string.Empty;
        public string ResponsibleFirstName { get; set; } = string.Empty;
        public string ResponsibleLastName { get; set; } = string.Empty;

        public int Computers { get; set; }
        public int Furniture { get; set; }
        public int Electronics { get; set; }
        public int LearningEquipment { get; set; }
    }
}