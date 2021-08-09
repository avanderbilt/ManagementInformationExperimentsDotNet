using System;
using System.Runtime.Serialization;

namespace ManagementInformation
{
    [Serializable]
    public class ManagementInformationExperimentException : Exception
    {
        public ManagementInformationExperimentException()
        {
        }

        public ManagementInformationExperimentException(string message) : base(message)
        {
        }

        public ManagementInformationExperimentException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ManagementInformationExperimentException(SerializationInfo info, StreamingContext context) : base(
            info, context)
        {
        }
    }
}