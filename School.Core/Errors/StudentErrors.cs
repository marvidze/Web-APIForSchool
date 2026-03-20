using School.Core.Common;

namespace School.Core.Errors
{
    public static class StudentErrors
    {
        public static Error NotFound(string id)
        {
            return new Error("student.not_found", $"Студент с ID {id} не найден", 404);
        }
    }
}
