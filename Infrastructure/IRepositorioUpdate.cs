namespace B2B.Infrastructure;

public interface IRepositorioUpdate<T> : IRepositorio<T>{
    void Update(T t);
}