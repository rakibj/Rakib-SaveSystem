namespace SaveSystem.Core
{
    public interface IEntityHolder<T> where T:BaseEntity
    {
        T GetEntity();
        void SetEntity(T entity);
    }
}