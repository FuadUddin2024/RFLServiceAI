using System.Data;

namespace CSWMS.Utility
{
    public class ExtractData
    {
        public static List<T> Convert<T>(DataTable table) where T : new()
        {
            var list = new List<T>();

            foreach (DataRow row in table.Rows)
            {
                T obj = new T();

                foreach (var prop in typeof(T).GetProperties())
                {
                    if (table.Columns.Contains(prop.Name) && row[prop.Name] != DBNull.Value)
                    {
                        prop.SetValue(obj, row[prop.Name]);
                    }
                }

                list.Add(obj);
            }

            return list;
        }
    }
}
