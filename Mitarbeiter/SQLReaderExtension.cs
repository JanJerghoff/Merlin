using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitarbeiter
{
    class SQLReaderExtension
    {
        public static int getIntOrMinusEins(MySqlDataReader rdr, int stelle) {

            if (rdr.IsDBNull(stelle))
            {
                return -1;
            }
            else {
                return rdr.GetInt32(stelle);
            }
        }

    }
}
