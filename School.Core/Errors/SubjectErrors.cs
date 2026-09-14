using School.Core.Common;

namespace School.Core.Errors
{
    public static class SubjectErrors
    {
        public static Error NotFound(string id)
        {
            return new Error("subject.not_found", $"Предмет с ID {id} не найден", 404);

        }

        public static Error Conflict(string name)
        {
            return new Error("subject.conflict", $"Предмет {name} уже существует", 409);
        }


    }
}
