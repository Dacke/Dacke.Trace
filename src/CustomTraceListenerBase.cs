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
        #region Write Overrides
        
        /// <summary>
        /// When overridden in a derived class, writes the specified message to the listener you create in the derived class.
        /// </summary>
        /// <param name="message">A message to write. </param>
        public override void Write(string message)
        {
            base.Write(PrependPreambleMessage(message));
        }

        /// <summary>
        /// When overridden in a derived class, writes the exception message to the listener you create in the derived class.
        /// </summary>
        /// <example>
        /// <code>
        ///   var ex = new NotImplimentedException();
        ///   Write(ex);
        /// </code>
        /// </example>
        /// <param name="ex">The exception object that you want to output.</param>
        public virtual void Write(Exception ex)
        {
            this.Write(
               PrependPreambleMessage(
                   String.Format("Source: {0}, Message: {1}", ex.Source, ex.Message)));
        }
        
        /// <summary>
        /// Writes the value of the object's ToString method to the listener you create when you implement the TraceListener class.
        /// </summary>
        /// <param name="o">An Object whose fully qualified class name you want to write. </param>
        public override void Write(object o)
        {
            base.Write(PrependPreambleMessage(o.GetType().FullName));
        }

        /// <summary>
        /// Writes a category name and a message to the trace listeners in the Listeners collection.
        /// </summary>
        /// <param name="message">A message to write.</param>
        /// <param name="category">A category name used to organize the output</param>
        public override void Write(string message, string category)
        {
            base.Write(PrependPreambleMessage(message), category);
        }

        /// <summary>
        /// Writes a category name and a message to the trace listeners in the Listeners collection.
        /// </summary>
        /// <param name="ex">The exception that you want to parse the output of.</param>
        /// <param name="category">A category name used to organize the output</param>
        public virtual void Write(Exception ex, string category)
        {
            this.Write(
               PrependPreambleMessage(
                   String.Format("Source: {0}, Message: {1}", ex.Source, ex.Message)), category);
        }

        /// <summary>
        /// Writes a category name and a message to the trace listeners in the Listeners collection.
        /// </summary>
        /// <param name="o">An object whose full name will be written to the output.</param>
        /// <param name="category">A category name used to organize the output</param>
        public override void Write(object o, string category)
        {
            base.Write(PrependPreambleMessage(o.GetType().FullName), category);
        }

        #endregion

        #region WriteLine Overrides

        /// <summary>
        /// Writes a message to the trace listeners in the Listeners collection.
        /// </summary>
        /// <param name="message">A message to write.</param>
        public override void WriteLine(string message)
        {
           base.WriteLine(PrependPreambleMessage(message));
        }

        /// <summary>
        /// Writes a message to the trace listeners in the Listeners collection.
        /// </summary>
        /// <param name="ex">The exception object whose message and source properties will be output.</param>
        public virtual void WriteLine(Exception ex)
        {
            this.WriteLine(
                PrependPreambleMessage(
                    String.Format("Source: {0}, Message: {1}", ex.Source, ex.Message)));
        }

        /// <summary>
        /// Writes a message to the trace listeners in the Listeners collection.
        /// </summary>
        /// <param name="o">An Object name is sent to the Listeners.</param>
        public override void WriteLine(object o)
        {
            base.WriteLine(PrependPreambleMessage(o.GetType().FullName));
        }

        /// <summary>
        /// Writes a category name and a message to the trace listeners in the Listeners collection.
        /// </summary>
        /// <param name="message">A message to write</param>
        /// <param name="category">A category name used to organize the output.</param>
        public override void WriteLine(string message, string category)
        {
            base.WriteLine(PrependPreambleMessage(message), category);
        }

        /// <summary>
        /// Writes a category name and a message to the trace listeners in the Listeners collection.
        /// </summary>
        /// <param name="ex">The exception object whose message and source properties will be output.</param>
        /// <param name="category">A category name used to organize the output.</param>
        public virtual void WriteLine(Exception ex, string category)
        {
            this.WriteLine(
                PrependPreambleMessage(
                    String.Format("Source: {0}, Message: {1}", ex.Source, ex.Message)), category);
        }

        /// <summary>
        /// Writes a category name and a message to the trace listeners in the Listeners collection.
        /// </summary>
        /// <param name="o">An Object name is sent to the Listeners.</param>
        /// <param name="category">A category name used to organize the output.</param>
        public override void WriteLine(object o, string category)
        {
            base.WriteLine(PrependPreambleMessage(o.GetType().FullName), category);
        }

        #endregion

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
