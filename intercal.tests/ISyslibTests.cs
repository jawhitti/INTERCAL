using INTERCAL.Runtime;
using Xunit;

using ExecutionContext = INTERCAL.Runtime.ExecutionContext;

namespace intercal.tests
{
    /// <summary>
    /// Tests for the INTERCAL-compiled isyslib (syslib64 compiled from .i source).
    /// Entry point labels are ASCII-encoded names packed into 64-bit values.
    /// </summary>
    public class ISyslibTests
    {
        private isyslib lib = new isyslib();

        // Label constants (ASCII-encoded)
        const long ADD16    = 4702958889031696384;
        const long ADD32    = 4702958897554522112;
        const long MINUS16  = 5569068542595249664;
        const long MINUS32  = 5569068542595379712;
        const long TIMES16  = 6073470532629640704;
        const long DIVIDE16 = 4920558940556964150;
        const long MODULO16 = 5570746397223760182;
        const long RANDOM16 = 5927104639891484982;
        const long RANDOM32 = 5927104639891485490;

        private void Call(long label, ExecutionContext ctx)
        {
            // Find and invoke the entry method by name
            var method = typeof(isyslib).GetMethod("DO_" + label);
            Assert.NotNull(method);
            method.Invoke(lib, new object[] { ctx });
        }

        // ================================================================
        // ADD16: .1 + .2 -> .3
        // ================================================================

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 1, 2)]
        [InlineData(100, 200, 300)]
        [InlineData(65534, 1, 65535)]
        public void IADD16(int a, int b, int expected)
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = (ulong)a;
            ctx[".2"] = (ulong)b;
            Call(ADD16, ctx);
            Assert.Equal((ulong)expected, ctx[".3"]);
        }

        // ================================================================
        // MINUS16: .1 - .2 -> .3
        // ================================================================

        [Theory]
        [InlineData(1, 0, 1)]
        [InlineData(1, 1, 0)]
        [InlineData(2, 1, 1)]
        [InlineData(7, 1, 6)]
        [InlineData(10, 3, 7)]
        [InlineData(100, 50, 50)]
        [InlineData(255, 1, 254)]
        [InlineData(65535, 1, 65534)]
        [InlineData(65535, 65535, 0)]
        public void IMINUS16(int a, int b, int expected)
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = (ulong)a;
            ctx[".2"] = (ulong)b;
            Call(MINUS16, ctx);
            Assert.Equal((ulong)expected, ctx[".3"]);
        }

        // ================================================================
        // MODULO16: .1 mod .2 -> .3
        // ================================================================

        [Theory]
        [InlineData(10, 3, 1)]
        [InlineData(15, 5, 0)]
        [InlineData(7, 2, 1)]
        [InlineData(100, 7, 2)]
        public void IMODULO16(int a, int b, int expected)
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = (ulong)a;
            ctx[".2"] = (ulong)b;
            Call(MODULO16, ctx);
            Assert.Equal((ulong)expected, ctx[".3"]);
        }

        // ================================================================
        // DIVIDE16: .1 / .2 -> .3 remainder .4
        // ================================================================

        [Theory]
        [InlineData(42, 7, 6, 0)]
        [InlineData(10, 3, 3, 1)]
        [InlineData(7, 2, 3, 1)]
        [InlineData(1, 1, 1, 0)]
        [InlineData(0, 5, 0, 0)]
        [InlineData(65535, 256, 255, 255)]
        public void IDIVIDE16(int a, int b, int quotient, int remainder)
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = (ulong)a;
            ctx[".2"] = (ulong)b;
            Call(DIVIDE16, ctx);
            Assert.Equal((ulong)quotient, ctx[".3"]);
            Assert.Equal((ulong)remainder, ctx[".4"]);
        }

        // ================================================================
        // RANDOM16: -> .1
        // ================================================================

        [Fact]
        public void IRANDOM16_Produces_Value()
        {
            var ctx = new ExecutionContext();
            Call(RANDOM16, ctx);
            Assert.True(ctx[".1"] <= 65535);
        }
    }
}
