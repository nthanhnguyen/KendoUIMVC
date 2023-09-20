using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace AspMVCWebApp.Models
{
    public class UserContext
    {
        string cs = ConfigurationManager.ConnectionStrings["WebDb"].ConnectionString;

        public List<UserModel> GetUsers()
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
                                UserID = reader.IsDBNull(0) ? null : reader.GetString(0),
                                UserName = reader.IsDBNull(1) ? null : reader.GetString(1),
                                Password = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Email = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Tel = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Disabled = reader.GetByte(5)
                            };
                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }
        public int UpdateUser(UserModel users)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("UpdateUser", con);
            cmd.Parameters.AddWithValue("@userid", users.UserID);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@username", users.UserName);
            cmd.Parameters.AddWithValue("@password", users.Password);
            cmd.Parameters.AddWithValue("@email", users.Email);
            cmd.Parameters.AddWithValue("@tel", users.Tel);
            cmd.Parameters.AddWithValue("@disabled", users.Disabled);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;

        }

        public int SaveUser(UserModel users)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("SaveUser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@userid", users.UserID.Trim());
            cmd.Parameters.AddWithValue("@username", users.UserName);
            cmd.Parameters.AddWithValue("@password", users.Password);
            cmd.Parameters.AddWithValue("@email", users.Email);
            cmd.Parameters.AddWithValue("@tel", users.Tel);
            cmd.Parameters.AddWithValue("@disabled", users.Disabled);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;

        }
        public UserModel GetUsersByID(string id)
        {
            SqlConnection con = new SqlConnection(cs);

            UserModel users = new UserModel();

            SqlCommand cmd = new SqlCommand("GetUsersById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userID", id);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {

                users.UserID = Convert.ToString(dr[0]).Trim();
                users.UserName = Convert.ToString(dr[1]);
                users.Password = Convert.ToString(dr[2]);
                users.Email = Convert.ToString(dr[3]);
                users.Tel = Convert.ToString(dr[4]);
                users.Disabled = Convert.ToByte(dr[5]);

            }
            return users;



        }

        public int DeleteUser(string id)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("DeleteUser", con);
            cmd.Parameters.AddWithValue("@userID", id);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;

        }
        public List<UserModel> GetUsersByEmail(string search)
        {
            SqlConnection con = new SqlConnection(cs);
            List<UserModel> users = new List<UserModel>();

            SqlCommand cmd = new SqlCommand("GetUsersByEmail", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@searchstring", search);


            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                UserModel user = new UserModel();
                user.UserID = (string)reader["UserID"];
                user.UserName = (string)reader["UserName"];
                user.Password = reader["Password"] == DBNull.Value ? null : (string)reader["Password"];
                user.Email = (string)reader["Email"];
                user.Tel = reader["Tel"] == DBNull.Value ? null : (string)reader["Tel"];
                user.Disabled = (byte)reader["Disabled"];

                users.Add(user);
            }

            reader.Close();
            return users;
        }
    }
}