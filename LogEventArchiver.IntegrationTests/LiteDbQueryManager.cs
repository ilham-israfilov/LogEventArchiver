using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LogEventArchiver.IntegrationTests
{
    internal static class LiteDbQueryManager
    {
        internal static IEnumerable<T> GetDocuments<T>(string dbFilePath, string collectionName, Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> result = null;

            using (var db = new LiteDatabase(dbFilePath))
            {
                var collection = db.GetCollection<T>(collectionName);
                result = collection.Find(predicate);
            }

            return result;
        }

        internal static void ClearCollection<T>(string dbFilePath, string collectionName, Expression<Func<T, bool>> predicate)
        {
            using (var db = new LiteDatabase(dbFilePath))
            {
                var collection = db.GetCollection<T>(collectionName);
                collection.Delete(predicate);
            }
        }
    }
}