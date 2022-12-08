using System;

using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetWPFHomeWork
{
    public class User
    {
        public User(int? id, string f, string i, string o, string passNumber, string passSeries, string passIssued, string phone, string email)
        {
            Id = id;
            F = f;
            I = i;
            O = o;
            PassNumber = passNumber;
            PassSeries = passSeries;
            PassIssued = passIssued;
            Phone = phone;
            Email = email;
        }

        public User(SqlDataReader sqlDataReader)
        {
            Id = sqlDataReader.GetInt32(0);
            F = sqlDataReader.GetString(1);
            I = sqlDataReader.GetString(2);
            O = sqlDataReader.GetString(3);
            PassNumber = sqlDataReader.GetString(4);
            PassSeries = sqlDataReader.GetString(5);
            PassIssued = sqlDataReader.GetString(6);
            Phone = sqlDataReader.GetString(7);
            Email = sqlDataReader.GetString(8);
        }

        public int? Id { get; set; }
        public string F { get; set; }
        public string I { get; set; }
        public string O { get; set; }
        public string PassNumber { get; set; }
        public string PassSeries { get; set; }
        public string PassIssued { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
