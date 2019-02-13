using System.Collections;
using System.Collections.Generic;
using System.Data;
using System;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using System.IO;
using UnityEngine;

public class Database_Connection {

    private Dictionary<string, string> connectionString = new Dictionary<string, string>();

    internal void InsertUser(Upgrade upgrade)
    {
        throw new NotImplementedException();
    }

    public Database_Connection(string Databasename)
    {
        string filepath;

        if (Application.platform != RuntimePlatform.Android)
        {
            filepath = Application.dataPath + "/StreamingAssets/" + Databasename;
            Debug.Log("-------- " + filepath + " --------");
        }

        else
        {
            filepath = Application.persistentDataPath + "/" + Databasename;
            string oriPath = System.IO.Path.Combine(Application.streamingAssetsPath, Databasename);
            while (!File.Exists(filepath))
            {
                WWW load = new WWW(oriPath);
                while (!load.isDone) { }

                System.IO.File.WriteAllBytes(filepath, load.bytes);
            }
        }

        connectionString.Add(Databasename, "URI=file:" + filepath);
    }

    internal void InsertUser(Car car)
    {
        throw new NotImplementedException();
    }

    internal void InsertUser(Item item)
    {
        throw new NotImplementedException();
    }

    public int selectTable(string connection_string, string table_name)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString[connection_string]))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                //check table is null?
                string sqlQuery = "SELECT COUNT(*) FROM " + table_name;
                dbCmd.CommandText = sqlQuery;
                int count = Convert.ToInt32(dbCmd.ExecuteScalar());
                if (count == 0)
                {
                    return count;
                }

                sqlQuery = "SELECT * FROM " + table_name;

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (connection_string == "UserDataBase.db" && table_name == "User")
                        {                            
                            User tmp = new User(reader.GetString(0), reader.GetInt16(1), reader.GetInt16(2));
                            l_User.l_user.Add(tmp);
                        }
                    }
                    reader.Close();
                    dbConnection.Close();

                    return count;
                }
            }
        }
    }

    public void InsertUser(User user)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString["UserDataBase.db"]))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("insert into User (Username, Score, Coin) values (\"{0}\", \"{1}\", \"{2}\")", user.Username, user.Score, user.Coin);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteNonQuery();
                dbConnection.Close();
            }
        }
    }

    public void Remove(string table, string colunm, string value)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString["UserDataBase.db"]))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("DELETE FROM {0} WHERE {1} = \"{2}\"", table, colunm, value);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }

    public void Update(string table, string colunm, string value, string adress, string condition)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString["UserDataBase.db"]))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("UPDATE {0} SET {1} = {2} WHERE {3} = \"{4}\"", table, colunm, value, adress, condition);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }

}
