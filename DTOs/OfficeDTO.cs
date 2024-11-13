namespace EduConsultant.DTOs
{
    public class OfficeDTO
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public string Timings { get; set; }
        public string Phone { get; set; }
        public long ManagerId { get; set; }
    }

    public class OfficePostDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public string Timings { get; set; }
        public string Phone { get; set; }
        public long ManagerId { get; set; }
        public ReadShortUserDto Manager { get; set; }
    }
    public class ReadOfficeDTO
    {
        public OfficeDTO Office { get; set; }
        public List<ReadShortUserDto> Admins { get; set; }
    }
}
