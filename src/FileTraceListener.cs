using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Configuration;

namespace Dacke.TraceListeners
{
    /// <summary>
    /// Directs tracing or debugging output to a FileStream.
    /// </summary>
    public class FileTraceListener : CustomTraceListenerBase, Dacke.TraceListeners.ICustomTraceListener
    {
        #region Properties

        public string TraceFile { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new instance of the TipsFileTrace object
        /// </summary>
        public FileTraceListener() : base()
        {
            var traceFileLocation = ConfigurationManager.AppSettings["TraceFileLocation"];            
            if (String.IsNullOrEmpty(traceFileLocation))
                this.TraceFile = Path.Combine(Environment.CurrentDirectory, "FileTraceListener.log");
            else
                this.TraceFile = traceFileLocation;
        }

        #region Write Overrides

        /// <summary>
        /// When overridden in a derived class, writes the specified message to the listener you create in the derived class.
        /// </summary>
        /// <param name="message">A message to write. </param>
        public override void Write(string message)
        {
            WriteOutputToFile(PrependPreambleMessage(message));
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
            this.Write(String.Format("Source: {0}, Message: {1}", ex.Source, ex.Message));
        }

        /// <summary>
        /// Writes the value of the object's ToString method to the listener you create when you implement the TraceListener class.
        /// </summary>
        /// <param name="o">An Object whose fully qualified class name you want to write. </param>
        public override void Write(object o)
        {
            this.Write(o.GetType().FullName);
        }

        /// <summary>
        /// Writes a category name and a message to the trace listeners in the Listeners collection.
        /// </summary>
        /// <param name="message">A message to write.</param>
        /// <param name="category">A category name used to organize the output</param>
        public override void Write(string message, string category)
        {
            WriteOutputToFile(PrependPreambleMessage(String.Format("Category: {0} - Message: {1}", message, category)));
        }

        /// <summary>
        /// Writes a category name and a message to the trace listeners in the Listeners collection.
        /// </summary>
        /// <param name="ex">The exception that you want to parse the output of.</param>
        /// <param name="category">A category name used to organize the output</param>
        public virtual void Write(Exception ex, string category)
        {
            this.Write(String.Format("Source: {0}, Message: {1}", ex.Source, ex.Message), category);
        }

        /// <summary>
        /// Writes a category name and a message to the trace listeners in the Listeners collection.
        /// </summary>
        /// <param name="o">An object whose full name will be written to the output.</param>
        /// <param name="category">A category name used to organize the output</param>
        public override void Write(object o, string category)
        {
            this.Write(o.GetType().FullName, category);
        }

        #endregion

        #region WriteLine Overrides

        /// <summary>
        /// Writes a message to the trace listeners in the Listeners collection.
        /// </summary>
        /// <param name="message">A message to write.</param>
        public override void WriteLine(string message)
        {
            WriteOutputToFile(PrependPreambleMessage(message));
        }

        /// <summary>
        /// Writes a message to the trace listeners in the Listeners collection.
        /// </summary>
        /// <param name="ex">The exception object whose message and source properties will be output.</param>
        public virtual void WriteLine(Exception ex)
        {
            this.WriteLine(String.Format("Source: {0}, Message: {1}", ex.Source, ex.Message));
        }

        /// <summary>
        /// Writes a message to the trace listeners in the Listeners collection.
        /// </summary>
        /// <param name="o">An Object name is sent to the Listeners.</param>
        public override void WriteLine(object o)
        {
            this.WriteLine(o.GetType().FullName);
        }

        /// <summary>
        /// Writes a category name and a message to the trace listeners in the Listeners collection.
        /// </summary>
        /// <param name="message">A message to write</param>
        /// <param name="category">A category name used to organize the output.</param>
        public override void WriteLine(string message, string category)
        {
            WriteOutputToFile(PrependPreambleMessage(String.Format("Category: {0} - Message: {1}", message, category)));
        }

        /// <summary>
        /// Writes a category name and a message to the trace listeners in the Listeners collection.
        /// </summary>
        /// <param name="ex">The exception object whose message and source properties will be output.</param>
        /// <param name="category">A category name used to organize the output.</param>
        public virtual void WriteLine(Exception ex, string category)
        {
            this.WriteLine(String.Format("Source: {0}, Message: {1}", ex.Source, ex.Message), category);
        }

        /// <summary>
        /// Writes a category name and a message to the trace listeners in the Listeners collection.
        /// </summary>
        /// <param name="o">An Object name is sent to the Listeners.</param>
        /// <param name="category">A category name used to organize the output.</param>
        public override void WriteLine(object o, string category)
        {
            this.WriteLine(o.GetType().FullName, category);
        }

        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Writes the output of the message to a file.
        /// </summary>
        /// <param name="Message">The message to write.</param>
        private void WriteOutputToFile(string Message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(this.TraceFile, true))
                {
                    writer.AutoFlush = true;
                    writer.WriteLine(Message);
                    writer.Close();
                }
            }
            catch { }
        }

        #endregion
    }
}
