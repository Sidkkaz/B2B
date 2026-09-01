public interface IRepositorio<T>{
    void Add(T t);
    void Remove(T t);
    T<t> Query();
    void Update(T t);
}