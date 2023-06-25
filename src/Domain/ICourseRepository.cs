namespace PoliVagas.Core.Domain;

public interface ICourseRepository
{
    public Task<Course> GetOrAdd(string courseName);
    public Task<IEnumerable<Course>> GetAll();
}
