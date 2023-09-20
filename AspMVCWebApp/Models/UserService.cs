using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace AspMVCWebApp.Models
{
    public class UserService
    {
        private static bool UpdateDatabase = false;
        private SqlConnection connection;
        string cs = ConfigurationManager.ConnectionStrings["WebDb"].ConnectionString;
        public UserService(string constring)
        {
            this.connection = new SqlConnection(constring);
            this.connection.Open();
        }

        public UserService()
        {
        }

        public IList<UserModel> GetUsers()

        {

            List<UserModel> users = new List<UserModel>();

            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("GetUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserModel user = new UserModel
                            {
                                UserID = reader.GetString(0),
                                UserName = reader.GetString(1),
                                Password = reader.GetString(2),
                                Email = reader.GetString(3),
                                Tel = reader.GetString(4),
                                Disabled = reader.GetByte(5)
                            };
                            users.Add(user);
                        }
                    }
                }


                return users;

            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> Read()
        {
            return GetUsers();
        }

        public void Create(UserModel users)
        {
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SaveUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userid", users.UserID);
                    command.Parameters.AddWithValue("@username", users.UserName);
                    command.Parameters.AddWithValue("@password", users.Password);
                    command.Parameters.AddWithValue("@email", users.Email);
                    command.Parameters.AddWithValue("@tel", users.Tel);
                    command.Parameters.AddWithValue("@disabled", users.Disabled);



                    command.ExecuteNonQuery();


                }
            }
        }

        public void Update(UserModel users)
        {

            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UpdateUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.AddWithValue("@userid", users.UserID);
                    command.Parameters.AddWithValue("@username", users.UserName);
                    command.Parameters.AddWithValue("@email", users.Email);
                    command.Parameters.AddWithValue("@password", users.Password);
                    command.Parameters.AddWithValue("@tel", users.Tel);
                    command.Parameters.AddWithValue("@disabled", users.Disabled);


                    command.ExecuteNonQuery();
                }
            }
            //}
        }

        public void Destroy(UserModel users)
        {

            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DeleteUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userID", users.UserID);
                    command.ExecuteNonQuery();
                }
            }
        }

        public UserModel One(Func<UserModel, bool> predicate)
        {
            return GetUsers().FirstOrDefault(predicate);

        }

    }

}