using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Extensions.Standard;
using Xunit;

namespace Diagnostics.Sizeof.XUnitTest
{
    public class SizeInMemTest
    {
        public enum DummyEnum
        {
            Test1,
            Test2,
            Test3,
            Teset4,
            Etc
        }

        [Fact]
        public void SizeInBytesTestPrimitives()
        {
            var i = 1;
            double db = 2;
            decimal dec = 10;
            var f = 421.0f;
            var ch = '\\';
            var str = "ikygutftfgyhji";
            const int ci = 12312;
            var lng = 1203918854123412;

            Assert.Equal(4, i.SizeInBytes());
            Assert.Equal(4, DummyEnum.Etc.SizeInBytes());
            Assert.Equal(8, db.SizeInBytes());
            Assert.Equal(16, dec.SizeInBytes());
            Assert.Equal(4, f.SizeInBytes());
            Assert.Equal(2, ch.SizeInBytes());
            Assert.Equal(2 * str.Length, str.SizeInBytes());
            Assert.Equal(4, ci.SizeInBytes());
            Assert.Equal(8, lng.SizeInBytes());
        }

        [Fact]
        public void Size3()
        {
            var anyDate = new DateTime(7675436767);
            var anyTimeSpan = new TimeSpan(7675436767);
            Assert.True(20 <= anyTimeSpan.SizeInBytes());
            Assert.True(20 <= anyDate.SizeInBytes());
        }
        [Fact]
        public void SizeInBytesGenericArrays()
        {
            var dobArr = new[] { 1.0, 2.3, 3.1, 1.0, 2.3, 3.1, 1.0, 2.3, 3.1, 8.8 };
            Assert.True(10 * 8 + 4 <= dobArr.SizeInBytes());

            var intArr = new[] { 1, 2, 3, 1, 2, 3, 1, 2, 3, 8 };
            Assert.True(10 * 4 + 4 <= intArr.SizeInBytes());

            var chArr = new[] { (char)1, (char)2, (char)3, (char)1, (char)2, (char)3, (char)1, (char)2, (char)3, (char)8 };
            Assert.True(10 * 2 + 4 <= chArr.SizeInBytes());

            var decArr = new[] { 1M, 2M, 3M, 1M, 2M, 3M, 1M, 2M, 3M, 8M };
            Assert.True(10 * 16 + 4 <= decArr.SizeInBytes());
        }


        [Fact]
        public void SizeInBytesArrays()
        {
            var dobArr = new object[] { 1.0, 2.3, 3.1, 1.0, 2.3, 3.1, 1.0, 2.3, 3.1, 8.8 };
            Assert.True(10 * 8 + 4 <= dobArr.SizeInBytes());

            var intArr = new object[] { 1, 2, 3, 1, 2, 3, 1, 2, 3, 8 };
            Assert.True(10 * 4 + 4 <= intArr.SizeInBytes());

            var chArr = new object[] { (char)1, (char)2, (char)3, (char)1, (char)2, (char)3, (char)1, (char)2, (char)3, (char)8 };
            Assert.True(10 * 2 + 4 <= chArr.SizeInBytes());

            var decArr = new object[] { 1M, 2M, 3M, 1M, 2M, 3M, 1M, 2M, 3M, 8M };
            Assert.True(10 * 16 + 4 <= decArr.SizeInBytes());
        }

        [Fact]
        public void SizeInBytesArrays2()
        {
            Array dobArr = new object[] { 1.0, 2.3, 3.1, 1.0, 2.3, 3.1, 1.0, 2.3, 3.1, 8.8 };
            Assert.True(10 * 8 + 4 <= dobArr.SizeInBytes());

            Array intArr = new object[] { 1, 2, 3, 1, 2, 3, 1, 2, 3, 8 };
            Assert.True(10 * 4 + 4 <= intArr.SizeInBytes());

            Array chArr = new object[] { (char)1, (char)2, (char)3, (char)1, (char)2, (char)3, (char)1, (char)2, (char)3, (char)8 };
            Assert.True(10 * 2 + 4 <= chArr.SizeInBytes());

            Array decArr = new object[] { 1M, 2M, 3M, 1M, 2M, 3M, 1M, 2M, 3M, 8M };
            Assert.True(10 * 16 + 4 <= decArr.SizeInBytes());
        }

        [Fact]
        public void SizeInBytesArrayList()
        {
            var dobArr = new ArrayList { 1.0, 2.3, 3.1, 1.0, 2.3, 3.1, 1.0, 2.3, 3.1, 8.8 };
            Assert.True(10 * 8 + 4 < dobArr.SizeInBytes());

            var intArr = new ArrayList { 1, 2, 3, 1, 2, 3, 1, 2, 3, 8 };
            Assert.True(10 * 4 + 4 < intArr.SizeInBytes());

            var chArr = new ArrayList { (char)1, (char)2, (char)3, (char)1, (char)2, (char)3, (char)1, (char)2, (char)3, (char)8 };
            Assert.True(10 * 2 + 4 < chArr.SizeInBytes());

            var decArr = new ArrayList { 1M, 2M, 3M, 1M, 2M, 3M, 1M, 2M, 3M, 8M };
            Assert.True(10 * 16 + 4 < decArr.SizeInBytes());
        }
        [Fact]
        public void SizeInBytesMultidimArrays()
        {
            var dobArr = new[,] { { 1.0, 2.3, 3.1 }, { 1.0, 2.3, 3.1 }, { 1.0, 2.3, 3.1 }, { 8.8, 2.3, 3.1 } };
            Assert.True(12 * 8 + 4 <= dobArr.SizeInBytes());// 4 goes to the array reference 

            var intArr = new[,] { { 1, 2, 3 }, { 1, 2, 3 }, { 1, 2, 3 }, { 8, 1, 2 } };
            Assert.True(12 * 4 + 4 <= intArr.SizeInBytes());// 4 goes to the array reference

        }

        [Fact]
        public void SizeInBytesJaggedArrays()
        {
            var chArr = new[] { new[] { (char)1, (char)2, (char)3 }, new[] { (char)1, (char)2, (char)3 }, new[] { (char)1, (char)2, (char)3 }, new[] { (char)8, (char)1, (char)2 } };
            Assert.True(12 * 2 + 5 * 4 <= chArr.SizeInBytes());// 4 goes to the array reference for each array nested and for array that nests

            var decArr = new[] { new[] { 1M, 2M, 3M }, new[] { 1M, 2M, 3M }, new[] { 1M, 2M, 3M }, new[] { 8M, 5M, 2M } };
            Assert.True(12 * 16 + 5 * 4 <= decArr.SizeInBytes());// 4 goes to the array reference for each array nested
        }

        [Fact]
        public void SizeInBytesMultidimArraysBoxed()
        {
            var dobArr = new object[,] { { 1.0, 2.3, 3.1 }, { 1.0, 2.3, 3.1 }, { 1.0, 2.3, 3.1 }, { 8.8, 2.3, 3.1 } };
            Assert.True(12 * 8 + 4 <= dobArr.SizeInBytes());// 4 goes to the array reference 

            var intArr = new object[,] { { 1, 2, 3 }, { 1, 2, 3 }, { 1, 2, 3 }, { 8, 1, 2 } };
            Assert.True(12 * 4 + 4 <= intArr.SizeInBytes());// 4 goes to the array reference

        }
        [Fact]
        public void SizeInBytesJaggedArraysBoxed()
        {
            var chArr = new[] { new object[] { (char)1, (char)2, (char)3 }, new object[] { (char)1, (char)2, (char)3 }, new object[] { (char)1, (char)2, (char)3 }, new object[] { (char)8, (char)1, (char)2 } };
            Assert.True(12 * 2 + 5 * 4 <= chArr.SizeInBytes());// 4 goes to the array reference for each array nested and for array that nests

            var decArr = new[] { new object[] { 1M, 2M, 3M }, new object[] { 1M, 2M, 3M }, new object[] { 1M, 2M, 3M }, new object[] { 8M, 5M, 2M } };
            Assert.True(12 * 16 + 5 * 4 <= decArr.SizeInBytes());// 4 goes to the array reference for each array nested
        }

        [Fact]
        public void SizeInBytesGenericList()
        {
            // Note that List object will take as many space as it has allocated, so not only Count * sizeof(elem), but rather Capacity * sizeof(elem)
            var zeroTest = new List<double>(0);
            Assert.True(zeroTest.Capacity + 20 <= zeroTest.SizeInBytes());

            var dobList = new List<double> { 1.0, 2.3, 3.1, 1.0, 2.3, 3.1, 1.0, 2.3, 3.1, 8.8 };
            Assert.True(dobList.Capacity * 8 + 20 <= dobList.SizeInBytes());

            var dobList2 = new List<double>(128) { 1.0, 2.3, 3.1, 1.0, 2.3, 3.1, 1.0, 2.3, 3.1, 8.8 };
            Assert.True(dobList2.Capacity * 8 + 20 <= dobList2.SizeInBytes());

            var intArr = new List<int> { 1, 2, 3, 1, 2, 3, 1, 2, 3, 8 };
            Assert.True(intArr.Capacity * 4 + 20 <= intArr.SizeInBytes());

            var chArr = new List<char> { (char)1, (char)2, (char)3, (char)1, (char)2, (char)3, (char)1, (char)2, (char)3, (char)8 };
            Assert.True(chArr.Capacity * 2 + 20 <= chArr.SizeInBytes());

            var decArr = new List<decimal> { 1M, 2M, 3M, 1M, 2M, 3M, 1M, 2M, 3M, 8M };
            Assert.True(decArr.Capacity * 16 + 20 <= decArr.SizeInBytes());
        }

        [Fact]
        public void SizeInBytesGenericLinkedList()
        {
            // Note that List object will take as many space as it has allocated, so not only Count * sizeof(elem), but rather Capacity * sizeof(elem)
            var zeroTest = new LinkedList<double>();
            Assert.True(8 < zeroTest.SizeInBytes());// 20: this is how much takes List object

            var dobList = new LinkedList<double>(new List<double> { 1.0, 2.3, 3.1, 1.0, 2.3, 3.1, 1.0, 2.3, 3.1, 8.8 });
            Assert.True(80 < dobList.SizeInBytes());

            var dobList2 = new List<double>(128) { 1.0, 2.3, 3.1, 1.0, 2.3, 3.1, 1.0, 2.3, 3.1, 8.8 };
            Assert.True(80 < dobList2.SizeInBytes());

            var intArr = new List<int> { 1, 2, 3, 1, 2, 3, 1, 2, 3, 8 };
            Assert.True(40 < intArr.SizeInBytes());

            var chArr = new List<char> { (char)1, (char)2, (char)3, (char)1, (char)2, (char)3, (char)1, (char)2, (char)3, (char)8 };
            Assert.True(20 < chArr.SizeInBytes());

            var decArr = new List<decimal> { 1M, 2M, 3M, 1M, 2M, 3M, 1M, 2M, 3M, 8M };
            Assert.True(160 < decArr.SizeInBytes());
        }

        [Fact]
        public void SizeInBytesGenericEnumerablesDownCasted()
        {
            // Note that List object will take as many space as it has allocated, so not only Count * sizeof(elem) + b, but rather Capacity * sizeof(elem)
            IEnumerable<double> zeroTest = new List<double>(0);
            Assert.True(20 <= zeroTest.ToList().SizeInBytes());// 20: this is how much takes List object

            IEnumerable<double> dobList = new List<double> { 1.0, 2.3, 3.1, 1.0, 2.3, 3.1, 1.0, 2.3, 3.1, 8.8 };
            Assert.True(((List<double>)dobList).Capacity * 8 + 20 <= dobList.SizeInBytes());

            IEnumerable<double> dobList2 = new List<double>(128) { 1.0, 2.3, 3.1, 1.0, 2.3, 3.1, 1.0, 2.3, 3.1, 8.8 };
            Assert.True(((List<double>)dobList2).Capacity * 8 + 20 <= dobList2.SizeInBytes());

            IEnumerable<int> intArr = new List<int> { 1, 2, 3, 1, 2, 3, 1, 2, 3, 8 };
            Assert.True(((List<int>)intArr).Capacity * 4 + 20 <= intArr.SizeInBytes());

            IEnumerable<char> chArr = new List<char> { (char)1, (char)2, (char)3, (char)1, (char)2, (char)3, (char)1, (char)2, (char)3, (char)8 };
            Assert.True(((List<char>)chArr).Capacity * 2 + 20 <= chArr.SizeInBytes());

            IEnumerable<decimal> decArr = new List<decimal> { 1M, 2M, 3M, 1M, 2M, 3M, 1M, 2M, 3M, 8M };
            Assert.True(((List<decimal>)decArr).Capacity * 16 + 20 <= decArr.SizeInBytes());
        }

        [Fact]
        public void SizeInBytesEnumerableDownCasted()
        {
            // Note that List object will take as many space as it has allocated, so not only Count * sizeof(elem) + b, but rather Capacity * sizeof(elem)
            IEnumerable zeroTest = new List<double>(0);
            Assert.True(20 <= zeroTest.SizeInBytes());

            IEnumerable dobList = new List<double> { 1.0, 2.3, 3.1, 1.0, 2.3, 3.1, 1.0, 2.3, 3.1, 8.8 };
            Assert.True(((List<double>)dobList).Capacity * 8 + 20 <= dobList.SizeInBytes());

            IEnumerable dobList2 = new List<double>(128) { 1.0, 2.3, 3.1, 1.0, 2.3, 3.1, 1.0, 2.3, 3.1, 8.8 };
            Assert.True(((List<double>)dobList2).Capacity * 8 + 20 <= dobList2.SizeInBytes());

            IEnumerable intArr = new List<int> { 1, 2, 3, 1, 2, 3, 1, 2, 3, 8 };
            Assert.True(((List<int>)intArr).Capacity * 4 + 20 <= intArr.SizeInBytes());

            IEnumerable chArr = new List<char> { (char)1, (char)2, (char)3, (char)1, (char)2, (char)3, (char)1, (char)2, (char)3, (char)8 };
            Assert.True(((List<char>)chArr).Capacity * 2 + 20 <= chArr.SizeInBytes());

            IEnumerable decArr = new List<decimal> { 1M, 2M, 3M, 1M, 2M, 3M, 1M, 2M, 3M, 8M };
            Assert.True(((List<decimal>)decArr).Capacity * 16 + 20 <= decArr.SizeInBytes());
        }

        [Fact]
        public void SizeInBytesStringBuilder()
        {
            var stbTest = new StringBuilder(100);
            var size1 = stbTest.SizeInBytes();
            Assert.True(size1 >= 20);
            stbTest.Append("efewfwefwefsfdsfdf");
            var size2 = stbTest.SizeInBytes();
            Assert.True(size2 > size1);
        }

        [Fact]
        public void SizeInBytesStringWriter()
        {
            var stbTest = new StringWriter();
            var size1 = stbTest.SizeInBytes();
            Assert.True(size1 >= 20);
            stbTest.Write("efewfwefwefsfdsfdf");
            var size2 = stbTest.SizeInBytes();
            Assert.True(size2 > size1);
        }

        [Fact]
        public void SizeInBytesTextWriter()
        {
            TextWriter stbTest = new StringWriter();
            var size1 = stbTest.SizeInBytes();
            Assert.True(size1 >= 16);
            stbTest.Write("efewfwefwefsfdsfdf");
            var size2 = stbTest.SizeInBytes();
            Assert.True(size2 > size1);
        }

        [Fact]
        public void SizeInBytesTextWriterSynchronized()
        {
            var stbTest = TextWriter.Synchronized(new StringWriter());
            var size1 = stbTest.SizeInBytes();
            Assert.True(size1 >= 16);
            stbTest.Write("efewfwefwefsfdsfdf");
            var size2 = stbTest.SizeInBytes();
            Assert.True(size2 > size1);
        }

        [Fact]
        public void SizeInBytesStringWriterSynchronized()
        {
            var stbTest = TextWriter.Synchronized(new StringWriter());
            var size1 = stbTest.SizeInBytes();
            Assert.True(size1 >= 20);
            stbTest.Write("efewfwefwefsfdsfdf");
            var size2 = stbTest.SizeInBytes();
            Assert.True(size2 > size1);
        }

        [Fact]
        public void SizeInBytesStreamWriterSynchronized()
        {
            var stbTest = TextWriter.Synchronized(new StreamWriter("TestFile.txt"));
            var size1 = stbTest.SizeInBytes();
            Assert.True(size1 >= 20);
            stbTest.Write("efewfwefwefsfdsfdf");
            var size2 = stbTest.SizeInBytes();
            Assert.True(size2 > size1);
        }

        [Fact]
        public void SizeInBytesActionInMemoryTest()
        {
            Action dummyAnonymousAction1 = () => { };
            var result = dummyAnonymousAction1.SizeInBytes();
            Console.WriteLine(result.AsMemory());
        }
        long RandomMethod(Action someAcionRef)
        {
            return someAcionRef.SizeInBytes();
        }
        [Fact]
        public void SizeInBytesActionInMemory1Test()
        {
            Action dummyAnonymousAction1 = () => { };
            var result = RandomMethod(dummyAnonymousAction1);
            Console.WriteLine(result.AsMemory());
        }
        [Fact]
        public void SizeInBytesActionInMemory2Test()
        {
            Action dummyAnonymousAction1 = () => { };
            var referenceToAction = dummyAnonymousAction1;
            var result = RandomMethod(referenceToAction);
            Console.WriteLine(result.AsMemory());
        }

        public static void RunX(Action test)
        {
            var x = test.SizeInBytes();
        }
        [Fact]
        public void SizeInBytesActionInMemoryRecurentFunckupTest()
        {
            Action actionT = () => { };
            var name = actionT.Method.Name;
            var x = actionT.SizeInBytes();
            Console.WriteLine($"Test: size = {x.AsMemory()}");
            RunX(actionT);
        }
        [Fact]
        public void SizeInBytesModuleHandleTestTest()
        {
            var handle = new ModuleHandle();
            var result = handle.SizeInBytes();
            Console.WriteLine($"{result} bytes");
            Assert.True(result >= 8);
        }
        [Fact]
        public void SizeInBytesHandleRefTest()
        {
            var handle = new HandleRef();
            var result = handle.SizeInBytes();
            Console.WriteLine($"{result} bytes");
            Assert.True(result >= 8);
        }
        class Foo
        {
            public Bar bar;
        }
        class Bar
        {
            public Foo foo;
        }

        [Fact]
        public void CircularReferenceTest()
        {
            var f = new Foo();
            var b = new Bar();
            f.bar = b;
            b.foo = f;
            var result = f.SizeInBytes();
            Console.WriteLine($"{result} bytes");
            Assert.True(result >= 8);
            Assert.True(result < 128);
        }
    }
}
