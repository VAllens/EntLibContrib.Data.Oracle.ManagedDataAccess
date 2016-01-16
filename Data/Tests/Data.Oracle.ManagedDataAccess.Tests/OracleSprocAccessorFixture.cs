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
using System.Linq;
using System.Data;
using System.Data.Common;
using EntLibContrib.Data.Oracle.ManagedDataAccess.Tests.TestSupport;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.EnterpriseLibrary.Common.TestSupport.ContextBase;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

namespace EntLibContrib.Data.Oracle.ManagedDataAccess.Tests
{
    public abstract class OracleSprocAccessorContext : ArrangeActAssert
    {
        protected ConnectionState ConnectionState;
        protected int NumberOfConnectionsCreated;
        protected string ConnectionString;
        protected OracleDatabase Database;

        protected override void Arrange()
        {
            EnvironmentHelper.AssertOracleClientIsInstalled();
            DatabaseProviderFactory factory = new DatabaseProviderFactory(OracleTestConfigurationSource.CreateConfigurationSource());
            Database = (OracleDatabase)factory.Create("OracleTest");
            ConnectionString = Database.ConnectionString;
        }

        protected class Product
        {
            public string TenMostExpensiveProducts { get; set; }
            public decimal UnitPrice { get; set; }
        }

        private class TestableSqlDatabase : OracleDatabase
        {
            OracleSprocAccessorContext context;
            public TestableSqlDatabase(string connectionstring, OracleSprocAccessorContext context)
                : base(connectionstring)
            {
                this.context = context;
            }

            public override DbConnection CreateConnection()
            {
                context.NumberOfConnectionsCreated++;

                DbConnection connection = base.CreateConnection();
                connection.StateChange += (sender, args) => { context.ConnectionState = args.CurrentState; };
                return connection;
            }
        }
    }

    [TestClass]
    public class WhenExecutingSproc : OracleSprocAccessorContext
    {
        [TestMethod]
        public void ThenConvertsResultInObjects()
        {
            var x = Database.ExecuteOracleSprocAccessor<Product>("NWND_TenMostExpensiveProducts");
            Assert.AreEqual(10, x.Count());
            Assert.IsNotNull(x.First().TenMostExpensiveProducts);
            Assert.AreNotEqual(0, x.First().UnitPrice);
        }
    }

    [TestClass]
    public class WhenExecutingSprocPassingRowMapper : OracleSprocAccessorContext
    {
        IRowMapper<Product> rowMapper;

        protected override void Arrange()
        {
            base.Arrange();

            rowMapper = new RowMapper();
        }

        [TestMethod]
        public void ThenConvertsResultInObjectsUsingRowMapper()
        {
            var x = Database.ExecuteOracleSprocAccessor<Product>("NWND_TenMostExpensiveProducts", rowMapper);
            Assert.AreEqual(10, x.Count());
            Assert.AreEqual("pname", x.First().TenMostExpensiveProducts);
            Assert.AreEqual(23, x.First().UnitPrice);
        }

        private class RowMapper : IRowMapper<Product>
        {
            public Product MapRow(IDataRecord row)
            {
                return new Product
                {
                    TenMostExpensiveProducts = "pname",
                    UnitPrice = 23
                };
            }
        }
    }

    [TestClass]
    public class WhenExecutingSprocPassingResultSetMapper : OracleSprocAccessorContext
    {
        IResultSetMapper<Product> resultSetMapper;

        protected override void Arrange()
        {
            base.Arrange();

            resultSetMapper = new ResultSetMapper();
        }

        [TestMethod]
        public void ThenConvertsResultInObjectsUsingRowMapper()
        {
            var x = Database.ExecuteOracleSprocAccessor<Product>("NWND_TenMostExpensiveProducts", resultSetMapper);
            Assert.AreEqual(1, x.Count());
            Assert.AreEqual("pname", x.First().TenMostExpensiveProducts);
            Assert.AreEqual(23, x.First().UnitPrice);
        }

        private class ResultSetMapper : IResultSetMapper<Product>
        {
            public IEnumerable<Product> MapSet(IDataReader reader)
            {
                yield return new Product
                {
                    TenMostExpensiveProducts = "pname",
                    UnitPrice = 23
                };
            }
        }
    }

    [TestClass]
    public class WhenExecutingSprocPassingParameterMapper : OracleSprocAccessorContext
    {
        IParameterMapper parameterMapper;

        protected override void Arrange()
        {
            base.Arrange();

            parameterMapper = new ParameterMapper();
        }

        [TestMethod]
        public void ThenConvertsResultInObjectsUsingRowMapper()
        {
            var x = Database.ExecuteOracleSprocAccessor<ProductSales>("NWND_SalesByCategory", parameterMapper);
            Assert.IsNotNull(x);
            Assert.AreEqual("Chai", x.First().ProductName);
        }

        private class ParameterMapper : IParameterMapper
        {
            public void AssignParameters(DbCommand command, object[] parameterValues)
            {
                command.Parameters.Add(new OracleParameter("CatName", "Beverages"));
                command.Parameters.Add(new OracleParameter("OrdYear", "1996"));
            }
        }

        private class ProductSales
        {
            public string ProductName { get; set; }
            public double TotalPurchase { get; set; }
        }
    }

    [TestClass]
    public class WhenExecutingSprocPassingParameterMapperAndRowMapper : OracleSprocAccessorContext
    {

        IParameterMapper parameterMapper;
        IRowMapper<ProductSales> rowMapper;

        protected override void Arrange()
        {
            base.Arrange();

            parameterMapper = new ParameterMapper();
            rowMapper = new RowMapper();
        }

        [TestMethod]
        public void ThenConvertsResultInObjectsUsingRowMapper()
        {
            var x = Database.ExecuteOracleSprocAccessor<ProductSales>("NWND_SalesByCategory", parameterMapper, rowMapper);
            Assert.IsNotNull(x);
            Assert.AreEqual("pname", x.First().ProductName);
            Assert.AreEqual(12, x.First().TotalPurchase);
        }

        private class ParameterMapper : IParameterMapper
        {
            public void AssignParameters(DbCommand command, object[] parameterValues)
            {
                command.Parameters.Add(new OracleParameter("CatName", "Beverages"));
                command.Parameters.Add(new OracleParameter("OrdYear", "1996"));
            }
        }

        private class ProductSales
        {
            public string ProductName { get; set; }
            public double TotalPurchase { get; set; }
        }

        private class RowMapper : IRowMapper<ProductSales>
        {
            public ProductSales MapRow(IDataRecord row)
            {
                return new ProductSales
                {
                    ProductName = "pname",
                    TotalPurchase = 12
                };
            }
        }
    }

    [TestClass]
    public class WhenExecutingSprocPassingParameterMapperAndResultSetMapper : OracleSprocAccessorContext
    {
        IParameterMapper parameterMapper;
        IResultSetMapper<ProductSales> resultSetMapper;

        protected override void Arrange()
        {
            base.Arrange();

            parameterMapper = new ParameterMapper();
            resultSetMapper = new ResultSetMapper();
        }

        [TestMethod]
        public void ThenConvertsResultInObjectsUsingRowMapper()
        {
            var x = Database.ExecuteOracleSprocAccessor<ProductSales>("NWND_SalesByCategory", parameterMapper, resultSetMapper);
            Assert.IsNotNull(x);
            Assert.AreEqual(1, x.Count());
            Assert.AreEqual("pname", x.First().ProductName);
            Assert.AreEqual(12, x.First().TotalPurchase);
        }

        private class ParameterMapper : IParameterMapper
        {
            public void AssignParameters(DbCommand command, object[] parameterValues)
            {
                command.Parameters.Add(new OracleParameter("CatName", "Beverages"));
                command.Parameters.Add(new OracleParameter("OrdYear", "1996"));
            }
        }

        private class ProductSales
        {
            public string ProductName { get; set; }
            public double TotalPurchase { get; set; }
        }

        private class ResultSetMapper : IResultSetMapper<ProductSales>
        {
            public IEnumerable<ProductSales> MapSet(IDataReader reader)
            {
                yield return new ProductSales
                {
                    ProductName = "pname",
                    TotalPurchase = 12
                };
            }
        }
    }

    [TestClass]
    public class WhenCreatingSprocAccessor : OracleSprocAccessorContext
    {
        [TestMethod]
        public void ThenCanCreateSprocAccessor()
        {
            var sprocAccessor = Database.CreateOracleSprocAccessor<Product>("NWND_TenMostExpensiveProducts");
            Assert.IsNotNull(sprocAccessor);
        }

        [TestMethod]
        public void ThenCanCreateSprocAccessorWithRowMapper()
        {
            var sprocAccessor = Database.CreateOracleSprocAccessor<Product>("NWND_TenMostExpensiveProducts", MapBuilder<Product>.MapNoProperties().Build());
            Assert.IsNotNull(sprocAccessor);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThenCreateSprocAccessorWithNullMapperThrows()
        {
            Database.CreateOracleSprocAccessor<Product>("prodedure name", (IRowMapper<Product>)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThenCreateSprocAccessorWithNullResultSetMapperThrows()
        {
            Database.CreateOracleSprocAccessor<Product>("prodedure name", (IResultSetMapper<Product>)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThenCreateSprocAccessorWithNullParameterMapperThrows()
        {
            Database.CreateOracleSprocAccessor<Product>("prodedure name", null, MapBuilder<Product>.BuildAllProperties());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThenCreateSprocAccessorWithNullDatabaseThrows()
        {
            new SprocAccessor<Product>(null, "procedure name", MapBuilder<Product>.BuildAllProperties());
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThenCreateSprocAccessorWithEmptyArgThrowsArgumentException()
        {
            Database.CreateOracleSprocAccessor<Product>(string.Empty);
        }
    }

    [TestClass]
    public class WhenExecutingSprocAccessor : OracleSprocAccessorContext
    {
        private DataAccessor<Product> accessor;

        protected override void Arrange()
        {
            base.Arrange();

            accessor = Database.CreateOracleSprocAccessor<Product>("NWND_TenMostExpensiveProducts");
        }

        [TestMethod]
        public void ThenReturnsResultsAsEnumerable()
        {
            Assert.IsNotNull(accessor.Execute());
        }

        [TestMethod]
        public void ThenClosesConnectionAfterResultsAreEnumerated()
        {
            var result = accessor.Execute();
            Assert.AreEqual(10, result.Count());

            Assert.AreEqual(ConnectionState.Closed, base.ConnectionState);
        }

        [TestMethod]
        public void ThenClosesConnectionEvenThoughEnumerationIsntFinished()
        {
            var result = accessor.Execute();
            var foo = result.First();

            Assert.AreEqual(ConnectionState.Closed, base.ConnectionState);
        }

        [TestMethod]
        public void ThenClosesConnectionAfterIteratingPartially()
        {
            var resultSet = accessor.Execute();

            int i = 0;
            foreach (var result in resultSet)
            {
                i++;
                if (i == 3) break;
            }

            Assert.AreEqual(ConnectionState.Closed, base.ConnectionState);
        }

        [TestMethod]
        public void ThenConnectionIsClosedAfterExecuting()
        {
            var result = accessor.Execute().ToList();
            Assert.AreEqual(ConnectionState.Closed, base.ConnectionState);
        }

        [TestMethod]
        public void ThenSetsPropertiesBasedOnPropertyName()
        {
            var result = accessor.Execute();
            Product firstProduct = result.First();
            Assert.IsNotNull(firstProduct.TenMostExpensiveProducts);
            Assert.AreNotEqual(0d, firstProduct.UnitPrice);
        }
    }


    [TestClass]
    public class WhenParameterizedSprocAccessorIsCreated : OracleSprocAccessorContext
    {
        private DataAccessor<ProductSales> accessor;

        protected override void Arrange()
        {
            base.Arrange();

            accessor = Database.CreateOracleSprocAccessor<ProductSales>("NWND_SalesByCategory");
        }

        [TestMethod]
        public void ThenCanPassParameterInExecute()
        {
            var result = accessor.Execute("Beverages", "1998");
            Assert.IsNotNull(result);
            var enumerared = result.ToList();
        }


        private class ProductSales
        {
            public string ProductName { get; set; }
            public double TotalPurchase { get; set; }
        }
    }

    [TestClass]
    public class WhenSprocAccessorIsCreatedPassingCustomRowMapper : OracleSprocAccessorContext
    {
        private DataAccessor<Product> accessor;
        private CustomMapper mapper;

        protected override void Arrange()
        {
            base.Arrange();

            mapper = new CustomMapper();
            accessor = Database.CreateOracleSprocAccessor<Product>("NWND_TenMostExpensiveProducts", mapper);
        }

        [TestMethod]
        public void ThenMapperIsCalledForEveryRow()
        {
            accessor.Execute().ToList();
            Assert.AreEqual(10, mapper.MapRowCallCount);
        }

        private class CustomMapper : IRowMapper<Product>
        {
            public int MapRowCallCount = 0;

            public Product MapRow(IDataRecord row)
            {
                MapRowCallCount++;
                return new Product();
            }
        }
    }

    [TestClass]
    public class WhenSprocAccessorIsCreatedPassingCustomResultSetMapper : OracleSprocAccessorContext
    {
        private DataAccessor<Product> accessor;
        private CustomMapper mapper;

        protected override void Arrange()
        {
            base.Arrange();

            mapper = new CustomMapper();
            accessor = Database.CreateOracleSprocAccessor<Product>("NWND_TenMostExpensiveProducts", mapper);
        }

        [TestMethod]
        public void ThenMapperIsCalledOncePerExecute()
        {
            accessor.Execute().ToList();
            Assert.AreEqual(1, mapper.MapSetCallCount);
        }

        private class CustomMapper : IResultSetMapper<Product>
        {
            public int MapSetCallCount = 0;

            public IEnumerable<Product> MapSet(IDataReader reader)
            {
                MapSetCallCount++;
                return Enumerable.Empty<Product>();
            }
        }
    }

    [TestClass]
    public class WhenSprocAccessorIsCreatedPassingParameterMapper : OracleSprocAccessorContext
    {
        private DataAccessor<ProductSales> accessor;
        private SqlParameterMapper parameterMapper;

        protected override void Arrange()
        {
            base.Arrange();
            parameterMapper = new SqlParameterMapper();

            accessor = Database.CreateOracleSprocAccessor<ProductSales>("NWND_SalesByCategory", parameterMapper);
        }

        [TestMethod]
        public void ThenParameterMapperIsCalledOnceOnExecute()
        {
            var result = accessor.Execute("Beverages", "1996").ToList();
            Assert.AreEqual(2, parameterMapper.AssignParametersCallCount);
        }

        [TestMethod]
        public void ThenParameterMapperOutputIsUsedToExecuteSproc()
        {
            var result = accessor.Execute("Beverages", "1996");
            Assert.IsNotNull(result);
            Assert.AreEqual("Chai", result.First().ProductName);
        }

        private class SqlParameterMapper : IParameterMapper
        {
            public int AssignParametersCallCount = 0;

            public void AssignParameters(DbCommand command, object[] parameterValues)
            {
                AssignParametersCallCount++;

                DbParameter parameter = command.CreateParameter();
                parameter.ParameterName = "CatName";
                parameter.Value = parameterValues.First();

                command.Parameters.Add(parameter);

                AssignParametersCallCount++;

                DbParameter parameter2 = command.CreateParameter();
                parameter2.ParameterName = "OrdYear";
                parameter2.Value = parameterValues.ElementAt(1);

                command.Parameters.Add(parameter2);
            }
        }

        private class ProductSales
        {
            public string ProductName { get; set; }
            public double TotalPurchase { get; set; }
        }
    }
}
