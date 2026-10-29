using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace QuanLyNhanSu.Data
{
    /// <summary>Mô tả một cột ánh xạ từ thuộc tính của thực thể.</summary>
    public sealed class ColumnMeta
    {
        public PropertyInfo Property { get; set; }
        public string Name { get; set; }        // tên cột trong CSDL
        public bool IsKey { get; set; }         // thuộc khóa chính
        public string Label { get; set; }        // nhãn hiển thị tiếng Việt
    }

    /// <summary>
    /// Phản chiếu để ánh xạ thực thể &lt;-&gt; bảng CSDL:
    /// đọc giá trị từ IDataRecord vào đối tượng, sinh tham số CRUD.
    /// </summary>
    public static class DbMapping
    {
        private static readonly object Gate = new object();
        private static readonly Dictionary<Type, List<ColumnMeta>> Cache = new Dictionary<Type, List<ColumnMeta>>();

        public static List<ColumnMeta> GetColumns(Type type)
        {
            lock (Gate)
            {
                if (Cache.TryGetValue(type, out List<ColumnMeta> existing))
                    return existing;

                List<ColumnMeta> cols = new List<ColumnMeta>();
                foreach (PropertyInfo prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (prop.GetCustomAttribute<IgnoreAttribute>() != null || !prop.CanRead || !prop.CanWrite)
                        continue;

                    string name = prop.GetCustomAttribute<ColumnAttribute>()?.Name ?? prop.Name;
                    bool isKey = prop.GetCustomAttribute<KeyAttribute>() != null;
                    string label = prop.GetCustomAttribute<LabelAttribute>()?.Text ?? prop.Name;

                    cols.Add(new ColumnMeta { Property = prop, Name = name, IsKey = isKey, Label = label });
                }

                if (cols.Count == 0)
                    throw new InvalidOperationException($"Lớp {type.Name} không có thuộc tính nào được ánh xạ.");

                Cache[type] = cols;
                return cols;
            }
        }

        public static string GetTableName(Type type)
        {
            TableAttribute table = type.GetCustomAttribute<TableAttribute>();
            if (table == null)
                throw new InvalidOperationException($"Lớp {type.Name} thiếu [Table].");
            return table.Name;
        }

        public static List<ColumnMeta> GetKeys(Type type)
        {
            return GetColumns(type).Where(c => c.IsKey).OrderBy(c => c.Property.MetadataToken).ToList();
        }

        /// <summary>
        /// Đọc một dòng từ IDataRecord vào đối tượng. Ánh xạ theo tên cột cho
        /// mọi thuộc tính có setter (kể cả [Ignore]) để lấy được các cột "hiển thị"
        /// do câu truy vấn JOIN trả về (ví dụ: HoTen, TenPB, TenCa...).
        /// </summary>
        public static void Map(IDataRecord record, object target)
        {
            Type type = target.GetType();
            foreach (PropertyInfo prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!prop.CanWrite)
                    continue;

                string name = prop.GetCustomAttribute<ColumnAttribute>()?.Name ?? prop.Name;
                int ordinal;
                try { ordinal = record.GetOrdinal(name); }
                catch (IndexOutOfRangeException) { continue; }
                if (record.IsDBNull(ordinal))
                    continue;

                object value = record.GetValue(ordinal);
                prop.SetValue(target, ConvertValue(value, prop.PropertyType), null);
            }
        }

        public static object ConvertValue(object value, Type targetType)
        {
            if (value == null || value == DBNull.Value)
            {
                if (targetType.IsValueType && Nullable.GetUnderlyingType(targetType) == null)
                    return Activator.CreateInstance(targetType);   // giá trị mặc định cho kiểu không nullable
                return null;
            }

            Type underlying = Nullable.GetUnderlyingType(targetType) ?? targetType;

            if (underlying == typeof(TimeSpan) && value is TimeSpan ts) return ts;
            if (underlying == typeof(TimeSpan) && value is string s && TimeSpan.TryParse(s, out TimeSpan tsp)) return tsp;
            if (underlying == typeof(Guid) && value is Guid g) return g;

            if (underlying.IsEnum)
                return Enum.ToObject(underlying, value);

            try
            {
                return Convert.ChangeType(value, underlying, CultureInfo.InvariantCulture);
            }
            catch
            {
                return Convert.ChangeType(value, underlying, CultureInfo.CurrentCulture);
            }
        }
    }
}