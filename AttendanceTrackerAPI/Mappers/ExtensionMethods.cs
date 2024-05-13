using AttendanceTracker.Domain;
using AttendanceTracker.DTO;

namespace AttendanceTrackerAPI.Mappers
{
    public static class ExtensionMethods
    {

        public static Attends ToAttendsFromDTO(this AttendsDTO dto)
        {
            var attends=new Attends();
            attends.Index= dto.Index;
            attends.LessonId= dto.LessonId;
            return attends;
        }

        public static Lesson ToLessonFromCreateDTO(this LessonCreateDTO dto)
        {
            var lesson = new Lesson();
            lesson.ClassRoomId= dto.ClassRoomId;
            lesson.ProfessorId= dto.ProfessorId;
            lesson.SubjectId= dto.SubjectId;
            lesson.Time= dto.Time;
            return lesson;
        }
    }
}
