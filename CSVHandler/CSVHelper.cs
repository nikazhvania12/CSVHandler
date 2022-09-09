using System.Reflection;

namespace CSVHandler
{
    public static class CSVHelper
    {
        public static List<T> ConvertToModel<T>(string filepath)
        {
            using(var reader = new StreamReader(filepath))
            {
                List<T> result = new List<T>();
                List<PropertyInfo> ListProperties = new List<PropertyInfo>();
                var properties = typeof(T).GetProperties();

                var rows = reader.ReadToEnd().Split("\r\n");

                var titles = rows[0].Split(",");
                var content = rows.Skip(1).ToArray();

                ListProperties = GetCSVProperties(titles, properties);

                foreach(var row in content)
                {
                    if(!string.IsNullOrEmpty(row))
                    {
                        int i = 0;

                        var item = Activator.CreateInstance(typeof(T));
                        var values = row.Split(",");

                        foreach(var value in values)
                        {
                          AssignValue(item, value, ListProperties[i]);
                          i++;
                        }
                        if (item is T TItem)
                            result.Add(TItem);
                    }
                }

                return result;
            }
        }

        private static List<PropertyInfo> GetCSVProperties(string[] titles, PropertyInfo[] properties)
        {
            List<PropertyInfo> result = new List<PropertyInfo>();

            foreach (var title in titles)
            {
                foreach (var property in properties)
                {
                    var attr = property.GetCustomAttribute<DisplayNameAttribute>();

                    if (attr != null && attr.Name == title)
                    {
                        result.Add(property);
                    }
                }
            }

            return result;
        }

        private static void AssignValue(object item, string value, PropertyInfo property)
        {
            var @switch = new Dictionary<Type, Action>()
            {
                { typeof(string), () => property.SetValue(item, value.Replace("\"", "").Replace(@"\", ""))},
                { typeof(int), () => property.SetValue(item, Int32.Parse(value))},
                { typeof(DateTime), () => property.SetValue(item, DateTime.Parse(value))},
                { typeof(decimal), () => property.SetValue(item, decimal.Parse(value))},
                { typeof(bool), () => property.SetValue(item, bool.Parse(value))},

            };

            if (@switch.ContainsKey(property.PropertyType))
                @switch[property.PropertyType]();
        }

    }
}
