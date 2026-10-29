using System;

namespace QuanLyNhanSu.Data
{
    /// <summary>Ánh xạ lớp thực thể tới một bảng trong CSDL.</summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class TableAttribute : Attribute
    {
        public string Name { get; private set; }
        public TableAttribute(string name) { Name = name; }
    }

    /// <summary>Ánh xạ thuộc tính tới một cột (mặc định trùng tên thuộc tính).</summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public sealed class ColumnAttribute : Attribute
    {
        public string Name { get; private set; }
        public ColumnAttribute(string name) { Name = name; }
    }

    /// <summary>Đánh dấu thuộc tính là một phần của khóa chính (có thể nhiều -> khóa ghép).</summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public sealed class KeyAttribute : Attribute { }

    /// <summary>Bỏ qua thuộc tính khi ánh xạ (không phải cột trong bảng).</summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public sealed class IgnoreAttribute : Attribute { }

    /// <summary>Nhãn hiển thị (tiếng Việt) dùng cho giao diện.</summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public sealed class LabelAttribute : Attribute
    {
        public string Text { get; private set; }
        public LabelAttribute(string text) { Text = text; }
    }
}