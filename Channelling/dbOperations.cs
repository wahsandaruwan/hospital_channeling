using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Channelling
{
    //For Common Database Operations
    class dbOperations
    {
        MySQLConnect db = new MySQLConnect();
        MySqlCommand cmd;

        //For Data Grid View Populate
        public DataTable forPopulateTable(string query)
        {
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, db.getConnection());
            adapter.Fill(table);
            return table;
        }

        //Query Execution
        public string executeQuery(string query)
        {
            try
            {
                db.openCon();
                cmd = new MySqlCommand(query, db.getConnection());
                if (cmd.ExecuteNonQuery() == 1)
                {
                    return "T";
                }
                else
                {
                    return "F";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                db.closeCon();
            }
        }

       
    }
}
