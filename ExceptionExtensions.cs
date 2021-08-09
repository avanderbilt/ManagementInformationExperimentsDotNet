using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace ManagementInformation
{
    [SuppressMessage("ReSharper", "ArrangeObjectCreationWhenTypeEvident")]
    public static class ExceptionExtensions
    {
        private const string DefaultErrorMessage = @"""";
        private const string ErrorMessagePrefix = @"An error has occurred in the Management Information Experiments application";

        public static ManagementInformationExperimentException Wrap(this Exception e)
        {
            return new ManagementInformationExperimentException(e.FormatMessage(), e);
        }

        private static string FormatMessage(this Exception e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));

            return e.Message.IsNullOrWhiteSpace() ? $"{ErrorMessagePrefix}." : $"{ErrorMessagePrefix}: {e.Message}";
        }

        [SuppressMessage("ReSharper", "TailRecursiveCall")]
        private static void PrintMessage(this Exception e, bool isInnerException, TextWriter stream = null)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));
            
            var message = FormatMessageForOutput(e, isInnerException);

            stream?.WriteLine(message);

            e.InnerException?.PrintMessage(true, stream);
        }

        public static void PrintMessage(this Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            
            e.PrintMessage(false, Console.Error);
            
            Console.Clear();
        }
        private static string FormatMessageForOutput(this Exception e, bool isInnerException = false)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));

            var itIsAn = isInnerException ? "inner exception" : "exception"; 
            var exceptionTypeName = e.GetType().Name;
            return $"An {itIsAn} of type {exceptionTypeName} was thrown: {e.MessageOrDefault()}";
        }

        private static string MessageOrDefault(this Exception e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));
            
            return e.Message.IsNullOrWhiteSpace() ? DefaultErrorMessage : e.Message;
        }
    }
}