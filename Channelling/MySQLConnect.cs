using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Channelling
{
    class MySQLConnect
    {
        //Connection
        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='orangehos';username=root;password=''");
        
        //Return the Connection
        public MySqlConnection getConnection()
        {
            return connection;
        }

        //Open the Connection
        public void openCon()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        //Close the Connection
        public void closeCon()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
