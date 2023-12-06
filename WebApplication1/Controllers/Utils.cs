using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.Json;
namespace WebApi.Controllers
{
    public class Utils
    {
        public string DatatableToJson(DataTable dt)
        {
            List<Dictionary<string, object>> response = new List<Dictionary<string, object>>();

            foreach (DataRow row in dt.Rows)
            {
                List<Dictionary<string, object>> elements = new List<Dictionary<string, object>>();
                foreach (DataColumn column in dt.Columns)
                {
                    Dictionary<string, object> element = new Dictionary<string, object>
                    {
                        { "name", column.ColumnName },
                        { "value", row[column] }
                    };
                    elements.Add(element);
                }

                Dictionary<string, object> entry = new Dictionary<string, object>
                {
                    { "elements", elements }
                };

                response.Add(entry);
            }

            return JsonSerializer.Serialize(response);
        }

        public DataTable ToDataTable<T>(IEnumerable<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                object[] values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
        public string ListToJson<T>(List<T> items, string[] excludedProperties)
        {
            List<Dictionary<string, object>> response = new List<Dictionary<string, object>>();

            // Loop through each item in the list
            foreach (T item in items)
            {
                List<Dictionary<string, object>> elements = new List<Dictionary<string, object>>();

                // Get all the properties of the item
                PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in props)
                {
                    // Get the display name of the property
                    string displayName = prop.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                        .OfType<DisplayNameAttribute>()
                        .FirstOrDefault()?.DisplayName;

                    // Use the display name if available, otherwise use the property name
                    string columnName = string.IsNullOrWhiteSpace(displayName) ? prop.Name : displayName;

                    // Get the value of the property for the current item
                    object value = prop.GetValue(item);

                    // Convert value to string representation (handle null values)
                    string valueString = value?.ToString()?.Replace(',', '.');

                    // Create the element dictionary for the property
                    Dictionary<string, object> element = new Dictionary<string, object>
            {
                { "column", columnName },
                { "value", valueString }
            };
                    if (!excludedProperties.Contains(columnName))
                        elements.Add(element);
                }

                // Create the entry dictionary for the item
                Dictionary<string, object> entry = new Dictionary<string, object>
        {
            { "elements", elements }
        };

                response.Add(entry);
            }

            return JsonSerializer.Serialize(response);
        }

        public string ListToJson<T>(IEnumerable<T> items)
        {
            List<Dictionary<string, object>> response = new List<Dictionary<string, object>>();

            // Loop through each item in the list
            foreach (T item in items)
            {
                List<Dictionary<string, object>> elements = new List<Dictionary<string, object>>();

                // Get all the properties of the item
                PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in props)
                {
                    // Get the display name of the property
                    string displayName = prop.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                        .OfType<DisplayNameAttribute>()
                        .FirstOrDefault()?.DisplayName;

                    // Use the display name if available, otherwise use the property name
                    string columnName = string.IsNullOrWhiteSpace(displayName) ? prop.Name : displayName;

                    // Get the value of the property for the current item
                    object value = prop.GetValue(item);

                    // Convert value to string representation (handle null values)
                    string valueString = value?.ToString()?.Replace(',', '.');

                    // Create the element dictionary for the property
                    Dictionary<string, object> element = new Dictionary<string, object>
            {
                { "column", columnName },
                { "value", valueString }
            };

                    elements.Add(element);
                }

                // Create the entry dictionary for the item
                Dictionary<string, object> entry = new Dictionary<string, object>
        {
            { "elements", elements }
        };

                response.Add(entry);
            }

            return JsonSerializer.Serialize(response);
        }






    }

}
