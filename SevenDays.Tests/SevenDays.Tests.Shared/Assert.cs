using System;
using System.Collections.Generic;
using System.Text;

namespace SevenDays.Tests.Shared
{
    public static class Assert
    {
        public static void True(bool condition)
        {
            NUnit.Framework.Assert.True(condition);
            Xunit.Assert.True(condition);
        }

        public static void AreEqual(string expected, string actual)
        {
            NUnit.Framework.Assert.AreEqual(expected, actual);
            Xunit.Assert.Equal(expected, actual);
        }

        public static void NotNull(object @object)
        {
            NUnit.Framework.Assert.NotNull(@object);
            Xunit.Assert.NotNull(@object);
        }
    }
}
