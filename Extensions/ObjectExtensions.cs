﻿namespace Huawei.DeveloperApi
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.Json.Serialization;
    using Olive;

    static class ObjectExtensions
    {
        public static IDictionary<string, string> ToDictionary(this object @this)
        {
            return @this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                  .ToDictionary(x => x.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? x.Name.ToSnakeCase(), x => x.GetValue(@this)?.ToString());
        }
    }
}
