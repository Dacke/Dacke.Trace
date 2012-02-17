using Dacke.TraceListeners;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace UnitTests
{   
    /// <summary>
    /// This is a test class for CustomTraceListenerBaseTest and is intended
    /// to contain all CustomTraceListenerBaseTest Unit Tests
    /// </summary>
    [TestFixture]
    public class CustomTraceListenerBaseTest
    {
        private CustomTraceListenerBase _sut = new CustomTraceListenerBase();
        private const String _strCategory = "String Category";

        /// <summary>
        /// This method will test all of the Write string methods.
        /// </summary>
        [Test]
        public void WriteStringTests()
        {
            var strVariable = "String Variable Message";
            const string strConstant = "String Constant Message";

            Assert.DoesNotThrow(() =>
            {
                _sut.Write(strConstant);
                _sut.Write("String Literal");
                _sut.Write(strVariable);
                _sut.Write(strConstant, _strCategory);
                _sut.Write(strVariable, _strCategory);
            });
        }

        /// <summary>
        /// This method will test all of the Write object methods.
        /// </summary>
        [Test]
        public void WriteObjectTests()
        {
            Assert.DoesNotThrow(() =>
            {
                _sut.Write(DateTime.Now);
                _sut.Write(DateTime.Now, _strCategory);
            });
        }

        /// <summary>
        /// This method will test all of the Write exception methods.
        /// </summary>
        [Test]
        public void WriteExceptionTests()
        {
            var unthrownException = new NotImplementedException("Sample Exception Message");

            Assert.DoesNotThrow(() =>
            {
                _sut.Write(unthrownException);
                _sut.Write(unthrownException, _strCategory);
            });
        }

        /// <summary>
        /// This method will test all of the WriteLine string methods.
        /// </summary>
        [Test]
        public void WriteLineStringTests()
        {
            var strVariable = "String Variable Message";
            const string strConstant = "String Constant Message";

            Assert.DoesNotThrow(() =>
                                    {
                                        _sut.WriteLine(strConstant);
                                        _sut.WriteLine("String Literal");
                                        _sut.WriteLine(strVariable);
                                        _sut.WriteLine(strConstant, _strCategory);
                                        _sut.WriteLine(strVariable, _strCategory);
                                    });
        }

        /// <summary>
        /// This method will test all of the WriteLine object methods.
        /// </summary>
        [Test]
        public void WriteLineObjectTests()
        {
            var obj = new Tuple<int, string>(45, "Tuple String Value");

            Assert.DoesNotThrow(() =>
                                    {
                                        _sut.WriteLine(obj);
                                        _sut.WriteLine(obj, _strCategory);
                                    });
        }

        /// <summary>
        /// This method will test all of the WriteLine exception methods.
        /// </summary>
        [Test]
        public void WriteLineExceptionTests()
        {
            var unthrownException = new NotImplementedException("Sample Exception Message");

            Assert.DoesNotThrow(() =>
                                    {
                                        _sut.WriteLine(unthrownException);
                                        _sut.WriteLine(unthrownException, _strCategory);
                                    });
        }
    }
}
