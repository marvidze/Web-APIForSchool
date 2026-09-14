using School.Core.Common;

namespace School.Core.Errors
{
    public static class TeacherErrors
    {
        public static Error NotFound(string id)
        {
            return new Error("teacher.not_found", $"Преподаватель с ID {id} не найден", 404);
        }

        public static Error ConflictEmail()
        {
            return new Error("teacher.conflict_email", $"Преподаватель с таким email уже существует", 409);
        }
    }
}
