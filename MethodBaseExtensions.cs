using System.Reflection;

namespace ManagementInformation
{
    public static class MethodBaseExtensions
    {
        public static string DeclaringTypeName(this MethodBase method)
        {
            if (method == null||method.DeclaringType == null||method.DeclaringType.Name.IsNullOrWhiteSpace())
                return null;

            return method.DeclaringType.Name;
        }
    }
}