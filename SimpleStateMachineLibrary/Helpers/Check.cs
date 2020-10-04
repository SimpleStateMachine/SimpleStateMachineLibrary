using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleStateMachineLibrary.Helpers
{
    internal class Check
    {
        public static Tname Name<Tname>(Tname name, ILogger logger)
        {
            if (object.ReferenceEquals(name, default(Tname)))
            {
                string message = "Name must be not Empty";
                var ex = new ArgumentNullException(message: message, paramName:"Name");
                logger?.LogError(ex, message);
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
                logger?.LogError(ex, message, args);
                throw ex;
            }
                

            return objectRequested;
        }

        public static TObject NamedObject<TName, TKeyState, TKeyTransition, TKeyData, TObject>(TObject objectRequested, ILogger logger)
            where TObject : NamedObject<TName, TKeyState, TKeyTransition, TKeyData>
        {
            Check.Object(objectRequested, logger);
            Check.Name(objectRequested.Name, logger);
            return objectRequested;
        }

        
        public static bool Contains<TName, TKeyState, TKeyTransition, TKeyData, TObject>(Dictionary<TName, TObject> dictionary, TName nameObject, ILogger logger, bool exception = true)
            where TObject : NamedObject<TName, TKeyState, TKeyTransition, TKeyData>
        {
            nameObject = Contains<TName, TKeyState, TKeyTransition, TKeyData, TObject>(dictionary, nameObject, logger, out bool result, exception);
            return result;
        }
        public static bool Contains<TName, TKeyState, TKeyTransition, TKeyData, TObject>(Dictionary<TName, TObject> dictionary, TObject objectRequested, ILogger logger, bool exception = true) 
            where TObject : NamedObject<TName, TKeyState, TKeyTransition, TKeyData>
        {
            objectRequested = Contains<TName, TKeyState, TKeyTransition, TKeyData, TObject>(dictionary, objectRequested, logger, out bool result, exception);
            return result;
        }
        public static TName Contains<TName, TKeyState, TKeyTransition, TKeyData, TObject>(Dictionary<TName, TObject> dictionary, TName nameObject, ILogger logger, out bool result, bool exception = true) 
            where TObject : NamedObject<TName, TKeyState, TKeyTransition, TKeyData>
        {
            dictionary = Check.Object(dictionary, logger);
            nameObject = Check.Name(nameObject, logger);
            result = dictionary.ContainsKey(nameObject);

            if ((exception) && (!result))
            {
                object[] args = { nameObject };
                string message = "Element with name \"{0}\" is not found";
                var ex = new KeyNotFoundException(message: String.Format(message, args));
                logger?.LogError(ex, message, args);
                throw ex;
            }

            return nameObject;
        }
        public static TObject Contains<TName, TKeyState, TKeyTransition, TKeyData, TObject>(Dictionary<TName, TObject> dictionary, TObject objectRequested, ILogger logger, out bool result, bool exception = true) 
            where TObject : NamedObject<TName, TKeyState, TKeyTransition, TKeyData>
        {
            dictionary = Check.Object(dictionary, logger);
            objectRequested = Check.Object(objectRequested, logger);

            result = dictionary.ContainsValue(objectRequested);

            if ((exception) && (!result))
            {
                object[] args = { objectRequested.Name };
                string message = "Element with name \"{0}\" is not found";
                var ex = new KeyNotFoundException(message: String.Format(message, args));
                logger?.LogError(ex, message, args);
                throw ex;
            }

            return objectRequested;
        }


        public static bool NotContains<TName, TKeyState, TKeyTransition, TKeyData, TObject>(Dictionary<TName, TObject> dictionary, TName nameObject, ILogger logger, bool exception = true)
            where TObject : NamedObject<TName, TKeyState, TKeyTransition, TKeyData>
        {
            nameObject = NotContains<TName, TKeyState, TKeyTransition, TKeyData, TObject>(dictionary, nameObject, logger, out bool result, exception);
            return result;
        }
        public static bool NotContains<TName, TKeyState, TKeyTransition, TKeyData, TObject>(Dictionary<TName, TObject> dictionary, TObject objectRequested, ILogger logger, bool exception = true) 
            where TObject : NamedObject<TName, TKeyState, TKeyTransition, TKeyData>
        {
            objectRequested = NotContains<TName, TKeyState, TKeyTransition, TKeyData, TObject>(dictionary, objectRequested, logger, out bool result, exception);
            return result;
        }
        public static TName NotContains<TName, TKeyState, TKeyTransition, TKeyData, TObject>(Dictionary<TName, TObject> dictionary, TName nameObject, ILogger logger, out bool result, bool exception = true)
            where TObject : NamedObject<TName, TKeyState, TKeyTransition, TKeyData>
        {
            dictionary = Check.Object(dictionary, logger);
            nameObject = Check.Name(nameObject, logger);
            result = !dictionary.ContainsKey(nameObject);

            if ((exception) && (!result))
            {
                object[] args = { nameObject };
                string message = "Element with name \"{0}\" already exists";
                var ex = new ArgumentException(message: String.Format(message, args));
                logger?.LogError(ex, message, args);
                throw ex;
            }

            return nameObject;
        }
        public static TObject NotContains<TName, TKeyState, TKeyTransition, TKeyData, TObject>(Dictionary<TName, TObject> dictionary, TObject objectRequested, ILogger logger, out bool result, bool exception = true) 
            where TObject : NamedObject<TName, TKeyState, TKeyTransition, TKeyData>
        {
            dictionary = Check.Object(dictionary, logger);
            objectRequested = Check.Object(objectRequested, logger);
            result = !dictionary.ContainsValue(objectRequested);

            if ((exception) && (!result))
            {
                object[] args = { objectRequested.Name };
                string message = "Element with name \"{0}\" already exists";
                var ex = new ArgumentException(message: String.Format(message, args));
                logger?.LogError(ex, message, args);
                throw ex;
            }

            return objectRequested;
        }
      
        
        public static TObject Remove<TName, TKeyState, TKeyTransition, TKeyData, TObject>(Dictionary<TName, TObject> dictionary, TName nameObject, ILogger logger, out bool result, bool exception = true) where TObject : NamedObject<TName, TKeyState, TKeyTransition, TKeyData>
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
                     logger?.LogError(ex, message, args);
                    throw ex;
                }
                  
                else
                    return default(TObject);
            }

            dictionary.Remove(nameObject);
            result = true;
            return removedObj;
        }
        public static TObject Remove<TName, TKeyState, TKeyTransition, TKeyData, TObject>(Dictionary<TName, TObject> dictionary, TObject obj, ILogger logger, out bool result, bool exception = true) where TObject : NamedObject<TName, TKeyState, TKeyTransition, TKeyData>
        {
            result = false;
            dictionary = Check.Object(dictionary, logger);
            obj = Check.NamedObject<TName, TKeyState, TKeyTransition, TKeyData, TObject>(obj, logger);

            TObject removedObj = default(TObject);
            dictionary?.TryGetValue(obj.Name, out removedObj);

            if (removedObj == default(TObject))
            {
                if (exception)
                {
                    object[] args = { obj.Name };
                    string message = "Element with name \"{0}\" is not deleted because not found";
                    var ex = new KeyNotFoundException(String.Format(message, args));
                     logger?.LogError(ex, message, args);
                    throw ex;
                }
                
                else
                    return default(TObject);
            }

            dictionary.Remove(obj.Name);
            result = true;
            return removedObj;
        }


        public static TObject GetElement<TName, TKeyState, TKeyTransition, TKeyData, TObject>(Dictionary<TName, TObject> dictionary, TName nameObject, ILogger logger, out bool result, bool exception = true) where TObject : NamedObject<TName, TKeyState, TKeyTransition, TKeyData>
        {
            result = Contains<TName, TKeyState, TKeyTransition, TKeyData, TObject>(dictionary, nameObject, logger, exception);
            return result ? dictionary[nameObject] : default(TObject);
        }
        public static TObject GetElement<TName, TKeyState, TKeyTransition, TKeyData, TObject>(Dictionary<TName, TObject> dictionary, TObject obj, ILogger logger, out bool result, bool exception = true) where TObject : NamedObject<TName, TKeyState, TKeyTransition, TKeyData>
        {
            result = Contains<TName, TKeyState, TKeyTransition, TKeyData, TObject>(dictionary, obj, logger, exception);
            return result ? obj : default(TObject);
        }


        public static Dictionary<TName, TObject> GetValuesWhere<TName, TKeyState, TKeyTransition, TKeyData, TObject>(Dictionary<TName, TObject> dictionary, Func<TObject, bool> action, ILogger logger, out bool result, bool exception = true) where TObject : NamedObject<TName, TKeyState, TKeyTransition, TKeyData>
        {
            dictionary = Check.Object(dictionary, logger);
            Dictionary<TName, TObject> foundElements = dictionary.Values.Where(action).ToDictionary(x => x.Name, x => x);
            result = foundElements.Count > 1;
            if ((exception) && (!result))
            {
                object[] args = { };
                string message = "Elements aren't found";
                var ex = new KeyNotFoundException(message: String.Format(message, args));
                logger?.LogError(ex, message, args);
                throw ex;
            }

            return foundElements;
        }

    }
}
