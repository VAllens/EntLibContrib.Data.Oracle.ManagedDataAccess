# patterns & practices: Enterprise Library Contrib
## Overview
Enterprise Library Contribution Data ODP.NET Managed Provider, Support for Oracle 12c.
Modified from [https://entlibcontrib.codeplex.com](https://entlibcontrib.codeplex.com).

## Welcome to the **Enterprise Library Contrib** Project!

EntLib Contrib is a community-developed library of extensions to the patterns &amp; practices
[Enterprise Library](http://entlib.codeplex.com). 

In short, if you wrote it, why not share it with the world so that they can benefit from all your hard work? With your help EntLib Contrib will evolve to include a wide range of additional providers, extensions, tools and even application blocks that can be
 used with Enterprise Library.

Note: Extensions in the core EntLib Contrib project should work against official builds of Enterprise Library, and should not require any modifications to the core Enterprise Library code. Contributions that involve modifications to Enterprise Library code
 will be considered, but any such contributions will be made available as separate releases on this site.

### News!

*   **Sept 12th 2013 - A new DAAB data provider for Oracle ODP.NET 11g compatible with EntLib 6.0.1304.0 is now available!** The new data provider can be downloaded via NuGet (or from the Downloads section). For those who want to know more about the implementation,
 feel free to download the source code from either the Downloads or the Source Code section. The source package also includes unit tests (with the required Oracle Northwind SQL scripts) and a quick start project that shows how to use the provider. Documentation
 explaining how to use the provider is available under the Documentation section as well.

*   **Oct 29th 2011 - The first official release of EntLibContrib v5 is now available for download!** We included the EntLib 5.0.505.0 assemblies and the configuration tool in the EntLibContrib binaries package. And for those of you who want to know more
 about the implementation, feel free to download the source package (which also includes the unit tests and some quick starts). The documentation is currently being updated and should be released soon. Note that most of the v4.1 documentation is still valid
 and can be used to have an overview of the features. Enjoy!

*   **Oct 22th 2011 - The EntLibContrib v5 binaries are now available through NuGet!** Just like for EntLib, NuGet will probably become the primary ship vehicle for the compiled binaries. Each package includes the PDBs and the XML documentation files. The
 source code matching the PDBs is also available through NuGet. This package also includes the quick starts. For those of you who want to use the Enterprise Library Configuration Tool with the EntLibContrib bits, feel free to download the latest version of
 the tool using the Extension Manager.

*   **Oct 18th 2011 - The port to EntLib 5 is progressing!** - The Query Application Block, the Oracle ODP.NET Data Provider, the Logging and PIAB extensions are now all compatible with EntLib v5. Please find the latest bits under the Source Code section.
 The other data providers are next and will be followed by the Resource and Mapping application blocks.

*   **June 15th 2011 - The port of the 4.1 codebase to EntLib 5 has officially started!** - It's been a long time since the last check-in but things are moving again! Common, EHAB and VAB related contrib blocks are now compatible with Entlib 5 and available
 in the Source Code section. The other blocks should be ported one-by-one in the next fews weeks/month. Let us know which one you would like to be ported first!

*   **June 20th 2010 - the new Mapping Application Block (MAB) is now complete!** - This is now the third full block to be added to Enterprise Library Contrib. not only that but by combining with the Query Application Block (QAB) we now have an
**Object Relational Mapping (ORM)** tool for the Enterprise Library. The MAB completes the cycle by managing the data transfer objects used by the QAB and mapping them to and from fully typed domain objects. Currently the MAB is only available from souce
 code but I will be putting together a new release as it has been 6 months now since the last one.

*   **A new January 2010 release has been published.** - This is not just an upgrade of all of the user contributions built for the
**Enterprise Library v4.1** core, but also some brand new features: For starters we have 5 new or completely updated DAAB Data Providers; IBM DB2, MySql, Oracle ODP.NET, PostgreSql and SQLite and, in addition, we also have a
**brand new full application block** with the Query Application Block (QAB). The Resource Application Block has been upgraded to integrate with the Unity Application Block. You will also find that for this release the source code organisation is fully consistent
 with the core Enterprise Library v4.1. All folders and files are in the same places as for the Enterprise Library and all naming conventions consistent too with the exception that the top level namespace is
**EntLibContrib** instead of **Microsoft.Practices.EnterpriseLibrary**.

*   **Two more Providers for the DAAB v4.1.** - In addition to three new providers already in place the EntLib Contribution v4.1 core upgrade you will find another two new DAAB data providers for
**PostgreSQL** and **SQLite** and also an upgraded Extended SQL Provider (**SqlEx**). Each provider has been given a full set of Unit Tests to match those applied to the MS SQL Server data provider and, of course, a matching Northwind Database so the
 tests are all consistent. Simply download the latest changeset from source code. This completes all changes to the DAAB for the time being as focus will now move to the other blocks.

*   **Three new Providers for the DAAB v4.1.** - In the EntLib Contribution v4.1 core upgrade you will find three new DAAB data providers for Oracle
**ODP.NET**, IBM **DB2** and **MySQL**. Each provider has been given a full set of Unit Tests to match those applied to the MS SQL Server data provider and, of course, a matching Northwind Database so the tests can all be consistent. Simply download
 the latest changeset from source code. Coming soon are two more providers, one for PostgreSQL and one for SQLite. All these providers existed in the v3.1 days but without the Unit Tests and corresponding Northwind DB. In addition these data providers have
 been tested with the latest versions of their respective databases and .NET Providers.

### What's in EntLib Contrib?

The latest release of EntLib Contrib is [
Enterprise Library Contrib - 5.0 (Oct 2011)](https://entlibcontrib.codeplex.com/releases/view/69407). It contains the following functionality. For more detailed documentation,
[
follow this link](https://entlibcontrib.codeplex.com/wikipage?title=EntLibContrib41Doc&amp;referringTitle=Home "EntLibContrib41Doc&amp;referringTitle=Home") or click on the **Documentation** tab:

### Common extensions

*   **TypeConfigurationElement&lt;T&gt;** - A Polymorphic Configuration Element without having to be part of a PolymorphicConfigurationElementCollection.

*   **AnonymousConfigurationElement** - A Configuration element that can be uniquely identified without having to define its name explicitly.

### Data Access Application Block extensions

*   **[DB2 Provider](https://entlibcontrib.codeplex.com/wikipage?title=DB2DataProvider41&amp;referringTitle=Home "DB2DataProvider41&amp;referringTitle=Home")** - IBM DB2 data provider for the Data Access Application Block.

*   **[MySql Provider](https://entlibcontrib.codeplex.com/wikipage?title=MySqlDataProvider41&amp;referringTitle=Home "MySqlDataProvider41&amp;referringTitle=Home")** - MySql data provider for the Data Access Application Block.

*   **[ODP.NET Provider](https://entlibcontrib.codeplex.com/wikipage?title=OracleDataProvider41&amp;referringTitle=Home "OracleDataProvider41&amp;referringTitle=Home")** - Oracle ODP.NET data provider for the Data Access Application Block.

*   **[PostgreSQL Provider](https://entlibcontrib.codeplex.com/wikipage?title=PostgreSQLDataProvider41&amp;referringTitle=Home "PostgreSQLDataProvider41&amp;referringTitle=Home")** - PostgreSQL data provider for the Data Access Application Block.

*   **[SQLite Provider](https://entlibcontrib.codeplex.com/wikipage?title=SQLiteDataProvider41&amp;referringTitle=Home "SQLiteDataProvider41&amp;referringTitle=Home")** - SQLite data provider for the Data Access Application Block.

*   **[SqlEx Provider](https://entlibcontrib.codeplex.com/wikipage?title=ExtendedSqlDataProvider41&amp;referringTitle=Home "ExtendedSqlDataProvider41&amp;referringTitle=Home")** - This data provider extends the SqlDatabase provider included in the Data Access Application Block.

### Exception Handling Application Block extensions

*   **[SqlException Wrap Handler](https://entlibcontrib.codeplex.com/wikipage?title=SqlExceptionWrapHandler41&amp;referringTitle=Home "SqlExceptionWrapHandler41&amp;referringTitle=Home")** - Exception handler that will wrap a SqlException with different exceptions based on the SQL Server error code.

### Logging Application Block extensions

*   **[LogParser](https://entlibcontrib.codeplex.com/wikipage?title=LogParser41&amp;referringTitle=Home "LogParser41&amp;referringTitle=Home")** - Combines the benefits of the Enterprise Library Logging Application Block with the ability to deserialize from a human readable log text file
 all LogEntry objects back. This enables sophisticated log filter capabilities with LINQ on normal log files (.NET 2.0 and Orcas samples) with very few lines of code. The Log parser now includes a TimeStamp parser.

### Policy Injection Application Block extensions

*   **PIAB Call Handlers**

    *   CursorCallHandler: Temporarily changes the cursor while the next handler is being processed.

        *   OneWayCallHandler: Queues the call to the next handler on the ThreadPool*   SynchronizedCallHandler: Uses ISynchronizeInvoke to invoke the next handler*   ThreadSafeCallHandler: Synchronizes access to the next handler*   TransactionScopeCallHandler: Wraps the next handler with a TransactionScope

### Query Application Block

*   **[Query Application Block 4.1](https://entlibcontrib.codeplex.com/wikipage?title=Query%20Application%20Block%204.1&amp;referringTitle=Home "Query%20Application%20Block%204.1&amp;referringTitle=Home")** - Next level of integration up from the DAAB providing a common interface for data stored in a DB, XML
 file or Web/WCF service. A full Application Block complete with configuration console designer, Unity support, group policy support and instrumentation. This block is like NHibernate without Object Mapping.

### Resource Application Block

*   **[Resource Application Block 4.1](https://entlibcontrib.codeplex.com/wikipage?title=Resource%20Application%20Block%204.1&amp;referringTitle=Home "Resource%20Application%20Block%204.1&amp;referringTitle=Home")** - A full Application Block of configurable providers for Globalization and Localization, complete
 with configuration console designer, Unity support, group policy support and instrumentation.

### Validation Application Block extensions

*   **Validators**

    *   **[CollectionCountValidator](https://entlibcontrib.codeplex.com/wikipage?title=VABContributions41&amp;referringTitle=Home&amp;ANCHOR#CollectionCountValidator "VABContributions41&amp;referringTitle=Home&amp;ANCHOR#CollectionCountValidator")**

        *   **[CompositeRulesetValidator](https://entlibcontrib.codeplex.com/wikipage?title=VABContributions41&amp;referringTitle=Home&amp;ANCHOR#CompositeRulesetValidator "VABContributions41&amp;referringTitle=Home&amp;ANCHOR#CompositeRulesetValidator")**

        *   **[TypeValidator&lt;T&gt;](https://entlibcontrib.codeplex.com/wikipage?title=VABContributions41&amp;referringTitle=Home&amp;ANCHOR#TypeValidator "VABContributions41&amp;referringTitle=Home&amp;ANCHOR#TypeValidator")**

        *   **[ObjectValidator&lt;T&gt;](https://entlibcontrib.codeplex.com/wikipage?title=VABContributions41&amp;referringTitle=Home&amp;ANCHOR#ObjectValidator "VABContributions41&amp;referringTitle=Home&amp;ANCHOR#ObjectValidator")**

        *   **[EnumDefinedValidator](https://entlibcontrib.codeplex.com/wikipage?title=VABContributions41&amp;referringTitle=Home&amp;ANCHOR#EnumDefinedValidator "VABContributions41&amp;referringTitle=Home&amp;ANCHOR#EnumDefinedValidator")**

*   **Designtime enhancements**

    *   **[Lightweight type picker](https://entlibcontrib.codeplex.com/wikipage?title=LightweightTypePicker41&amp;referringTitle=Home "LightweightTypePicker41&amp;referringTitle=Home")**: An alternative type-picker that allows you to enter a typename in a text-box instead of using the tree-view.

        *   **[Test command for Validation Rules](https://entlibcontrib.codeplex.com/wikipage?title=TestCommandForValidationRules41&amp;referringTitle=Home "TestCommandForValidationRules41&amp;referringTitle=Home")**: A dialog that allows to test and play around with validators inside the configuration console.

*   **Other extensions**

    *   **[DefaultValidators](https://entlibcontrib.codeplex.com/wikipage?title=VABContributions41&amp;referringTitle=Home&amp;ANCHOR#DefaultValidators "VABContributions41&amp;referringTitle=Home&amp;ANCHOR#DefaultValidators")**: The DefaultValidators class provides pre-allocated validators. Use them instead of instantiating new
 ones every time you need common validators.*   **[ArgumentValidation](https://entlibcontrib.codeplex.com/wikipage?title=VABContributions41&amp;referringTitle=Home&amp;ANCHOR#ArgumentValidation "VABContributions41&amp;referringTitle=Home&amp;ANCHOR#ArgumentValidation")**: Validate pre/post conditions of method calls.

### Other separate releases hosted on this site are:

*   [Enterprise Library Contrib v3.1 May 2009](https://entlibcontrib.codeplex.com/releases/view/26680): Enterprise Library Contributions for the Enterprise Library v3.1 core.

*   [Standalone Validation Application Block 1.2](https://entlibcontrib.codeplex.com/releases/view/7637): adds deep WPF automatic object validating data-binding via a new ValidationBinding markup extension.

*   [Extended SQL Data Access Block 3.1.1](https://entlibcontrib.codeplex.com/releases/view/6197): Now integrated into the main EntLibContrib release.

### Other contributions in the source code but not in the latest release:

*   The Application Block Software Factory (needs GAX)*   The Strong-Naming Guidance Package (needs GAX)

### Contributing to EntLib Contrib

Would you like to join as a developer of the EntLib Contrib project to share your own extensions or improve the existing codebase? Great! Here is what you need to know

*   [About p&amp;p "Contrib" projects](https://entlibcontrib.codeplex.com/wikipage?title=About%20p%26p%20%22Contrib%22%20projects&amp;referringTitle=Home "About%20p%26p%20%22Contrib%22%20projects&amp;referringTitle=Home")

*   [Guidelines for EntLib Contrib code](https://entlibcontrib.codeplex.com/wikipage?title=Guidelines%20for%20EntLib%20Contrib%20code&amp;referringTitle=Home "Guidelines%20for%20EntLib%20Contrib%20code&amp;referringTitle=Home")

*   [Sign Up Process](https://entlibcontrib.codeplex.com/wikipage?title=Sign%20Up%20Process&amp;referringTitle=Home "Sign%20Up%20Process&amp;referringTitle=Home")

### Other p&amp;p Contrib Projects

*   [Web Client Software Factory Contrib](http://wcsfcontrib.codeplex.com)

*   [Smart Client Software Factory Contrib](http://scsfcontrib.codeplex.com)

*   [Unity Contrib](http://unitycontributions.codeplex.com)

## Documentation
Enterprise Library User Contributions Documentation

*   [Installing the Enterprise Library Contributions v6](https://entlibcontrib.codeplex.com/wikipage?title=InstallingEntLibContrib6&referringTitle=Documentation)
*   [Installing the Enterprise Library Contributions v5 (and older)](https://entlibcontrib.codeplex.com/wikipage?title=InstallingEntLibContrib&referringTitle=Documentation)

**Developers** - please add new pages describing any extensions you submit to the code tree making sure to link your documentation to the appropriate version of the Enterprise Library.
*   [Enterprise Library Contributions v6.0](https://entlibcontrib.codeplex.com/wikipage?title=EntLibContrib6Doc&referringTitle=Documentation) - Documentation for extensions written for the Enterprise Library v6.0 (Source Code branch: **EntLibContrib6Src**)
*   [Enterprise Library Contributions v4.1](https://entlibcontrib.codeplex.com/wikipage?title=EntLibContrib41Doc&referringTitle=Documentation) - Documentation for extensions written for the Enterprise Library v4.1 (Source Code branch: **EntLibContrib41Src**)
*   [Enterprise Library Contributions v3.1](https://entlibcontrib.codeplex.com/wikipage?title=EntLibContrib31Doc&referringTitle=Documentation) - Documentation for extensions written for the Enterprise Library v3.1 (Source Code branch: **EnterpriseLibraryContrib**)
*   [Other Independant Contributions](https://entlibcontrib.codeplex.com/wikipage?title=EntLibContribOtherDoc&referringTitle=Documentation) - Documentation for extentions written for the Enterprise Library (any version) (Source Code branch: **Separate for each contribution**)

## License
[Microsoft Permissive License (Ms-PL) v1.1](https://entlibcontrib.codeplex.com/license)

## Author
[jbourgault](https://www.codeplex.com/site/users/view/jbourgault)
[Allen](http://vallen.cnblogs.com)
