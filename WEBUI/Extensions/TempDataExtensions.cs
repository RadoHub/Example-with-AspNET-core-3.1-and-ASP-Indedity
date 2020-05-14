using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prj.WebUI.Extensions
{
    public static class TempDataExtensions
    {
 
        public static void Put<T> (this ITempDataDictionary tempData, string key, T value ) where T: class            
        {
            //if (tempData is T)
            //{
                tempData[key] = JsonConvert.SerializeObject(value);
           // }            
               // throw new InvalidCastException(string.Format("TempData does not contain type {0} for the key {1}", typeof(T), key));
        }

        public static T Get<T> (this ITempDataDictionary tempData, string key) where T: class
        {
            object o;
            tempData.TryGetValue(key, out o);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string) o);
        }
    }
}
