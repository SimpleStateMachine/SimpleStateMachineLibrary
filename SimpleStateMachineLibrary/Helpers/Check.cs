using System;
using System.Collections.Generic;

namespace SimpleStateMachineLibrary.Helpers
{
    public class Check
    {
        public static string Name(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("Name must be not Empty");
            return name;
        }

        public static TObject Object<TObject>(TObject objectRequested)
        {
            if (Equals(objectRequested, default(TObject)))
                throw new ArgumentNullException(String.Format("Object of type {0} must be not null", typeof(TObject).ToString()));
            return objectRequested;
        }

        public static TObject NamedObject<TObject>(TObject objectRequested) where TObject : NamedObject
        {
            Check.Object(objectRequested);
            Check.Name(objectRequested.Name);
            return objectRequested;
        }

        private static bool _Contains<TObject>(Dictionary<string, TObject> dictionary, string nameObject, bool needContains, bool exeption) where TObject : NamedObject
        {
            dictionary = Check.Object(dictionary);
            nameObject = Check.Name(nameObject);

            if (needContains == dictionary.ContainsKey(nameObject))
                return true;
            if (exeption)
                if (needContains)
                    throw new KeyNotFoundException(String.Format("Element with name {0} is not found", nameObject));
                else
                    throw new ArgumentException(String.Format("Element with name {0} already exists", nameObject));
            return false;
        }

        private static bool _Contains<TObject>(Dictionary<string, TObject> dictionary, TObject objectRequested, bool needContains, bool exeption) where TObject : NamedObject
        {
            dictionary = Check.Object(dictionary);
            objectRequested = Check.Object(objectRequested);

            if (needContains == dictionary.ContainsValue(objectRequested))
                return true;

            if (exeption)
                if (needContains)
                    throw new KeyNotFoundException(String.Format("Element of type {0} not found", typeof(TObject).ToString()));
                else
                    throw new ArgumentException(String.Format("Element of type {0} already exists", typeof(TObject).ToString()));
            return false;
        }


        public static bool Contains<TObject>(Dictionary<string, TObject> dictionary, string nameObject, bool exeption = true) where TObject : NamedObject
        {
            return _Contains(dictionary, nameObject, true, exeption);
        }

        public static bool Contains<TObject>(Dictionary<string, TObject> dictionary, TObject objectRequested, bool exeption = true) where TObject : NamedObject
        {
            return _Contains(dictionary, objectRequested, true, exeption);
        }


        public static bool NotContains<TObject>(Dictionary<string, TObject> dictionary, string nameObject, bool exeption = true) where TObject : NamedObject
        {
            return _Contains(dictionary, nameObject, false, exeption);
        }

        public static bool NotContains<TObject>(Dictionary<string, TObject> dictionary, TObject objectRequested, bool exeption = true) where TObject : NamedObject
        {
            return _Contains(dictionary, objectRequested, false, exeption);
        }


        public static TObject Remove<TObject>(Dictionary<string, TObject> dictionary, string nameObject, bool exeption = true) where TObject : NamedObject
        {
            dictionary = Check.Object(dictionary);
            nameObject = Check.Name(nameObject);

            TObject removedObj = default(TObject);
            dictionary?.TryGetValue(nameObject, out removedObj);

            if (removedObj == default(TObject))
            {
                if (exeption)
                    throw new KeyNotFoundException(String.Format("Element with name {0} is not deleted because not found. ", nameObject));
                else
                    return default(TObject);
            }

            dictionary.Remove(nameObject);
            return removedObj;
        }

        public static TObject Remove<TObject>(Dictionary<string, TObject> dictionary, TObject obj, bool exeption = true) where TObject : NamedObject
        {
            dictionary = Check.Object(dictionary);
            obj = Check.NamedObject(obj);

            TObject removedObj = default(TObject);
            dictionary?.TryGetValue(obj.Name, out removedObj);

            if (removedObj == default(TObject))
            {
                if (exeption)
                    throw new KeyNotFoundException(String.Format("Element with name {0} is not deleted because not found. ", obj.Name));
                else
                    return default(TObject);
            }

            dictionary.Remove(obj.Name);
            return removedObj;
        }


        public static TObject GetElement<TObject>(Dictionary<string, TObject> dictionary, string nameObject, bool exeption = true) where TObject : NamedObject
        {
            bool contains = Contains(dictionary, nameObject, exeption);
            return contains ? dictionary[nameObject] : default(TObject);
        }

        public static TObject GetElement<TObject>(Dictionary<string, TObject> dictionary, TObject obj, bool exeption = true) where TObject : NamedObject
        {
            bool contains = Contains(dictionary, obj, exeption);
            return contains ? obj : default(TObject);
        }


        public static TObject AddElement<TObject>(Dictionary<string, TObject> dictionary, TObject obj, bool exeption = true) where TObject : NamedObject
        {
            return AddElement(dictionary, obj?.Name, obj, exeption);
        }

        public static TObject AddElement<TObject>(Dictionary<string, TObject> dictionary, string name, TObject obj, bool exeption = true) where TObject : NamedObject
        {
            obj = Check.NamedObject(obj);
            bool nonContains = NotContains(dictionary, name, exeption);

            if (nonContains)
                return default(TObject);

            dictionary.Add(name, obj);
            return obj;
        }
    }
}
