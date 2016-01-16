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

using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

using EntLibContrib.Data.TestSupport;

namespace EntLibContrib.Data.Oracle.ManagedDataAccess.Tests.TestSupport
{
    public static class OracleTestConfigurationSource
    {
        public const string OracleConnectionString = "Data Source=XE;User id=system;Password=admin";
        public const string OracleConnectionStringName = "OracleTest";
        public const string OracleProviderName = "Oracle.ManagedDataAccess.Client";

        public static DictionaryConfigurationSource CreateConfigurationSource()
        {
            DictionaryConfigurationSource configSource = TestConfigurationSource.CreateConfigurationSource();

            var connectionString = new ConnectionStringSettings(
                OracleConnectionStringName,
                OracleConnectionString,
                OracleProviderName);

            var connectionStrings = new ConnectionStringsSection();
            connectionStrings.ConnectionStrings.Add(connectionString);

            configSource.Add("connectionStrings", connectionStrings);
            return configSource;
        }
    }
}
