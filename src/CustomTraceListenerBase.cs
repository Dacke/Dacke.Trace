using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

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
            Write((object)message);
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
        /// <param name="ex">The exeception object that you want to output.</param>
        public void Write(Exception ex)
        {
            Write((object)String.Format("Source: {0}, Message: {1}", ex.Source, ex.Message));
        }
        
        /// <summary>
        /// Writes the value of the object's ToString method to the listener you create when you implement the TraceListener class.
        /// </summary>
        /// <param name="o">An Object whose fully qualified class name you want to write. </param>
        public override void Write(object o)
        {
            base.Write(o);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="category"></param>
        public override void Write(string message, string category)
        {
            base.Write(message, category);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="category"></param>
        public void Write(Exception ex, string category)
        {
            base.Write(ex, category);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <param name="category"></param>
        public override void Write(object o, string category)
        {
            base.Write(o, category);
        }

        #endregion

        #region WriteLine Overrides

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public override void WriteLine(string message)
        {
            base.WriteLine(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public void WriteLine(Exception ex)
        {
            base.WriteLine(ex.Message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        public override void WriteLine(object o)
        {
            base.WriteLine(o);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="category"></param>
        public override void WriteLine(string message, string category)
        {
            base.WriteLine(message, category);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="category"></param>
        public void WriteLine(Exception ex, string category)
        {
            base.WriteLine(ex, category);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <param name="category"></param>
        public override void WriteLine(object o, string category)
        {
            base.WriteLine(o, category);
        }

        #endregion

        #region Private Methods


        #endregion
    }
}
