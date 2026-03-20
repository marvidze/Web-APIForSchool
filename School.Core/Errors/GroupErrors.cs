using School.Core.Common;

namespace School.Core.Errors
{
    public static class GroupErrors
    {
        public static Error NotFound(string id)
        {
            return new Error("group.not_found", $"Группа с ID {id} не найдена", 404);
        }
    }
}
