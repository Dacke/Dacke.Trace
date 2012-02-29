using System;
namespace Dacke.TraceListeners
{
    interface ICustomTraceListener
    {
        void Write(Exception ex);
        void Write(Exception ex, string category);
        void Write(object o);
        void Write(object o, string category);
        void Write(string message);
        void Write(string message, string category);
        void WriteLine(Exception ex);
        void WriteLine(Exception ex, string category);
        void WriteLine(object o);
        void WriteLine(object o, string category);
        void WriteLine(string message);
        void WriteLine(string message, string category);
    }
}
