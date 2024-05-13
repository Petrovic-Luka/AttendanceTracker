namespace AttendanceTracker.DTO
{
    public class LessonCreateDTO
    {
        public int ProfessorId { get; set; }
        public int SubjectId { get; set; }
        public int ClassRoomId { get; set; }
        public DateTime Time { get; set; }
    }
}
