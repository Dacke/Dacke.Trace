using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;


namespace Dacke.TraceListeners
{
    /// <summary>
    /// Provides the abstract base class for the listeners who monitor trace and debug output.
    /// </summary>
    public class CustomTraceListenerBase : System.Diagnostics.DefaultTraceListener
    {
        #region Protected Methods

        /// <summary>
        /// Gets the information from the stack trace and builds the Preamble message to be 
        /// attached to any Trace statements that execute through the Write or WriteLine 
        /// overrides.
        /// </summary>
        /// <param name="message">A message to write.</param>
        /// <returns>The message to write with the pre-pended preamble message.</returns>
        protected virtual string PrependPreambleMessage(string message)
        {
            //	Initialize the variable
            var preambleMessage = String.Empty;
            
            try
            {
                //	Declare variables
                StringBuilder preamble = new StringBuilder(Process.GetCurrentProcess().ProcessName + " - ");
                StackTrace stackTrace = new StackTrace();
                StackFrame stackFrame;
                ParameterInfo[] parameters;
                MethodBase stackFrameMethod;
                int nframeCount = 0;
                int parameterIndex = 0;
                string typeName;


                do
                {
                    nframeCount++;
                    stackFrame = stackTrace.GetFrame(nframeCount);
                    stackFrameMethod = stackFrame.GetMethod();
                    typeName = stackFrameMethod.ReflectedType.FullName;
                } while (typeName.StartsWith("System") || typeName.EndsWith("Dacke.Trace"));

                //	Log DateTime, Namespace, Class and Method Name
                preamble.Append(DateTime.Now.ToUniversalTime().ToString());
                preamble.Append(": ");
                preamble.Append(typeName);
                preamble.Append(".");
                preamble.Append(stackFrameMethod.Name);
                preamble.Append("( ");

                // log parameter types and names
                parameters = stackFrameMethod.GetParameters();

                while (parameterIndex < parameters.Length)
                {
                    preamble.Append(parameters[parameterIndex].ParameterType.Name);
                    preamble.Append(" ");
                    preamble.Append(parameters[parameterIndex].Name);
                    parameterIndex++;

                    if (parameterIndex != parameters.Length)
                    {
                        preamble.Append(", ");
                    }
                }

                preamble.Append(" ): ");

                preambleMessage = preamble.ToString();
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                    Debugger.Break();                
                throw ex;
            }

            return (preambleMessage + message);
        }

        /// <summary>
        /// Returns the name of the calling method.
        /// </summary>
        /// <returns>The method name.</returns>
        protected virtual string GetCallingMethodName()
        {
            var callingMethod = MethodBase.GetCurrentMethod().Name;

            var stackFrames = new StackTrace().GetFrames();
            foreach (var stackFrame in stackFrames)
            {
                var currentMethodFrame = stackFrame.GetMethod();
                if (currentMethodFrame.Module.Assembly.GetName().Name != Assembly.GetExecutingAssembly().GetName().Name)
                {
                    callingMethod = currentMethodFrame.Name;
                    break;
                }
                
            }

            return callingMethod;
        }

        #endregion
    }
}
