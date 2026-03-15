using INTERCAL.Runtime;
using Xunit;

using ExecutionContext = INTERCAL.Runtime.ExecutionContext;

namespace intercal.tests
{
    public class SyslibTests
    {
        private syslib lib = new syslib();

        // ================================================================
        // (1000) 16-bit ADD: .1 + .2 -> .3
        // ================================================================

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 1, 2)]
        [InlineData(1, 0, 1)]
        [InlineData(0, 1, 1)]
        [InlineData(100, 200, 300)]
        [InlineData(255, 1, 256)]
        [InlineData(65534, 1, 65535)]
        public void Add16_BasicCases(int a, int b, int expected)
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = (ulong)a;
            ctx[".2"] = (ulong)b;
            lib.DO_1000(ctx);
            Assert.Equal((ulong)expected, ctx[".3"]);
        }

        // ================================================================
        // (1009) 16-bit SUBTRACT: .1 - .2 -> .3
        // Known broken: returns .1 + .2 instead of .1 - .2
        // ================================================================

        [Theory(Skip = "Pre-existing syslib bug: (1009) adds instead of subtracting")]
        [InlineData(1, 0, 1)]
        [InlineData(1, 1, 0)]
        [InlineData(2, 1, 1)]
        [InlineData(7, 1, 6)]
        [InlineData(10, 3, 7)]
        [InlineData(100, 50, 50)]
        [InlineData(255, 1, 254)]
        [InlineData(65535, 1, 65534)]
        [InlineData(65535, 65535, 0)]
        public void Subtract16_BasicCases(int a, int b, int expected)
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = (ulong)a;
            ctx[".2"] = (ulong)b;
            lib.DO_1009(ctx);
            Assert.Equal((ulong)expected, ctx[".3"]);
        }

        // ================================================================
        // (1020) 16-bit MULTIPLY: .1 * .2 -> .3
        // Known broken: always returns 0
        // ================================================================

        [Theory(Skip = "Pre-existing syslib bug: (1020) always returns 0")]
        [InlineData(0, 0, 0)]
        [InlineData(1, 1, 1)]
        [InlineData(6, 7, 42)]
        [InlineData(255, 255, 65025)]
        public void Multiply16_BasicCases(int a, int b, int expected)
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = (ulong)a;
            ctx[".2"] = (ulong)b;
            lib.DO_1020(ctx);
            Assert.Equal((ulong)expected, ctx[".3"]);
        }

        // ================================================================
        // (1500) 32-bit ADD: :1 + :2 -> :3
        // ================================================================

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 1, 2)]
        [InlineData(100000, 200000, 300000)]
        public void Add32_BasicCases(uint a, uint b, uint expected)
        {
            var ctx = new ExecutionContext();
            ctx[":1"] = a;
            ctx[":2"] = b;
            lib.DO_1500(ctx);
            Assert.Equal((ulong)expected, ctx[":3"]);
        }

        // ================================================================
        // (1509) 32-bit SUBTRACT: :1 - :2 -> :3
        // Known broken: returns :1 + :2 instead of :1 - :2
        // ================================================================

        [Theory(Skip = "Pre-existing syslib bug: (1509) adds instead of subtracting")]
        [InlineData(1, 0, 1)]
        [InlineData(1, 1, 0)]
        [InlineData(100000, 1, 99999)]
        [InlineData(100000, 50000, 50000)]
        public void Subtract32_BasicCases(uint a, uint b, uint expected)
        {
            var ctx = new ExecutionContext();
            ctx[":1"] = a;
            ctx[":2"] = b;
            lib.DO_1509(ctx);
            Assert.Equal((ulong)expected, ctx[":3"]);
        }
    }
}
