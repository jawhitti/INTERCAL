using INTERCAL.Runtime;
using Xunit;

using ExecutionContext = INTERCAL.Runtime.ExecutionContext;

namespace intercal.tests
{
    public class Syslib64Tests
    {
        // ================================================================
        // ADD16: .1 + .2 -> .3
        // ================================================================

        [Fact]
        public void ADD16_BasicAddition()
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = 3;
            ctx[".2"] = 4;
            syslib64_native.DO_ADD16(new ComponentCall(ctx));
            Assert.Equal(7UL, ctx[".3"]);
        }

        [Fact]
        public void ADD16_ZeroPlusZero()
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = 0;
            ctx[".2"] = 0;
            syslib64_native.DO_ADD16(new ComponentCall(ctx));
            Assert.Equal(0UL, ctx[".3"]);
        }

        [Fact]
        public void ADD16_MaxValues()
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = 65534;
            ctx[".2"] = 1;
            syslib64_native.DO_ADD16(new ComponentCall(ctx));
            Assert.Equal(65535UL, ctx[".3"]);
        }

        [Fact]
        public void ADD16_Overflow_Throws()
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = 65535;
            ctx[".2"] = 1;
            Assert.Throws<IntercalException>(() => syslib64_native.DO_ADD16(new ComponentCall(ctx)));
        }

        // ================================================================
        // ADD32: :1 + :2 -> :3
        // ================================================================

        [Fact]
        public void ADD32_BasicAddition()
        {
            var ctx = new ExecutionContext();
            ctx[":1"] = 100000;
            ctx[":2"] = 200000;
            syslib64_native.DO_ADD32(new ComponentCall(ctx));
            Assert.Equal(300000UL, ctx[":3"]);
        }

        [Fact]
        public void ADD32_Overflow_Throws()
        {
            var ctx = new ExecutionContext();
            ctx[":1"] = uint.MaxValue;
            ctx[":2"] = 1;
            Assert.Throws<IntercalException>(() => syslib64_native.DO_ADD32(new ComponentCall(ctx)));
        }

        // ================================================================
        // ADD64: ::1 + ::2 -> ::3
        // ================================================================

        [Fact]
        public void ADD64_BasicAddition()
        {
            var ctx = new ExecutionContext();
            ctx["::1"] = 1000000000000UL;
            ctx["::2"] = 2000000000000UL;
            syslib64_native.DO_ADD64(new ComponentCall(ctx));
            Assert.Equal(3000000000000UL, ctx["::3"]);
        }

        [Fact]
        public void ADD64_LargeValues()
        {
            var ctx = new ExecutionContext();
            ctx["::1"] = ulong.MaxValue - 1;
            ctx["::2"] = 1;
            syslib64_native.DO_ADD64(new ComponentCall(ctx));
            Assert.Equal(ulong.MaxValue, ctx["::3"]);
        }

        [Fact]
        public void ADD64_Overflow_Throws()
        {
            var ctx = new ExecutionContext();
            ctx["::1"] = ulong.MaxValue;
            ctx["::2"] = 1;
            Assert.Throws<IntercalException>(() => syslib64_native.DO_ADD64(new ComponentCall(ctx)));
        }

        // ================================================================
        // MINUS16: .1 - .2 -> .3
        // ================================================================

        [Fact]
        public void MINUS16_BasicSubtraction()
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = 10;
            ctx[".2"] = 3;
            syslib64_native.DO_MINUS16(new ComponentCall(ctx));
            Assert.Equal(7UL, ctx[".3"]);
        }

        [Fact]
        public void MINUS16_SubtractToZero()
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = 42;
            ctx[".2"] = 42;
            syslib64_native.DO_MINUS16(new ComponentCall(ctx));
            Assert.Equal(0UL, ctx[".3"]);
        }

        [Fact]
        public void MINUS16_Underflow_Throws()
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = 3;
            ctx[".2"] = 10;
            Assert.Throws<IntercalException>(() => syslib64_native.DO_MINUS16(new ComponentCall(ctx)));
        }

        // ================================================================
        // MINUS32: :1 - :2 -> :3
        // ================================================================

        [Fact]
        public void MINUS32_BasicSubtraction()
        {
            var ctx = new ExecutionContext();
            ctx[":1"] = 1000000;
            ctx[":2"] = 999999;
            syslib64_native.DO_MINUS32(new ComponentCall(ctx));
            Assert.Equal(1UL, ctx[":3"]);
        }

        [Fact]
        public void MINUS32_Underflow_Throws()
        {
            var ctx = new ExecutionContext();
            ctx[":1"] = 0;
            ctx[":2"] = 1;
            Assert.Throws<IntercalException>(() => syslib64_native.DO_MINUS32(new ComponentCall(ctx)));
        }

        // ================================================================
        // MINUS64: ::1 - ::2 -> ::3
        // ================================================================

        [Fact]
        public void MINUS64_BasicSubtraction()
        {
            var ctx = new ExecutionContext();
            ctx["::1"] = 5000000000000UL;
            ctx["::2"] = 3000000000000UL;
            syslib64_native.DO_MINUS64(new ComponentCall(ctx));
            Assert.Equal(2000000000000UL, ctx["::3"]);
        }

        [Fact]
        public void MINUS64_Underflow_Throws()
        {
            var ctx = new ExecutionContext();
            ctx["::1"] = 0;
            ctx["::2"] = 1;
            Assert.Throws<IntercalException>(() => syslib64_native.DO_MINUS64(new ComponentCall(ctx)));
        }

        // ================================================================
        // TIMES16: .1 * .2 -> :3 (16x16 -> 32-bit result)
        // ================================================================

        [Fact]
        public void TIMES16_BasicMultiply()
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = 6;
            ctx[".2"] = 7;
            syslib64_native.DO_TIMES16(new ComponentCall(ctx));
            Assert.Equal(42UL, ctx[":3"]);
        }

        [Fact]
        public void TIMES16_MaxValues()
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = 65535;
            ctx[".2"] = 65535;
            syslib64_native.DO_TIMES16(new ComponentCall(ctx));
            Assert.Equal((ulong)65535 * 65535, ctx[":3"]);
        }

        [Fact]
        public void TIMES16_ByZero()
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = 12345;
            ctx[".2"] = 0;
            syslib64_native.DO_TIMES16(new ComponentCall(ctx));
            Assert.Equal(0UL, ctx[":3"]);
        }

        // ================================================================
        // TIMES32: :1 * :2 -> ::3 (32x32 -> 64-bit result)
        // ================================================================

        [Fact]
        public void TIMES32_BasicMultiply()
        {
            var ctx = new ExecutionContext();
            ctx[":1"] = 100000;
            ctx[":2"] = 200000;
            syslib64_native.DO_TIMES32(new ComponentCall(ctx));
            Assert.Equal(20000000000UL, ctx["::3"]);
        }

        [Fact]
        public void TIMES32_MaxValues()
        {
            var ctx = new ExecutionContext();
            ctx[":1"] = uint.MaxValue;
            ctx[":2"] = uint.MaxValue;
            syslib64_native.DO_TIMES32(new ComponentCall(ctx));
            Assert.Equal((ulong)uint.MaxValue * uint.MaxValue, ctx["::3"]);
        }

        // ================================================================
        // TIMES64: ::1 * ::2 -> ::3 (truncated to 64 bits)
        // ================================================================

        [Fact]
        public void TIMES64_BasicMultiply()
        {
            var ctx = new ExecutionContext();
            ctx["::1"] = 1000000UL;
            ctx["::2"] = 1000000UL;
            syslib64_native.DO_TIMES64(new ComponentCall(ctx));
            Assert.Equal(1000000000000UL, ctx["::3"]);
        }

        [Fact]
        public void TIMES64_ByZero()
        {
            var ctx = new ExecutionContext();
            ctx["::1"] = ulong.MaxValue;
            ctx["::2"] = 0;
            syslib64_native.DO_TIMES64(new ComponentCall(ctx));
            Assert.Equal(0UL, ctx["::3"]);
        }

        [Fact]
        public void TIMES64_ByOne()
        {
            var ctx = new ExecutionContext();
            ctx["::1"] = 123456789UL;
            ctx["::2"] = 1;
            syslib64_native.DO_TIMES64(new ComponentCall(ctx));
            Assert.Equal(123456789UL, ctx["::3"]);
        }

        // ================================================================
        // DIVIDE16: .1 / .2 -> .3 remainder .4
        // ================================================================

        [Fact]
        public void DIVIDE16_ExactDivision()
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = 42;
            ctx[".2"] = 7;
            syslib64_native.DO_DIVIDE16(new ComponentCall(ctx));
            Assert.Equal(6UL, ctx[".3"]);
            Assert.Equal(0UL, ctx[".4"]);
        }

        [Fact]
        public void DIVIDE16_WithRemainder()
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = 10;
            ctx[".2"] = 3;
            syslib64_native.DO_DIVIDE16(new ComponentCall(ctx));
            Assert.Equal(3UL, ctx[".3"]);
            Assert.Equal(1UL, ctx[".4"]);
        }

        [Fact]
        public void DIVIDE16_DividendSmallerThanDivisor()
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = 3;
            ctx[".2"] = 10;
            syslib64_native.DO_DIVIDE16(new ComponentCall(ctx));
            Assert.Equal(0UL, ctx[".3"]);
            Assert.Equal(3UL, ctx[".4"]);
        }

        [Fact]
        public void DIVIDE16_DivideByOne()
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = 65535;
            ctx[".2"] = 1;
            syslib64_native.DO_DIVIDE16(new ComponentCall(ctx));
            Assert.Equal(65535UL, ctx[".3"]);
            Assert.Equal(0UL, ctx[".4"]);
        }

        [Fact]
        public void DIVIDE16_DivideByZero_Throws()
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = 42;
            ctx[".2"] = 0;
            Assert.Throws<IntercalException>(() => syslib64_native.DO_DIVIDE16(new ComponentCall(ctx)));
        }

        [Fact]
        public void DIVIDE16_ZeroDividend()
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = 0;
            ctx[".2"] = 5;
            syslib64_native.DO_DIVIDE16(new ComponentCall(ctx));
            Assert.Equal(0UL, ctx[".3"]);
            Assert.Equal(0UL, ctx[".4"]);
        }

        [Fact]
        public void DIVIDE16_FizzBuzz_Mod3()
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = 15;
            ctx[".2"] = 3;
            syslib64_native.DO_DIVIDE16(new ComponentCall(ctx));
            Assert.Equal(5UL, ctx[".3"]);
            Assert.Equal(0UL, ctx[".4"]);
        }

        [Fact]
        public void DIVIDE16_FizzBuzz_Mod5()
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = 15;
            ctx[".2"] = 5;
            syslib64_native.DO_DIVIDE16(new ComponentCall(ctx));
            Assert.Equal(3UL, ctx[".3"]);
            Assert.Equal(0UL, ctx[".4"]);
        }

        // ================================================================
        // DIVIDE32: :1 / :2 -> :3 remainder :4
        // ================================================================

        [Fact]
        public void DIVIDE32_ExactDivision()
        {
            var ctx = new ExecutionContext();
            ctx[":1"] = 1000000;
            ctx[":2"] = 1000;
            syslib64_native.DO_DIVIDE32(new ComponentCall(ctx));
            Assert.Equal(1000UL, ctx[":3"]);
            Assert.Equal(0UL, ctx[":4"]);
        }

        [Fact]
        public void DIVIDE32_WithRemainder()
        {
            var ctx = new ExecutionContext();
            ctx[":1"] = 1000001;
            ctx[":2"] = 1000;
            syslib64_native.DO_DIVIDE32(new ComponentCall(ctx));
            Assert.Equal(1000UL, ctx[":3"]);
            Assert.Equal(1UL, ctx[":4"]);
        }

        [Fact]
        public void DIVIDE32_DivideByZero_Throws()
        {
            var ctx = new ExecutionContext();
            ctx[":1"] = 42;
            ctx[":2"] = 0;
            Assert.Throws<IntercalException>(() => syslib64_native.DO_DIVIDE32(new ComponentCall(ctx)));
        }

        [Fact]
        public void DIVIDE32_LargeValues()
        {
            var ctx = new ExecutionContext();
            ctx[":1"] = uint.MaxValue;
            ctx[":2"] = 2;
            syslib64_native.DO_DIVIDE32(new ComponentCall(ctx));
            Assert.Equal((ulong)(uint.MaxValue / 2), ctx[":3"]);
            Assert.Equal(1UL, ctx[":4"]);
        }

        // ================================================================
        // DIVIDE64: ::1 / ::2 -> ::3 remainder ::4
        // ================================================================

        [Fact]
        public void DIVIDE64_ExactDivision()
        {
            var ctx = new ExecutionContext();
            ctx["::1"] = 1000000000000UL;
            ctx["::2"] = 1000000UL;
            syslib64_native.DO_DIVIDE64(new ComponentCall(ctx));
            Assert.Equal(1000000UL, ctx["::3"]);
            Assert.Equal(0UL, ctx["::4"]);
        }

        [Fact]
        public void DIVIDE64_WithRemainder()
        {
            var ctx = new ExecutionContext();
            ctx["::1"] = 7UL;
            ctx["::2"] = 2UL;
            syslib64_native.DO_DIVIDE64(new ComponentCall(ctx));
            Assert.Equal(3UL, ctx["::3"]);
            Assert.Equal(1UL, ctx["::4"]);
        }

        [Fact]
        public void DIVIDE64_DivideByZero_Throws()
        {
            var ctx = new ExecutionContext();
            ctx["::1"] = 42;
            ctx["::2"] = 0;
            Assert.Throws<IntercalException>(() => syslib64_native.DO_DIVIDE64(new ComponentCall(ctx)));
        }

        // ================================================================
        // MODULO16: .1 mod .2 -> .3
        // ================================================================

        [Fact]
        public void MODULO16_Basic()
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = 10;
            ctx[".2"] = 3;
            syslib64_native.DO_MODULO16(new ComponentCall(ctx));
            Assert.Equal(1UL, ctx[".3"]);
        }

        [Fact]
        public void MODULO16_ExactlyDivisible()
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = 15;
            ctx[".2"] = 5;
            syslib64_native.DO_MODULO16(new ComponentCall(ctx));
            Assert.Equal(0UL, ctx[".3"]);
        }

        [Fact]
        public void MODULO16_DivideByZero_Throws()
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = 42;
            ctx[".2"] = 0;
            Assert.Throws<IntercalException>(() => syslib64_native.DO_MODULO16(new ComponentCall(ctx)));
        }

        // ================================================================
        // MODULO32: :1 mod :2 -> :3
        // ================================================================

        [Fact]
        public void MODULO32_Basic()
        {
            var ctx = new ExecutionContext();
            ctx[":1"] = 1000001;
            ctx[":2"] = 1000;
            syslib64_native.DO_MODULO32(new ComponentCall(ctx));
            Assert.Equal(1UL, ctx[":3"]);
        }

        // ================================================================
        // MODULO64: ::1 mod ::2 -> ::3
        // ================================================================

        [Fact]
        public void MODULO64_Basic()
        {
            var ctx = new ExecutionContext();
            ctx["::1"] = 1000000000001UL;
            ctx["::2"] = 1000000UL;
            syslib64_native.DO_MODULO64(new ComponentCall(ctx));
            Assert.Equal(1UL, ctx["::3"]);
        }

        // ================================================================
        // RANDOM16: -> .1
        // ================================================================

        [Fact]
        public void RANDOM16_ProducesValue()
        {
            var ctx = new ExecutionContext();
            syslib64_native.DO_RANDOM16(new ComponentCall(ctx));
            // Result should be in 16-bit range
            Assert.True(ctx[".1"] <= 65535);
        }

        [Fact]
        public void RANDOM16_NotAlwaysSame()
        {
            // Call it several times, at least one should differ
            var ctx = new ExecutionContext();
            syslib64_native.DO_RANDOM16(new ComponentCall(ctx));
            ulong first = ctx[".1"];
            bool differs = false;
            for (int i = 0; i < 100; i++)
            {
                syslib64_native.DO_RANDOM16(new ComponentCall(ctx));
                if (ctx[".1"] != first) { differs = true; break; }
            }
            Assert.True(differs);
        }

        // ================================================================
        // RANDOM32: -> :1
        // ================================================================

        [Fact]
        public void RANDOM32_ProducesValue()
        {
            var ctx = new ExecutionContext();
            syslib64_native.DO_RANDOM32(new ComponentCall(ctx));
            Assert.True(ctx[":1"] <= uint.MaxValue);
        }

        [Fact]
        public void RANDOM32_NotAlwaysSame()
        {
            var ctx = new ExecutionContext();
            syslib64_native.DO_RANDOM32(new ComponentCall(ctx));
            ulong first = ctx[":1"];
            bool differs = false;
            for (int i = 0; i < 100; i++)
            {
                syslib64_native.DO_RANDOM32(new ComponentCall(ctx));
                if (ctx[":1"] != first) { differs = true; break; }
            }
            Assert.True(differs);
        }

        // ================================================================
        // RANDOM64: -> ::1
        // ================================================================

        [Fact]
        public void RANDOM64_ProducesValue()
        {
            var ctx = new ExecutionContext();
            syslib64_native.DO_RANDOM64(new ComponentCall(ctx));
            // Just verify it doesn't throw
            Assert.True(true);
        }

        [Fact]
        public void RANDOM64_NotAlwaysSame()
        {
            var ctx = new ExecutionContext();
            syslib64_native.DO_RANDOM64(new ComponentCall(ctx));
            ulong first = ctx["::1"];
            bool differs = false;
            for (int i = 0; i < 100; i++)
            {
                syslib64_native.DO_RANDOM64(new ComponentCall(ctx));
                if (ctx["::1"] != first) { differs = true; break; }
            }
            Assert.True(differs);
        }

        // ================================================================
        // Cross-validation: quotient * divisor + remainder = dividend
        // ================================================================

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(7, 3)]
        [InlineData(100, 7)]
        [InlineData(255, 16)]
        [InlineData(65535, 1)]
        [InlineData(65535, 256)]
        [InlineData(65535, 65535)]
        [InlineData(12345, 678)]
        public void DIVIDE16_QuotientTimesDiv_PlusRemainder_EqualsDividend(int a, int b)
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = (ulong)a;
            ctx[".2"] = (ulong)b;
            syslib64_native.DO_DIVIDE16(new ComponentCall(ctx));
            ulong quotient = ctx[".3"];
            ulong remainder = ctx[".4"];
            Assert.Equal((ulong)a, quotient * (ulong)b + remainder);
            Assert.True(remainder < (ulong)b);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1000000, 7)]
        [InlineData(4294967295, 65536)]
        [InlineData(123456789, 9999)]
        public void DIVIDE32_QuotientTimesDiv_PlusRemainder_EqualsDividend(uint a, uint b)
        {
            var ctx = new ExecutionContext();
            ctx[":1"] = a;
            ctx[":2"] = b;
            syslib64_native.DO_DIVIDE32(new ComponentCall(ctx));
            ulong quotient = ctx[":3"];
            ulong remainder = ctx[":4"];
            Assert.Equal((ulong)a, quotient * (ulong)b + remainder);
            Assert.True(remainder < (ulong)b);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1000000000000, 7)]
        [InlineData(18446744073709551615, 1000000)]
        public void DIVIDE64_QuotientTimesDiv_PlusRemainder_EqualsDividend(ulong a, ulong b)
        {
            var ctx = new ExecutionContext();
            ctx["::1"] = a;
            ctx["::2"] = b;
            syslib64_native.DO_DIVIDE64(new ComponentCall(ctx));
            ulong quotient = ctx["::3"];
            ulong remainder = ctx["::4"];
            Assert.Equal(a, quotient * b + remainder);
            Assert.True(remainder < b);
        }
    }
}
