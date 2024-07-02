using System;
using System.Collections.Generic;

namespace EHTool.DBKit {

    public interface IDictionaryable<T> {
        IDictionary<string, object> ToDictionary();
        static T FromDictionary(IDictionary<string, object> d) => throw new NotImplementedException();

    }

    public interface IDatabaseConnector<T> where T : IDictionaryable<T> {

        public void Connect(string databaseName);
        public bool IsDatabaseExist();
        public void AddRecord(T Record);
        public void GetRecordAt(CallbackMethod<T> callback, CallbackMethod fallback, int idx);
        public void UpdateRecordAt(T Record, int idx);
        public void GetAllRecord(CallbackMethod<IList<T>> callback);

    }
}