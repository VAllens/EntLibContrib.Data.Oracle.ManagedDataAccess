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
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;

namespace EntLibContrib.Data.TestSupport
{
    public class TestConfigurationSource
    {
        public const string NorthwindDummyUser = "system";
        public const string NorthwindDummyPassword = "admin";

        public static DictionaryConfigurationSource CreateConfigurationSource()
        {
            DictionaryConfigurationSource source = new DictionaryConfigurationSource();

            DatabaseSettings settings = new DatabaseSettings();
            settings.DefaultDatabase = "Service_Dflt";
            settings.ProviderMappings.Add(new DbProviderMapping("Oracle.ManagedDataAccess.Client", "EntLibContrib.Data.Oracle.ManagedDataAccess.OracleDatabase, EntLibContrib.Data.Oracle.ManagedDataAccess, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"));

            OracleConnectionSettings oracleConnectionSettings = new OracleConnectionSettings();
            OracleConnectionData data = new OracleConnectionData();
            data.Name = "OracleTest";
            data.Packages.Add(new OraclePackageData("TESTPACKAGE", "TESTPACKAGETOTRANSLATE"));
            data.Packages.Add(new OraclePackageData("PKGNORTHWIND", "NWND_"));
            data.Packages.Add(new OraclePackageData("PKGENTLIB", "RegionSelect"));
            oracleConnectionSettings.OracleConnectionsData.Add(data);

            source.Add(DatabaseSettings.SectionName, settings);
            source.Add(OracleConnectionSettings.SectionName, oracleConnectionSettings);

            return source;
        }
    }
}
