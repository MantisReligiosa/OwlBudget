using System;
using System.Collections.Generic;
using System.ComponentModel;
using Core.Models;

namespace Core.Tools;

public static class EnumExtension
{
    public static List<EnumItem<TEnum>> GetEnumItems<TEnum>()
        where TEnum : Enum
    {
        var enumType = typeof(TEnum);
        var result = new List<EnumItem<TEnum>>();
        foreach (var enumValue in Enum.GetValues(typeof(TEnum)))
        {
            var enumValueMemberInfo = enumType.GetMember(enumValue.ToString()!)[0];
            var valueAttributes = enumValueMemberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            var description = ((DescriptionAttribute) valueAttributes[0]).Description;

            var enumItem = new EnumItem<TEnum>
                {Value = (TEnum) enumValue, Description = description};

            result.Add(enumItem);
        }

        return result;
    }
}