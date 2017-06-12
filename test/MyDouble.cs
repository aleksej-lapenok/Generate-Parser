using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace test
{
    public class MyDouble
    {
        private double val;

        public MyDouble(string val)
        {
            Value = double.Parse(val);
        }

        public MyDouble(double val)
        {
            Value = val;
        }

        public double Value
        {
            get => val;
            private set => val = value;
        }

        public MyDouble Add(MyDouble other)
        {
            return new MyDouble(val + other.val);
        }

        public MyDouble Mul(MyDouble other)
        {
            return new MyDouble(val * other.val);
        }

        public MyDouble Sub(MyDouble other)
        {
            return new MyDouble(val - other.val);
        }
    }
}