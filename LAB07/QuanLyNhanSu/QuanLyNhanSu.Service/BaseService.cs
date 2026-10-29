using System;
using System.Collections.Generic;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Service
{
    /// <summary>
    /// Lớp dịch vụ tổng quát: bọc repository, cung cấp CRUD cơ bản
    /// để các dịch vụ nghiệp vụ kế thừa và bổ sung quy tắc riêng.
    /// </summary>
    public abstract class BaseService<T> where T : class, new()
    {
        protected readonly Repository<T> Repo = new Repository<T>();

        public virtual List<T> GetAll() => Repo.GetAll(null);

        public virtual T GetById(params object[] keys) => Repo.GetById(keys);

        public virtual void Insert(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Repo.Insert(entity);
        }

        public virtual void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Repo.Update(entity);
        }

        public virtual void Delete(params object[] keys) => Repo.Delete(keys);
    }
}