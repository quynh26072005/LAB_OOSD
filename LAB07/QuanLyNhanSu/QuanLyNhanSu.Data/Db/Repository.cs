using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace QuanLyNhanSu.Data
{
    /// <summary>
    /// Repository tổng quát (Data Access) cho một thực thể T bất kỳ:
    /// CRUD cơ bản dựa trên [Table], [Key], [Column]. Các nghiệp vụ
    /// phức tạp được đặt ở tầng Service.
    /// </summary>
    public class Repository<T> where T : class, new()
    {
        private readonly string _table;
        private readonly List<ColumnMeta> _columns;
        private readonly List<ColumnMeta> _keys;

        public Repository()
        {
            Type t = typeof(T);
            _table = DbMapping.GetTableName(t);
            _columns = DbMapping.GetColumns(t);
            _keys = DbMapping.GetKeys(t);

            if (_keys.Count == 0)
                throw new InvalidOperationException($"Lớp {t.Name} phải có ít nhất một thuộc tính [Key].");
        }

        public List<T> GetAll(string orderBy = null)
        {
            string sql = $"SELECT * FROM [{_table}]";
            if (!string.IsNullOrWhiteSpace(orderBy))
                sql += " ORDER BY " + orderBy;
            return SqlHelper.ExecuteList<T>(sql);
        }

        public List<T> FindBy(string where, params SqlParameter[] parameters)
        {
            string sql = $"SELECT * FROM [{_table}] WHERE {where}";
            return SqlHelper.ExecuteList<T>(sql, parameters);
        }

        /// <summary>Lấy một bản ghi theo khoá chính (đúng thứ tự khai báo [Key]).</summary>
        public T GetById(params object[] keyValues)
        {
            if (keyValues.Length != _keys.Count)
                throw new ArgumentException("Số lượng giá trị khóa không khớp.", nameof(keyValues));

            var where = new StringBuilder();
            var ps = new List<SqlParameter>();
            for (int i = 0; i < _keys.Count; i++)
            {
                if (i > 0) where.Append(" AND ");
                where.Append($"[{_keys[i].Name}] = @k{i}");
                ps.Add(new SqlParameter($"@k{i}", keyValues[i] ?? DBNull.Value));
            }
            return SqlHelper.ExecuteSingle<T>($"SELECT * FROM [{_table}] WHERE {where}", ps.ToArray());
        }

        public void Insert(T entity)
        {
            var cols = _columns;
            var names = cols.Select(c => $"[{c.Name}]").ToArray();
            var ps = new List<SqlParameter>();
            for (int i = 0; i < cols.Count; i++)
            {
                ps.Add(new SqlParameter($"@p{i}", cols[i].Property.GetValue(entity, null) ?? DBNull.Value));
            }

            string sql = $"INSERT INTO [{_table}] ({string.Join(", ", names)}) VALUES ({string.Join(", ", cols.Select((c, i) => "@p" + i))})";
            SqlHelper.ExecuteNonQuery(sql, ps.ToArray());
        }

        public void Update(T entity)
        {
            var vals = new StringBuilder();
            var ps = new List<SqlParameter>();
            int n = 0;
            foreach (ColumnMeta col in _columns)
            {
                if (col.IsKey) continue;   // khoá chính không cập nhật
                if (vals.Length > 0) vals.Append(", ");
                vals.Append($"[{col.Name}] = @v{n}");
                ps.Add(new SqlParameter($"@v{n}", col.Property.GetValue(entity, null) ?? DBNull.Value));
                n++;
            }

            var where = new StringBuilder();
            for (int i = 0; i < _keys.Count; i++)
            {
                if (i > 0) where.Append(" AND ");
                where.Append($"[{_keys[i].Name}] = @k{i}");
                ps.Add(new SqlParameter($"@k{i}", _keys[i].Property.GetValue(entity, null) ?? DBNull.Value));
            }

            string sql = $"UPDATE [{_table}] SET {vals} WHERE {where}";
            SqlHelper.ExecuteNonQuery(sql, ps.ToArray());
        }

        public void Delete(params object[] keyValues)
        {
            if (keyValues.Length != _keys.Count)
                throw new ArgumentException("Số lượng giá trị khóa không khớp.", nameof(keyValues));

            var where = new StringBuilder();
            var ps = new List<SqlParameter>();
            for (int i = 0; i < _keys.Count; i++)
            {
                if (i > 0) where.Append(" AND ");
                where.Append($"[{_keys[i].Name}] = @k{i}");
                ps.Add(new SqlParameter($"@k{i}", keyValues[i] ?? DBNull.Value));
            }

            string sql = $"DELETE FROM [{_table}] WHERE {where}";
            SqlHelper.ExecuteNonQuery(sql, ps.ToArray());
        }
    }
}