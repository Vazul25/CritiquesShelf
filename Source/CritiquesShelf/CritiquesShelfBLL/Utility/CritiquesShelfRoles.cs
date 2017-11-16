using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace CritiquesShelfBLL.Utility
{
    public enum CritiquesShelfRoles
    {
        [DisplayName("User")]
        User,
        [DisplayName("Admin")]
        Admin
    }
    public static class CritiquesShelfRolesExtension
    {
        public static string GetName(this CritiquesShelfRoles role)
        {

            var type = role.GetType();
            var members = type.GetMembers();
            MemberInfo[] memberInfos = type.GetMember(role.ToString());
            foreach (var memberInfo in memberInfos)
            {
                List<DisplayNameAttribute> displayNameAtributes = new List<DisplayNameAttribute>(memberInfo.GetCustomAttributes<DisplayNameAttribute>());
                if (displayNameAtributes != null && displayNameAtributes.Count > 0) return displayNameAtributes[0].DisplayName;
                
            }

            return role.ToString();
        }


    }
}
