using Services.Contracts;
using System.Dynamic;
using System.Reflection;

namespace Services.Concretes
{
    public class DataShaperManager<T> : IDataShaper<T>
    {
        public DataShaperManager()
        {
            PropertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        public PropertyInfo[] PropertyInfos { get; set; }

        public ExpandoObject ShapeData(T entity, string fieldsString)
        {
            var requiredObjects = GetRequiredProperties(fieldsString);
            var expandoObject = FetchDataForEntity(entity, requiredObjects);
            return expandoObject;
        }

        public IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fieldsString)
        {
            var requiredObjects = GetRequiredProperties(fieldsString);
            var expandoObjects = FetchDataForEntities(entities, requiredObjects);
            return expandoObjects;
        }

        private IEnumerable<PropertyInfo> GetRequiredProperties(string fieldsString)
        {
            var requiredProperties = new List<PropertyInfo>();

            if (string.IsNullOrWhiteSpace(fieldsString))
            {
                requiredProperties = PropertyInfos.ToList();
            }
            else
            {
                var fields = fieldsString.Split(',', StringSplitOptions.RemoveEmptyEntries);

                foreach (var field in fields)
                {
                    var property = PropertyInfos.
                        FirstOrDefault(pp => pp.Name.Equals(field.Trim().ToLower(),
                        StringComparison.InvariantCultureIgnoreCase
                        ));

                    if (property is null)
                        continue;

                    requiredProperties.Add(property);
                }
            }

            return requiredProperties;
        }

        private ExpandoObject FetchDataForEntity(T entity, IEnumerable<PropertyInfo> requiredProperties)
        {
            var expandoObject = new ExpandoObject();

            foreach (var property in requiredProperties)
            {
                var value = property.GetValue(entity);
                expandoObject.TryAdd(property.Name, value);
            }

            return expandoObject;
        }

        private IEnumerable<ExpandoObject> FetchDataForEntities(IEnumerable<T> entities, IEnumerable<PropertyInfo> requiredProperties)
        {
            var expandoObjects = new List<ExpandoObject>();

            foreach (var entity in entities)
            {
                var expandoOjbect = FetchDataForEntity(entity, requiredProperties);
                expandoObjects.Add(expandoOjbect);
            }

            return expandoObjects;
        }
    }
}
