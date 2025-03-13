namespace Presentation.Extensions
{
    public static class ExtensionMethods
    {
        public static Dictionary<string, object> GetUpdatedFields<T>(this T original, T updated)
        {
            var updatedData = new Dictionary<string, object>();

            foreach (var prop in typeof(T).GetProperties())
            {
                var originalValue = prop.GetValue(original);
                var updatedValue = prop.GetValue(updated);

                if (prop.Name == "Id" && prop.PropertyType == typeof(int))
                {
                    updatedData[prop.Name] = null;
                    continue;
                }

                if (!Equals(originalValue, updatedValue) && updatedValue != null)
                {
                    updatedData[prop.Name] = updatedValue;
                }
            }
            return updatedData;
        }
    }
}
