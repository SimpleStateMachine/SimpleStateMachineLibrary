using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleStateMachineLibrary.Helpers
{
    internal class Check
    {
         //= null
        public static string Name(string name, ILogger logger)
        {
            if (String.IsNullOrEmpty(name))
            {
                string message = "Name must be not Empty";
                var ex = new ArgumentNullException(message: message, paramName:"Name");
                logger.LogError(ex, message);
                throw ex;
            }
               
            return name;
        }

        public static TObject Object<TObject>(TObject objectRequested, ILogger logger)
        {
            if (Equals(objectRequested, default(TObject)))
            {
                object[] args = { typeof(TObject).Name };
                string message = "Object of type \"{0}\" must be not null";
                var ex = new ArgumentNullException(message: String.Format(message, args), paramName: typeof(TObject).Name);
                logger.LogError(ex, message, args);
                throw ex;
            }
                

            return objectRequested;
        }

        public static TObject NamedObject<TObject>(TObject objectRequested, ILogger logger) where TObject : NamedObject
        {
            Check.Object(objectRequested, logger);
            Check.Name(objectRequested.Name, logger);
            return objectRequested;
        }

       
        public static bool Contains<TObject>(Dictionary<string, TObject> dictionary, string nameObject, ILogger logger, bool exception = true) where TObject : NamedObject
        {
            dictionary = Check.Object(dictionary, logger);
            nameObject = Check.Name(nameObject, logger);
            bool contains = dictionary.ContainsKey(nameObject);

            if ((exception)&&(!contains))
            {
                object[] args = { nameObject };
                string message = "Element with name \"{0}\" is not found";
                var ex = new KeyNotFoundException(message: String.Format(message, args));
                logger.LogError(ex, message, args);
                throw ex;
            }

            return contains;
        }

        public static bool Contains<TObject>(Dictionary<string, TObject> dictionary, TObject objectRequested, ILogger logger, bool exception = true) where TObject : NamedObject
        {
            dictionary = Check.Object(dictionary, logger);
            objectRequested = Check.Object(objectRequested, logger);

            bool contains = dictionary.ContainsValue(objectRequested);

            if ((exception) && (!contains))
            {
                object[] args = { objectRequested.Name };
                string message = "Element with name \"{0}\" is not found";
                var ex = new KeyNotFoundException(message: String.Format(message, args));
                logger.LogError(ex, message, args);
                throw ex;
            }

            return contains;
        }

        public static bool NotContains<TObject>(Dictionary<string, TObject> dictionary, string nameObject, ILogger logger, bool exception = true) where TObject : NamedObject
        {
            dictionary = Check.Object(dictionary, logger);
            nameObject = Check.Name(nameObject, logger);
            bool notContains = !dictionary.ContainsKey(nameObject);

            if ((exception) && (!notContains))
            {
                object[] args = { nameObject };
                string message = "Element of type \"{0}\" already exists";
                var ex = new KeyNotFoundException(message: String.Format(message, args));
                logger.LogError(ex, message, args);
                throw ex;
            }

            return notContains;
        }

        public static bool NotContains<TObject>(Dictionary<string, TObject> dictionary, TObject objectRequested, ILogger logger, bool exception = true) where TObject : NamedObject
        {
            dictionary = Check.Object(dictionary, logger);
            objectRequested = Check.Object(objectRequested, logger);

            bool notContains = !dictionary.ContainsValue(objectRequested);

            if ((exception) && (!notContains))
            {
                object[] args = { objectRequested.Name };
                string message = "Element of type \"{0}\" already exists";
                var ex = new KeyNotFoundException(message: String.Format(message, args));
                logger.LogError(ex, message, args);
                throw ex;
            }

            return notContains;
        }
        
        public static TObject Remove<TObject>(Dictionary<string, TObject> dictionary, string nameObject, ILogger logger, out bool result, bool exception = true) where TObject : NamedObject
        {
            result = false;
            dictionary = Check.Object(dictionary, logger);
            nameObject = Check.Name(nameObject, logger);

            TObject removedObj = default(TObject);
            dictionary?.TryGetValue(nameObject, out removedObj);

            if (removedObj == default(TObject))
            {
                if (exception)
                {
                    object[] args = { nameObject };
                    string message = "Element with name \"{0}\" is not deleted because not found";
                    var ex = new KeyNotFoundException(String.Format(message, args));
                    logger.LogError(ex, message, args);
                    throw ex;
                }
                  
                else
                    return default(TObject);
            }

            dictionary.Remove(nameObject);
            result = true;
            return removedObj;
        }

        public static TObject Remove<TObject>(Dictionary<string, TObject> dictionary, TObject obj, ILogger logger, out bool result, bool exception = true) where TObject : NamedObject
        {
            result = false;
            dictionary = Check.Object(dictionary, logger);
            obj = Check.NamedObject(obj, logger);

            TObject removedObj = default(TObject);
            dictionary?.TryGetValue(obj.Name, out removedObj);

            if (removedObj == default(TObject))
            {
                if (exception)
                {
                    object[] args = { obj.Name };
                    string message = "Element with name \"{0}\" is not deleted because not found";
                    var ex = new KeyNotFoundException(String.Format(message, args));
                    logger.LogError(ex, message, args);
                    throw ex;
                }
                
                else
                    return default(TObject);
            }

            dictionary.Remove(obj.Name);
            result = true;
            return removedObj;
        }


        public static TObject GetElement<TObject>(Dictionary<string, TObject> dictionary, string nameObject, ILogger logger, out bool result, bool exception = true) where TObject : NamedObject
        {
            result = Contains(dictionary, nameObject, logger, exception);
            return result ? dictionary[nameObject] : default(TObject);
        }

        public static TObject GetElement<TObject>(Dictionary<string, TObject> dictionary, TObject obj, ILogger logger, out bool result, bool exception = true) where TObject : NamedObject
        {
            result = Contains(dictionary, obj, logger, exception);
            return result ? obj : default(TObject);
        }

    }
}
