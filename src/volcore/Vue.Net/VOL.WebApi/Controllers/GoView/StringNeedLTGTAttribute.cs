using System;

namespace VOL.WebApi.Controllers.GoView
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class StringNeedLTGTAttribute : Attribute
    {
    }
}
