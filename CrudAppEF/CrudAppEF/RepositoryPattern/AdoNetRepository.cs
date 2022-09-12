using CrudAppEF.Models.Base;
using Microsoft.Data.SqlClient;
using Pluralize.NET.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppEF.RepositoryPattern
{
    public class AdoNetRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly SqlConnection _connection;

        public AdoNetRepository(SqlConnection connection)
        {
            _connection = connection;

        }

        public T Add(T entity)
        {
            ICollection<string> keys = new List<string>();
            foreach (var property in entity.GetType().GetProperties())
            {
                if (property.Name == "Id")
                    continue;
                keys.Add(property.Name);
            }
            string tableName = new Pluralizer().Pluralize(typeof(T).Name);
            //Insert into contacts (firstname,lastname,phone,email)  values (@firstname,@lastname,@phone,@email)
            string query = $"insert into {tableName} ({string.Join(",", keys)}) values(@{string.Join(", @", keys)})";
            SqlCommand command = new SqlCommand(query, _connection);

            foreach (var prop in entity.GetType().GetProperties())
            {
                if (prop.Name != "Id")
                    command.Parameters.Add(prop.Name, System.Data.SqlDbType.NVarChar).Value = prop.GetValue(entity);
            }
            _connection.Open();
            bool result = command.ExecuteNonQuery() > 0;
            _connection.Close();
            if (result) return entity;
            throw new Exception("");
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Search(Expression<Func<T, bool>> func)
        {
            ICollection<T> entities = new List<T>();
            string tableName = new Pluralizer().Pluralize(typeof(T).Name);
            string paramName = string.Empty;
            string searchValue = string.Empty;
            string query = String.Empty;
            if (func.Body is BinaryExpression ex)
            {
                paramName = ((MemberExpression)ex.Left).Member.Name;
                searchValue = ex.Right.ToString().Trim('"');
                query = $"select * from {tableName} where {paramName} = @SearchValue";

            }
            else if (func.Body is MethodCallExpression methodEx)
            {
                paramName = ((MemberExpression)methodEx.Object).Member.Name;
                searchValue = methodEx.Arguments[0].ToString().Trim('"');
                query = $"select * from {tableName} where {paramName} like '%'+@SearchValue+'%'";
            }
            else throw new Exception();

            SqlCommand command = new SqlCommand(query, _connection);
            _connection.Open();
            command.Parameters.Add("SearchValue", System.Data.SqlDbType.NVarChar).Value = searchValue;
            SqlDataReader dr = command.ExecuteReader();
            var props = typeof(T).GetProperties();
            while (dr.Read())
            {
                T obj = new T();
                for (int i = 0; i < props.Length; i++)
                {
                    props[i].SetValue(obj, dr[props[i].Name]);
                }
                entities.Add(obj);
            }
            return entities;
        }
    }
}
