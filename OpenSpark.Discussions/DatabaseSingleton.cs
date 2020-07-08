﻿using System;
using System.Configuration;
using Raven.Client.Documents;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace OpenSpark.Discussions
{
    public class DatabaseSingleton
    {
        private static readonly Lazy<IDocumentStore> _store = new Lazy<IDocumentStore>(CreateDocumentStore);

        public static IDocumentStore Store => _store.Value;

        private static IDocumentStore CreateDocumentStore()
        {
            // var url = ConfigurationManager.AppSettings["RavenDb:Discussions:Url"];
            // var databaseName = ConfigurationManager.AppSettings["RavenDb:Discussions:Name"];

            const string databaseName = "OpenSpark.Discussions";
            const string url = "http://127.0.0.1:8080/";

            var store = new DocumentStore
            {
                Urls = new[] { url },
                Conventions =
                {
                    MaxNumberOfRequestsPerSession = 1024,
                    UseOptimisticConcurrency = true
                },
                Database = databaseName,
                // Define a client certificate (optional)
                // Certificate = new X509Certificate2("C:\\path_to_your_pfx_file\\cert.pfx"),
            };

            try
            {
                // Initialize the Document Store
                store.Initialize();

                try
                {
                    // Attempt to create the database if it does not already exist
                    store.Maintenance.Server.Send(new CreateDatabaseOperation(new DatabaseRecord(store.Database)));
                }
                catch (Exception)
                {
                    // database exists
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to initialize RavenDB: {ex}");
            }

            return store;
        }
    }
}
