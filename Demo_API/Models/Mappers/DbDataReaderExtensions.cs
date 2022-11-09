using Demo_API.Models;
using System.Data.Common;

namespace Demo_API.Models.Mappers
{
    internal static class DbDataReaderExtensions
    {
        internal static Contact ToContact(this DbDataReader reader)
        {

            return new Contact()
            {
                Id = (int)reader["Id"],
                FirstName = (string)reader["FirstName"],
                LastName = (string)reader["LastName"],
                Birthdate = (DateTime)reader["Birthdate"],
                Email = (string)reader["Email"],
                SurName = reader["SurName"]==DBNull.Value ? null : (string?)reader["SurName"],
                Phone = reader["Phone"] == DBNull.Value ? null : (string?)reader["Phone"],
            };
        }


        internal static User ToUser(this DbDataReader reader)
        {
            return new User()
            {
                Id = (int)reader["Id"],
                FirstName = (string)reader["FirstName"],
                LastName = (string)reader["LastName"],
                Birthdate = (DateTime)reader["Birthdate"],
                Email = (string)reader["Email"],
            };
        }
    }
}
