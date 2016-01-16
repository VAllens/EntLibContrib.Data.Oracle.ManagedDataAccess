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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.TestSupport.ContextBase;
using EntLibContrib.Data.Oracle.ManagedDataAccess.Tests.TestSupport;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EntLibContrib.Data.Oracle.ManagedDataAccess.Tests
{
#pragma warning disable 612, 618
    [TestClass]
    public class WhenSprocAccessorIsCreatedForOracleDatabase : ArrangeActAssert
    {
        OracleDatabase database;

        protected override void Arrange()
        {
            EnvironmentHelper.AssertOracleClientIsInstalled();
            database = new OracleDatabase("Data Source=XE;User id=system;Password=admin");
        }

        [TestMethod]
        public void ThenCanExecuteParameterizedSproc()
        {
            // JBourgault (2013-08-15):
            // The CreateSprocAccessor Databse extension doesn't work properly with Odp.NET.
            // In a nutshell, by wrapping the command in a using, SprocAccessor.Execute(...) dispose 
            // the internal OracleCommand and its parameters before actually calling the database.
            var accessor = database.CreateOracleSprocAccessor<Customer>("PKGNORTHWIND.NWND_GetCustomersTest");
            var result = accessor.Execute("BLAUS", null).ToArray();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
        }

        private class Customer
        {
            public string ContactName { get; set; }
        }
    }
#pragma warning restore 612, 618
}
