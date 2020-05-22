using PortTunneling.ServerMode.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace PortTunneling
{
    public sealed class SqLite
    {
        public const string DatabaseFile = "db.sqlite";
        private readonly string _connectionString;

        public SqLite(string connectionString)
        {
            _connectionString = connectionString;

            if (!File.Exists(DatabaseFile))
            {
                CreateEmptyDB();
            }
        }

        public void CreateEmptyDB()
        {
            const string query = 
@"CREATE TABLE Tunnels (
    id          INTEGER   PRIMARY KEY AUTOINCREMENT
                          NOT NULL,
    enabled     BIT       NOT NULL
                          DEFAULT False,
    src_port    INT       NOT NULL
                          UNIQUE,
    dst_host    TEXT      NOT NULL,
    dst_port    INT       NOT NULL,
    mac         CHAR (12) NOT NULL,
    description TEXT
);";

            Void(query);
        }

        public DataTable GetAllTunnels()
        {
            return Table("SELECT * FROM tunnels");
        }

        internal TunnelModel[] GetAll()
        {
            var tunnels = GlobalVars.SQL.Query("SELECT * FROM tunnels", x =>
            {
                bool enabled = x.Value<bool>("enabled");
                var mac = PhysicalAddress.Parse(x.Value<string>("mac"));
                var dst_host = x.Value< string>("dst_host");
                var dst_port = x.Value<int>("dst_port");
                var endPoint = new DnsEndPoint(dst_host, dst_port);

                return new TunnelModel
                {
                    ID = x.Value<int>("id"),
                    Enabled = enabled,
                    SrcPort = x.Value<int>("src_port"),
                    DstEndPoint = endPoint,
                    Mac = mac,
                    Description = x.Value<string>("description"),
                };
            });

            return tunnels.ToArray();
        }

        public List<T> Query<T>(string query, Func<DbDataReader, T> selector)
        {
            using (var sqLiteConnection = new SQLiteConnection(_connectionString))
            {
                sqLiteConnection.Open();
                using (SQLiteCommand command = sqLiteConnection.CreateCommand())
                {
                    command.CommandText = query;
                    using (SQLiteDataReader sqLiteDataReader = command.ExecuteReader())
                    {
                        var objList = new List<T>();
                        while (sqLiteDataReader.Read())
                            objList.Add(selector(sqLiteDataReader));

                        return objList;
                    }
                }
            }
        }

        public int Void(string query, IList<object> args = null)
        {
            using (var sqLiteConnection = new SQLiteConnection(_connectionString))
            {
                sqLiteConnection.Open();
                using (SQLiteCommand command = sqLiteConnection.CreateCommand())
                {
                    command.CommandText = query;
                    command.CreateParameters(args);
                    return command.ExecuteNonQuery();
                }
            }
        }

        public DataTable Table(string query)
        {
            using (var sqLiteConnection = new SQLiteConnection(_connectionString))
            {
                sqLiteConnection.Open();
                using (SQLiteCommand command = sqLiteConnection.CreateCommand())
                {
                    command.CommandText = query;
                    using (SQLiteDataReader sqLiteDataReader = command.ExecuteReader())
                    {
                        using (DataSet dataSet = new DataSet())
                        {
                            dataSet.EnforceConstraints = false;
                            DataTable dataTable = dataSet.Tables.Add();
                            dataTable.Load((IDataReader)sqLiteDataReader);
                            return dataTable;
                        }
                    }
                }
            }
        }
    }
}
