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

using System;
using System.Data;
using System.Data.Common;
using EntLibContrib.Data.Oracle.ManagedDataAccess.Tests.TestSupport;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EntLibContrib.Data.TestSupport;

namespace EntLibContrib.Data.Oracle.ManagedDataAccess.Tests
{
    [TestClass]
    public class OracleParameterDiscoveryFixture
    {
        ParameterCache cache;
        Database db;
        DbConnection connection;
        ParameterDiscoveryFixture baseFixture;
        DbCommand storedProcedure;

        [TestInitialize]
        public void TestInitialize()
        {
            EnvironmentHelper.AssertOracleClientIsInstalled();
            DatabaseProviderFactory factory = new DatabaseProviderFactory(OracleTestConfigurationSource.CreateConfigurationSource());
            db = factory.Create("OracleTest");
            storedProcedure = db.GetStoredProcCommand("NWND_CustOrdersOrders");
            connection = db.CreateConnection();
            connection.Open();
            storedProcedure.Connection = connection;
            cache = new ParameterCache();

            baseFixture = new ParameterDiscoveryFixture(storedProcedure);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (connection != null)
            {
                connection.Dispose();
            }
        }

        [TestMethod]
        public void CanGetParametersForStoredProcedure()
        {
            cache.SetParameters(storedProcedure, db);
            Assert.AreEqual(2, storedProcedure.Parameters.Count);
            Assert.AreEqual("CUR_OUT", ((IDataParameter)storedProcedure.Parameters["CUR_OUT"]).ParameterName);
            Assert.AreEqual("CUSTID", ((IDataParameter)storedProcedure.Parameters["CUSTID"]).ParameterName);
        }

        [TestMethod]
        public void IsCacheUsed()
        {
            ParameterDiscoveryFixture.TestCache testCache = new ParameterDiscoveryFixture.TestCache();
            testCache.SetParameters(storedProcedure, db);

            DbCommand storedProcDuplicate = db.GetStoredProcCommand("NWND_CustOrdersOrders");
            storedProcDuplicate.Connection = connection;
            testCache.SetParameters(storedProcDuplicate, db);

            Assert.IsTrue(testCache.CacheUsed, "Cache is not used");
        }

        [TestMethod]
        public void CanDiscoverFeaturesWhileInsideTransaction()
        {
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                DbCommand storedProcedure = db.GetStoredProcCommand("NWND_CustOrdersOrders");
                storedProcedure.Connection = transaction.Connection;
                storedProcedure.Transaction = transaction;

                db.DiscoverParameters(storedProcedure);

                Assert.AreEqual(2, storedProcedure.Parameters.Count);
            }
        }

        [TestMethod]
        public void CanCreateStoredProcedureCommand()
        {
            baseFixture.CanCreateStoredProcedureCommand();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetParametersWithNullCommandThrows()
        {
            cache.SetParameters(null, db);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetParametersWithNullDatabaseThrows()
        {
            cache.SetParameters(storedProcedure, null);
        }
    }
}
