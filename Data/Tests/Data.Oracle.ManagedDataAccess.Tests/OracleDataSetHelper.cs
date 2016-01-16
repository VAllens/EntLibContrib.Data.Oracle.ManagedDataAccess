//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Data Access Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================


using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

namespace EntLibContrib.Data.Oracle.ManagedDataAccess.Tests
{
    internal sealed class OracleDataSetHelper
    {
        public static void CreateDataAdapterCommandsDynamically(Database db, ref DbCommand insertCommand, ref DbCommand updateCommand, ref DbCommand deleteCommand)
        {
            insertCommand = db.GetStoredProcCommandWithSourceColumns("RegionInsert", "RegionID", "RegionDescription");
            updateCommand = db.GetStoredProcCommandWithSourceColumns("RegionUpdate", "RegionID", "RegionDescription");
            deleteCommand = db.GetStoredProcCommandWithSourceColumns("RegionDelete", "RegionID");
        }

        public static void CreateDataAdapterCommands(Database db, ref DbCommand insertCommand, ref DbCommand updateCommand, ref DbCommand deleteCommand)
        {
            insertCommand = db.GetStoredProcCommand("RegionInsert");
            updateCommand = db.GetStoredProcCommand("RegionUpdate");
            deleteCommand = db.GetStoredProcCommand("RegionDelete");

            db.AddInParameter(insertCommand, "vRegionID", DbType.Int32, "RegionID", DataRowVersion.Default);
            db.AddInParameter(insertCommand, "vRegionDescription", DbType.String, "RegionDescription", DataRowVersion.Default);

            db.AddInParameter(updateCommand, "vRegionID", DbType.Int32, "RegionID", DataRowVersion.Default);
            db.AddInParameter(updateCommand, "vRegionDescription", DbType.String, "RegionDescription", DataRowVersion.Default);

            db.AddInParameter(deleteCommand, "vRegionID", DbType.Int32, "RegionID", DataRowVersion.Default);
        }

        public static void CreateStoredProcedures(Database db)
        {
            DbCommand command;
            string sql;

            try
            {
                DeleteStoredProcedures(db);
            }
            catch { }

            sql = "CREATE OR REPLACE PACKAGE PKGENTLIB AS " +
                "TYPE T_CURSOR IS REF CURSOR; " +
                "PROCEDURE RegionSelect (CUR_OUT OUT T_CURSOR); " +
                "END PKGENTLIB;";

            command = db.GetSqlStringCommand(sql);
            db.ExecuteNonQuery(command);

            sql = "CREATE OR REPLACE PACKAGE BODY PKGENTLIB AS " +
                "PROCEDURE RegionSelect(CUR_OUT OUT T_CURSOR) IS " +
                "V_CURSOR T_CURSOR; " +
                "BEGIN " +
                "OPEN V_CURSOR FOR " +
                "SELECT * FROM Region ORDER BY RegionID; " +
                "CUR_OUT := V_CURSOR; " +
                "END RegionSelect; " +
                "END PKGENTLIB;";

            command = db.GetSqlStringCommand(sql);
            db.ExecuteNonQuery(command);

            sql = "create procedure RegionInsert (pRegionID IN Region.RegionID%TYPE, pRegionDescription IN Region.RegionDescription%TYPE) as " +
                    "BEGIN " +
                    "   insert into Region values(pRegionID, pRegionDescription); " +
                    "END;";

            command = db.GetSqlStringCommand(sql);
            db.ExecuteNonQuery(command);

            sql = "create procedure RegionUpdate (pRegionID IN Region.RegionID%TYPE, pRegionDescription IN Region.RegionDescription%TYPE) as " +
                    "BEGIN " +
                    "   update Region set RegionDescription = pRegionDescription where RegionID = pRegionID; " +
                    "END;";

            command = db.GetSqlStringCommand(sql);
            db.ExecuteNonQuery(command);

            sql = "create procedure RegionDelete (pRegionID IN Region.RegionID%TYPE) as " +
                    "BEGIN " +
                    "   delete from Region where RegionID = pRegionID; " +
                    "END;";

            command = db.GetSqlStringCommand(sql);
            db.ExecuteNonQuery(command);
        }

        public static void DeleteStoredProcedures(Database db)
        {
            DbCommand command;
            string sql = "drop procedure RegionSelect";
            command = db.GetSqlStringCommand(sql);
            try { db.ExecuteNonQuery(command); }
            catch { }
            sql = "drop procedure RegionInsert";
            command = db.GetSqlStringCommand(sql);
            try { db.ExecuteNonQuery(command); }
            catch { }
            sql = "drop procedure RegionDelete";
            command = db.GetSqlStringCommand(sql);
            try { db.ExecuteNonQuery(command); }
            catch { }
            sql = "drop procedure RegionUpdate";
            command = db.GetSqlStringCommand(sql);
            try { db.ExecuteNonQuery(command); }
            catch { }
        }

        public static void AddTestData(Database db)
        {
            string sql =
                "BEGIN " +
                    "insert into Region values (99, 'Midwest');" +
                    "insert into Region values (100, 'Central Europe');" +
                    "insert into Region values (101, 'Middle East');" +
                    "insert into Region values (102, 'Australia');" +
                    "END;";
            DbCommand testDataInsertion = db.GetSqlStringCommand(sql);
            db.ExecuteNonQuery(testDataInsertion);
        }
    }
}

