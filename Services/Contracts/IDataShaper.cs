using System.Dynamic;

namespace Services.Contracts
{
    public interface IDataShaper<T>
    {
        ExpandoObject ShapeData(T entity, string fieldsString);
        IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fieldsString);
    }
}
