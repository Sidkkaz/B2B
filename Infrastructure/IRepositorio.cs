namespace B2B.Infrastructure;

public interface IRepositorio<T>{
    void Add(T t);
    void Remove(T t);
    List<T> Query();
}