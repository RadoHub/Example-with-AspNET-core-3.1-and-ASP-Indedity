using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prj.WebUI.Extensions
{
    public static class ViewDataExtensions
    {
        public static void Put<T> (this ViewDataDictionary viewData, string key, T value ) where T: class

        {
            if (viewData is T)
            {
                viewData[key] = JsonConvert.SerializeObject(value);
            }
            throw new InvalidCastException(string.Format("ViewData does not contain type of {0} for the key {1}", typeof(T), key));
        }
        public static T Get<T> (this ViewDataDictionary viewData, string key ) where T:class
        {
            object o;
            viewData.TryGetValue(key, out o);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }
    }
}
