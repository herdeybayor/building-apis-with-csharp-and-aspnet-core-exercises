namespace TheEmployeeAPI.Abstractions;

public interface IRepository<T>
{
    T? GetById(int id);
    T? GetEmployeeBySsn(string ssn);
    IEnumerable<T> GetAll();
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}
