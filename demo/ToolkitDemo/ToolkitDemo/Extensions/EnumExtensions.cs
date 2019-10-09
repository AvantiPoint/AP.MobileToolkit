using System;
using System.ComponentModel;
using System.Reflection;

namespace ToolkitDemo.Extensions
{
    public static class EnumExtensions
    {
        public static string Description(this Enum objEnum)
        {
            Type type = objEnum.GetType();
            MemberInfo[] memInfo = type.GetMember(objEnum.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return objEnum.ToString();
        }
    }
}
