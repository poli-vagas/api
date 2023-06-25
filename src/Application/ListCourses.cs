using PoliVagas.Core.Domain;

namespace PoliVagas.Core.Application.ListCourses;

public class ListCoursesHandler
{
    private readonly ICourseRepository _courses;

    public ListCoursesHandler(ICourseRepository courses) {
        _courses = courses;
    }

    public async Task<IEnumerable<Course>> Execute() {
        return await _courses.GetAll();
    }
}
