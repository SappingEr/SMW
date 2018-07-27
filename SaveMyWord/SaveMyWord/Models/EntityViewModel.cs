namespace SaveMyWord.Models
{
    public abstract class EntityViewModel<T>
    {
        public T Entity { get; set; }
    }
}