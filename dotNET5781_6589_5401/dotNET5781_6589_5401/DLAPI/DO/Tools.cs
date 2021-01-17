﻿using System.Reflection;

namespace DO
{
    public static class Tools
    {
        /// <summary>
        /// extension method for
        /// RTTI for ToString
        /// </summary>
        /// <typeparam name="T">generic type</typeparam>
        /// <param name="t">"this" type</param>
        /// <returns></returns>
        public static string ToStringProperty<T>(this T t)
        {
            string str = "";
            foreach (PropertyInfo item in t.GetType().GetProperties())
                str += "\n" + item.Name + ": " + item.GetValue(t, null);
            return str;
        }
    }
}