using System;

namespace Backend{
  public class UserModel{
    public int id { get; set; }
    public string name  { get; set; }
    public string login { get; set; }
    public string password { get; set; }
    public DateTime dateNescient { get; set; }
  }
}