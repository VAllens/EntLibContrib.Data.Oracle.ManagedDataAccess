using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;

namespace EntLibContrib.Data.Oracle.ManagedDataAccess
{
    /// <summary>
    /// Represents a stored procedure call to the database that will return an enumerable of <typeparamref name="TResult"/>.
    /// </summary>
    /// <typeparam name="TResult">The element type that will be used to consume the result set.</typeparam>
    public class OracleSprocAccessor<TResult> : CommandAccessor<TResult>
    {
        readonly IParameterMapper _parameterMapper;
        readonly string _procedureName;

        /// <summary>
        /// Creates a new instance of <see cref="SprocAccessor&lt;TResult&gt;"/> that works for a specific <paramref name="database"/>
        /// and uses <paramref name="rowMapper"/> to convert the returned rows to clr type <typeparamref name="TResult"/>.
        /// </summary>
        /// <param name="database">The <see cref="Database"/> used to execute the Transact-SQL.</param>
        /// <param name="procedureName">The stored procedure that will be executed.</param>
        /// <param name="rowMapper">The <see cref="IRowMapper&lt;TResult&gt;"/> that will be used to convert the returned data to clr type <typeparamref name="TResult"/>.</param>
        public OracleSprocAccessor(OracleDatabase database, string procedureName, IRowMapper<TResult> rowMapper)
            : this(database, procedureName, new DefaultParameterMapper(database), rowMapper)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="SprocAccessor&lt;TResult&gt;"/> that works for a specific <paramref name="database"/>
        /// and uses <paramref name="resultSetMapper"/> to convert the returned set to an enumerable of clr type <typeparamref name="TResult"/>.
        /// </summary>
        /// <param name="database">The <see cref="Database"/> used to execute the Transact-SQL.</param>
        /// <param name="procedureName">The stored procedure that will be executed.</param>
        /// <param name="resultSetMapper">The <see cref="IResultSetMapper&lt;TResult&gt;"/> that will be used to convert the returned set to an enumerable of clr type <typeparamref name="TResult"/>.</param>
        public OracleSprocAccessor(OracleDatabase database, string procedureName, IResultSetMapper<TResult> resultSetMapper)
            : this(database, procedureName, new DefaultParameterMapper(database), resultSetMapper)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="SprocAccessor&lt;TResult&gt;"/> that works for a specific <paramref name="database"/>
        /// and uses <paramref name="rowMapper"/> to convert the returned rows to clr type <typeparamref name="TResult"/>.
        /// The <paramref name="parameterMapper"/> will be used to interpret the parameters passed to the Execute method.
        /// </summary>
        /// <param name="database">The <see cref="Database"/> used to execute the Transact-SQL.</param>
        /// <param name="procedureName">The stored procedure that will be executed.</param>
        /// <param name="parameterMapper">The <see cref="IParameterMapper"/> that will be used to interpret the parameters passed to the Execute method.</param>
        /// <param name="rowMapper">The <see cref="IRowMapper&lt;TResult&gt;"/> that will be used to convert the returned data to clr type <typeparamref name="TResult"/>.</param>
        public OracleSprocAccessor(OracleDatabase database, string procedureName, IParameterMapper parameterMapper, IRowMapper<TResult> rowMapper)
            : base(database, rowMapper)
        {
            if (string.IsNullOrEmpty(procedureName)) throw new ArgumentException("The value can not be a null or empty string.");
            if (parameterMapper == null) throw new ArgumentNullException(nameof(parameterMapper));

            _procedureName = procedureName;
            _parameterMapper = parameterMapper;
        }

        /// <summary>
        /// Creates a new instance of <see cref="SprocAccessor&lt;TResult&gt;"/> that works for a specific <paramref name="database"/>
        /// and uses <paramref name="resultSetMapper"/> to convert the returned set to an enumerable of clr type <typeparamref name="TResult"/>.
        /// The <paramref name="parameterMapper"/> will be used to interpret the parameters passed to the Execute method.
        /// </summary>
        /// <param name="database">The <see cref="Database"/> used to execute the Transact-SQL.</param>
        /// <param name="procedureName">The stored procedure that will be executed.</param>
        /// <param name="parameterMapper">The <see cref="IParameterMapper"/> that will be used to interpret the parameters passed to the Execute method.</param>
        /// <param name="resultSetMapper">The <see cref="IResultSetMapper&lt;TResult&gt;"/> that will be used to convert the returned set to an enumerable of clr type <typeparamref name="TResult"/>.</param>
        public OracleSprocAccessor(OracleDatabase database, string procedureName, IParameterMapper parameterMapper, IResultSetMapper<TResult> resultSetMapper)
            : base(database, resultSetMapper)
        {
            if (string.IsNullOrEmpty(procedureName)) throw new ArgumentException("The value can not be a null or empty string.");
            if (parameterMapper == null) throw new ArgumentNullException(nameof(parameterMapper));

            _procedureName = procedureName;
            _parameterMapper = parameterMapper;
        }

        /// <summary>
        /// Executes the stored procedure and returns an enumerable of <typeparamref name="TResult"/>.
        /// The enumerable returned by this method uses deferred loading to return the results.
        /// </summary>
        /// <param name="parameterValues">Values that will be interpret by an <see cref="IParameterMapper"/> and function as parameters to the stored procedure.</param>
        /// <returns>An enumerable of <typeparamref name="TResult"/>.</returns>
        public override IEnumerable<TResult> Execute(params object[] parameterValues)
        {
            DbCommand command = null;

            try
            {
                command = Database.GetStoredProcCommand(_procedureName);

                _parameterMapper.AssignParameters(command, parameterValues);

                #region reference : http://stackoverflow.com/questions/17828684/odp-net-in-microsoft-enterprise-library-6-0

                //return base.Execute(command).ToArray();

                //foreach added by ngpojne.  This allows the command to be used before it is disposed of.
                foreach (TResult result in base.Execute(command))
                {
                    yield return result;
                }

                #endregion
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
            }
        }

        private class DefaultParameterMapper : IParameterMapper
        {
            readonly Database _database;
            public DefaultParameterMapper(Database database)
            {
                _database = database;
            }

            public void AssignParameters(DbCommand command, object[] parameterValues)
            {
                if (parameterValues.Length > 0)
                {
                    GuardParameterDiscoverySupported();
                    _database.AssignParameters(command, parameterValues);
                }
            }

            private void GuardParameterDiscoverySupported()
            {
                if (!_database.SupportsParemeterDiscovery)
                {
                    throw new InvalidOperationException(
                        string.Format(CultureInfo.CurrentCulture,
                                      "The database type \"{0}\" does not support automatic parameter discovery. Use an IParameterMapper instead.",
                                      _database.GetType().FullName));
                }
            }
        }

        /// <summary>
        /// Begin executing the database object asynchronously, 
        /// returning a System.IAsyncResult object that can be used to retrieve the result set after the operation completes.
        /// </summary>
        /// <param name="callback">Callback to execute when the operation's results are available. May be null if you don't wish to use a callback.</param>
        /// <param name="state">Extra information that will be passed to the callback. May be null.</param>
        /// <param name="parameterValues">Parameters to pass to the database.</param>
        /// <returns></returns>
        public override IAsyncResult BeginExecute(AsyncCallback callback, object state, params object[] parameterValues)
        {
            throw new NotSupportedException();
        }
    }
}