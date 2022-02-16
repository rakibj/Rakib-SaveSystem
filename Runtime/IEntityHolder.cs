namespace SaveSystem.Core
{
    public interface IEntityHolder<T> where T:BaseEntity
    {
        T GetEntity();
        T GetDefaultEntity();
        void SetEntity(T entity);
    }
}