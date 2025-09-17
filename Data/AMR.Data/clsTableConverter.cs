// Service Name  : clsTableConverter
// Date Created  : 10/22/2013
// Written By    : Stephen Farkas
// Version       : 2013.01
// Description   : Convert data returned from EntityFramework Link  Request to DataTable
//
//------------------------------------------------------------------------
//  Set Options & Imports
//------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;

namespace AMR.Data
{
    public static class clsTableConverter
    {
        public static DataTable ToDataTable<T>(IEnumerable<T> source)
        {
            //public static DataTable ToDataTable<T>(this IEnumerable<T> source)
            //{
            PropertyInfo[] properties = typeof(T).GetProperties();

            DataTable dtOutput = new DataTable();

            foreach (var p in properties)
            {
                Type colType = p.PropertyType;

                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                {
                    colType = colType.GetGenericArguments()[0];
                }

                //dtOutput.Columns.Add(p.Name, p.PropertyType);

                dtOutput.Columns.Add(new DataColumn(p.Name, colType));

            }

            foreach (var item in source)
            {
                DataRow row = dtOutput.NewRow();

                foreach (var p in properties)
                {
                    row[p.Name] = p.GetValue(item, null) == null ? DBNull.Value : p.GetValue(item, null);
                }

                dtOutput.Rows.Add(row);
            }

            return dtOutput;
        }
    }
}
