using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// doesn't work with nested classes
    /// </summary>
    static class Cloning
    {
        static T Cloned<T>(this T original)
        {
            T result = (T)Activator.CreateInstance(typeof(T));
            foreach (PropertyInfo item in original.GetType().GetProperties())
            {
                item.SetValue(result, item.GetValue(original, null));
            }
            return result;
        }
    }
}
