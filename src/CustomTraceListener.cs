using System;
using System.Collections.Generic;
using System.Text;

namespace Dacke.TraceListeners
{
    /// <summary>
    /// Provides the default output methods and behavior for tracing.
    /// </summary>
    public class DefaultTraceListener : CustomTraceListenerBase, Dacke.TraceListeners.ICustomTraceListener
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
    }
}
