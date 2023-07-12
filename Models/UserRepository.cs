using System;
using MySqlConnector;
using System.Collections.Generic;

namespace Backend{
  public class UserRepository{
    private const string ConnectUrl = "Database=railway;Data Source=containers-us-west-31.railway.app;User Id=root;Password=Pf95OimosEw3IU7oUcfF;Port=6050";

    public void connectDb(){
      MySqlConnection connection = new MySqlConnection(ConnectUrl);
      connection.Open();
      Console.WriteLine("Connected");
      connection.Close();
    }

    public void insertNewUser(UserModel user){
      MySqlConnection connection = new MySqlConnection(ConnectUrl);
      connection.Open();

      string query = "INSERT INTO Usuario (nome, login, senha, data_nascimento) VALUES (@nome, @login, @senha, @data_nascimento);";
      MySqlCommand command = new MySqlCommand(query, connection);
      command.Parameters.AddWithValue("@nome", user.name);
      command.Parameters.AddWithValue("@login", user.login);
      command.Parameters.AddWithValue("@senha", user.password);
      command.Parameters.AddWithValue("@data_nascimento", user.dateNescient);
      command.ExecuteNonQuery();
      connection.Close();
    }

    public void updateDataUser (UserModel user){
      MySqlConnection connection = new MySqlConnection(ConnectUrl);
      connection.Open();

      String query = "UPDATE Usuario SET nome = @nome, login = @login, senha = @senha, data_nascimento = @data_nascimento, WHERE id = @id;";
      MySqlCommand command = new MySqlCommand(query, connection);
      command.Parameters.AddWithValue("@nome", user.name);
      command.Parameters.AddWithValue("@login", user.login);
      command.Parameters.AddWithValue("@senha", user.password);
      command.Parameters.AddWithValue("@data_nascimento", user.dateNescient);
      command.ExecuteNonQuery();
      connection.Close();
    }

    public void deleteUser(UserModel user){
      MySqlConnection connection = new MySqlConnection(ConnectUrl);
      connection.Open();

      String query = "DELETE FROM Usuario WHERE id = @id;";
      MySqlCommand command = new MySqlCommand(query, connection);
      command.Parameters.AddWithValue("@Id", user.id);
      command.ExecuteNonQuery();
      connection.Close();
    }

    public UserModel searchUserFromId(int id){
      MySqlConnection connection = new MySqlConnection(ConnectUrl);
      connection.Open();

      String query = "SELECT * FROM Usuario WHERE id = @id;";
      MySqlCommand command = new MySqlCommand(query, connection);
      command.Parameters.AddWithValue("@id", id);

      MySqlDataReader reader = command.ExecuteReader();
      UserModel foundUser = new UserModel();

      if (reader.Read()){
        foundUser.id = reader.GetInt32("id");
        if(!reader.IsDBNull(reader.GetOrdinal("nome"))){
          foundUser.name = reader.GetString("nome");
        }
        if(!reader.IsDBNull(reader.GetOrdinal("login"))){
          foundUser.login = reader.GetString("login");
        }
        if(!reader.IsDBNull(reader.GetOrdinal("senha"))){
          foundUser.password = reader.GetString("senha");
        }
        if(!reader.IsDBNull(reader.GetOrdinal("data_nascimento"))){
          foundUser.dateNescient = reader.GetDateTime("data_nascimento");
        }
      }
      connection.Close();
      return foundUser;
    }

    public List<UserModel> listAllUsers() {
      MySqlConnection connection = new MySqlConnection(ConnectUrl);
      connection.Open();

      String query = "USE Inertia; SELECT * FROM Usuario;";
      MySqlCommand command = new MySqlCommand(query, connection);
      MySqlDataReader reader = command.ExecuteReader();

      List<UserModel> listUsers = new List<UserModel>();

      while (reader.Read()){
        UserModel foundUser = new UserModel();
        foundUser.id = reader.GetInt32("id");
        if(!reader.IsDBNull(reader.GetOrdinal("nome"))){
          foundUser.name = reader.GetString("nome");
        }
        if(!reader.IsDBNull(reader.GetOrdinal("login"))){
          foundUser.login = reader.GetString("login");
        }
        if(!reader.IsDBNull(reader.GetOrdinal("senha"))){
          foundUser.password = reader.GetString("senha");
        }
        if(!reader.IsDBNull(reader.GetOrdinal("data_nascimento"))){
          foundUser.dateNescient = reader.GetDateTime("data_nascimento");
        }
        listUsers.Add(foundUser);
      }
      connection.Close();
      return listUsers;
    }

    public UserModel validateLogin(UserModel user) {
      MySqlConnection connection = new MySqlConnection(ConnectUrl);
      connection.Open();

      String query = "SELECT * FROM Usuario WHERE login = @login;";
      MySqlCommand command = new MySqlCommand(query, connection);

      command.Parameters.AddWithValue("@login", user.login);

      MySqlDataReader reader = command.ExecuteReader();
      UserModel foundUser = null;

      if (reader.Read()){
        foundUser = new UserModel();
        foundUser.id = reader.GetInt32("id");
        foundUser.name = reader.GetString("nome");
        foundUser.login = reader.GetString("login");
        foundUser.password = reader.GetString("senha");
        foundUser.dateNescient = reader.GetDateTime("data_nascimento");
      }
      connection.Close();
      return foundUser;
    }
  }
}
