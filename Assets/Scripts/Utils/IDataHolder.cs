namespace LPTask.Utils
{
    public interface IDataHolder<T>
    {
        bool TryGetData(out T data);
        void SetData(T data);
    }
}