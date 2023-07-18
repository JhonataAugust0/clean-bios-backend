using System;
using MySqlConnector;
using System.Collections.Generic;

namespace Backend{
  public class ScheduleRepository{
    private const string ConnectUrl = "Database=railway;Data Source=containers-us-west-60.railway.app;User Id=root;Password=CmB4Dp3eKBtag2ufH4EJ;Port=7292";

    public void connectDb(){
      MySqlConnection connection = new MySqlConnection(ConnectUrl);
      connection.Open();
      Console.WriteLine("Connected");
      connection.Close();
    }

    public void insertNewSchedule(ScheduleModel schedule){
      MySqlConnection connection = new MySqlConnection(ConnectUrl);
      connection.Open();

      string query = "INSERT INTO Agendamento (dia, servico) VALUES (@dia, @servico);";
      MySqlCommand command = new MySqlCommand(query, connection);
      command.Parameters.AddWithValue("@dia", schedule.dia);
      command.Parameters.AddWithValue("@servico", schedule.servico);
      command.ExecuteNonQuery();
      connection.Close();
    }

    public void updateDataSchedule (ScheduleModel schedule){
      MySqlConnection connection = new MySqlConnection(ConnectUrl);
      connection.Open();

      String query = "UPDATE Agendamento SET dia = @dia, servico = @servico WHERE id = @id;";
      MySqlCommand command = new MySqlCommand(query, connection);
      command.Parameters.AddWithValue("@dia", schedule.dia);
      command.Parameters.AddWithValue("@servico", schedule.servico);
      command.ExecuteNonQuery();
      connection.Close();
    }

    public void deleteSchedule(ScheduleModel schedule){
      MySqlConnection connection = new MySqlConnection(ConnectUrl);
      connection.Open();

      String query = "DELETE FROM Agendamento WHERE id = @id;";
      MySqlCommand command = new MySqlCommand(query, connection);
      command.Parameters.AddWithValue("@Id", schedule.id);
      command.ExecuteNonQuery();
      connection.Close();
    }

    public ScheduleModel searchScheduleFromId(int id){
      MySqlConnection connection = new MySqlConnection(ConnectUrl);
      connection.Open();

      String query = "SELECT * FROM Agendamento WHERE id = @id;";
      MySqlCommand command = new MySqlCommand(query, connection);
      command.Parameters.AddWithValue("@id", id);

      MySqlDataReader reader = command.ExecuteReader();
      ScheduleModel foundUser = new ScheduleModel();

      if (reader.Read()){
        foundUser.id = reader.GetInt32("id");
        if(!reader.IsDBNull(reader.GetOrdinal("dia"))){
          foundUser.dia = reader.GetDateTime("dia");
        }
        if(!reader.IsDBNull(reader.GetOrdinal("servico"))){
          foundUser.servico = reader.GetString("servico");
        }
      }
      connection.Close();
      return foundUser;
    }

    public List<ScheduleModel> listAllSchedules() {
      MySqlConnection connection = new MySqlConnection(ConnectUrl);
      connection.Open();

      String query = "USE Inertia; SELECT * FROM Agendamento;";
      MySqlCommand command = new MySqlCommand(query, connection);
      MySqlDataReader reader = command.ExecuteReader();

      List<ScheduleModel> listUsers = new List<ScheduleModel>();

      while (reader.Read()){
        ScheduleModel foundUser = new ScheduleModel();
        foundUser.id = reader.GetInt32("id");
        if(!reader.IsDBNull(reader.GetOrdinal("dia"))){
          foundUser.dia = reader.GetDateTime("dia");
        }
        if(!reader.IsDBNull(reader.GetOrdinal("servico"))){
          foundUser.servico = reader.GetString("servico");
        }
        listUsers.Add(foundUser);
      }
      connection.Close();
      return listUsers;
  }
}
}
