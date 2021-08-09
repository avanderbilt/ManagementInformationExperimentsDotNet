using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace ManagementInformation
{
    public static class ManagementInformationUtility
    {
        public static IEnumerable<string> ManagementInformationExtractor(string className, ExtractorHandler extractor)
        {
            if (className.IsNullOrWhiteSpace())
                throw new ArgumentNullException(nameof(className));
            if (extractor == null)
                throw new ArgumentNullException(nameof(extractor));

            var managementObject = FindManagementBaseObject(className);
            var instances = GetManagementObjectInstances(managementObject.ClassPath);
            var result = instances.Select(instance => extractor(instance)).ToList();
            result.Sort();
            return result;
        }

        private static IEnumerable<ManagementBaseObject> GetManagementObjectInstances(ManagementPath classPath)
        {
            var managementObjectCollection = new ManagementClass(classPath).GetInstances();
            var managementObjects = new ManagementBaseObject[managementObjectCollection.Count];
            managementObjectCollection.CopyTo(managementObjects, 0);
            return new List<ManagementBaseObject>(managementObjects);
        }

        private static ManagementBaseObject FindManagementBaseObject(string className)
        {
            var queryString = $"SELECT * FROM meta_class WHERE __class like '{className.ToLower()}'";
            var managementBaseObjectCollection = new ManagementObjectSearcher(queryString).Get();
            var managementBaseObjects = new ManagementBaseObject[managementBaseObjectCollection.Count];

            managementBaseObjectCollection.CopyTo(managementBaseObjects, 0);
            
            Console.Out.WriteLine($"Retrieved {managementBaseObjects.Length} base objects matching \"{className}\".");

            return managementBaseObjects.Length != 1 ? null : managementBaseObjects[0];
        }
    }
}