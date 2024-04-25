namespace AttendanceTracker.Domain
{
    public class Attends
    {
        public Guid LessonId { get; set; }
        public string Index { get; set; }
        public Lesson? Lesson { get; set; }
        public Student? Student { get; set; }    
    }
}
