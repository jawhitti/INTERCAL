// i# System Library — the next generation INTERCAL syslib
//
// Labels are ASCII strings packed into 64-bit values:
//   "ADD16" = 0x4144443136000000, "DIVIDE64" = 0x4449564944453634, etc.
//
// Conventions:
//   16-bit:  .1, .2 inputs → .3 output (.4 for remainder)
//   32-bit:  :1, :2 inputs → :3 output (:4 for remainder)
//   64-bit: ::1, ::2 inputs → ::3 output (::4 for remainder)
//   RANDOM:  no inputs → .1 / :1 / ::1 output

using System;
using INTERCAL.Runtime;

// ADD
[assembly: EntryPoint("(4702958889031696384)", "syslib64_native", "DO_ADD16")]
[assembly: EntryPoint("(4702958897554522112)", "syslib64_native", "DO_ADD32")]
[assembly: EntryPoint("(4702958910472978432)", "syslib64_native", "DO_ADD64")]
// MINUS (subtract)
[assembly: EntryPoint("(5569068542595249664)", "syslib64_native", "DO_MINUS16")]
[assembly: EntryPoint("(5569068542595379712)", "syslib64_native", "DO_MINUS32")]
[assembly: EntryPoint("(5569068542595576832)", "syslib64_native", "DO_MINUS64")]
// TIMES (multiply)
[assembly: EntryPoint("(6073470532629640704)", "syslib64_native", "DO_TIMES16")]
[assembly: EntryPoint("(6073470532629770752)", "syslib64_native", "DO_TIMES32")]
[assembly: EntryPoint("(6073470532629967872)", "syslib64_native", "DO_TIMES64")]
// DIVIDE
[assembly: EntryPoint("(4920558940556964150)", "syslib64_native", "DO_DIVIDE16")]
[assembly: EntryPoint("(4920558940556964658)", "syslib64_native", "DO_DIVIDE32")]
[assembly: EntryPoint("(4920558940556965428)", "syslib64_native", "DO_DIVIDE64")]
// MODULO
[assembly: EntryPoint("(5570746397223760182)", "syslib64_native", "DO_MODULO16")]
[assembly: EntryPoint("(5570746397223760690)", "syslib64_native", "DO_MODULO32")]
[assembly: EntryPoint("(5570746397223761460)", "syslib64_native", "DO_MODULO64")]
// RANDOM
[assembly: EntryPoint("(5927104639891484982)", "syslib64_native", "DO_RANDOM16")]
[assembly: EntryPoint("(5927104639891485490)", "syslib64_native", "DO_RANDOM32")]
[assembly: EntryPoint("(5927104639891486260)", "syslib64_native", "DO_RANDOM64")]

[Serializable]
[System.Diagnostics.DebuggerNonUserCode]
public class syslib64_native : System.Object
{
    private static Random random = new Random();

    // ========================================================================
    // ADD: .1 + .2 → .3  /  :1 + :2 → :3  /  ::1 + ::2 → ::3
    // ========================================================================

    public static void DO_ADD16(ComponentCall call)
    {
        var context = call.Context;
        ushort a = (ushort)context[".1"];
        ushort b = (ushort)context[".2"];
        uint result = (uint)a + (uint)b;
        if (result > ushort.MaxValue)
            Lib.Fail("13 * (1999)  DOUBLE OR SINGLE PRECISION OVERFLOW");
        context[".3"] = result;
    }

    public static void DO_ADD32(ComponentCall call)
    {
        var context = call.Context;
        uint a = (uint)context[":1"];
        uint b = (uint)context[":2"];
        ulong result = (ulong)a + (ulong)b;
        if (result > uint.MaxValue)
            Lib.Fail("13 * (1999)  DOUBLE OR SINGLE PRECISION OVERFLOW");
        context[":3"] = result;
    }

    public static void DO_ADD64(ComponentCall call)
    {
        var context = call.Context;
        ulong a = context["::1"];
        ulong b = context["::2"];
        // Overflow check: if a + b wraps around
        ulong result = a + b;
        if (result < a)
            Lib.Fail("13 * (1999)  DOUBLE OR SINGLE PRECISION OVERFLOW");
        context["::3"] = result;
    }

    // ========================================================================
    // MINUS: .1 - .2 → .3  /  :1 - :2 → :3  /  ::1 - ::2 → ::3
    // Uses the vertical mirror (ones' complement) conceptually:
    //   -x + 1 = two's complement, then add.
    // ========================================================================

    public static void DO_MINUS16(ComponentCall call)
    {
        var context = call.Context;
        ushort a = (ushort)context[".1"];
        ushort b = (ushort)context[".2"];
        if (b > a)
            Lib.Fail("13 * (1999)  DOUBLE OR SINGLE PRECISION OVERFLOW");
        context[".3"] = (ulong)(a - b);
    }

    public static void DO_MINUS32(ComponentCall call)
    {
        var context = call.Context;
        uint a = (uint)context[":1"];
        uint b = (uint)context[":2"];
        if (b > a)
            Lib.Fail("13 * (1999)  DOUBLE OR SINGLE PRECISION OVERFLOW");
        context[":3"] = (ulong)(a - b);
    }

    public static void DO_MINUS64(ComponentCall call)
    {
        var context = call.Context;
        ulong a = context["::1"];
        ulong b = context["::2"];
        if (b > a)
            Lib.Fail("13 * (1999)  DOUBLE OR SINGLE PRECISION OVERFLOW");
        context["::3"] = a - b;
    }

    // ========================================================================
    // TIMES: .1 * .2 → .3  /  :1 * :2 → :3  /  ::1 * ::2 → ::3
    // ========================================================================

    public static void DO_TIMES16(ComponentCall call)
    {
        var context = call.Context;
        ushort a = (ushort)context[".1"];
        ushort b = (ushort)context[".2"];
        uint result = (uint)a * (uint)b;
        if (result > uint.MaxValue)
            Lib.Fail("13 * (1999)  DOUBLE OR SINGLE PRECISION OVERFLOW");
        // 16-bit multiply produces 32-bit result in :3
        context[":3"] = (ulong)result;
    }

    public static void DO_TIMES32(ComponentCall call)
    {
        var context = call.Context;
        uint a = (uint)context[":1"];
        uint b = (uint)context[":2"];
        ulong result = (ulong)a * (ulong)b;
        // 32-bit multiply produces 64-bit result in ::3
        context["::3"] = result;
    }

    public static void DO_TIMES64(ComponentCall call)
    {
        var context = call.Context;
        ulong a = context["::1"];
        ulong b = context["::2"];
        // Check for overflow: if a != 0 and result / a != b
        ulong result = a * b;
        if (a != 0 && result / a != b)
            Lib.Fail("13 * (1999)  DOUBLE OR SINGLE PRECISION OVERFLOW");
        context["::3"] = result;
    }

    // ========================================================================
    // DIVIDE: .1 / .2 → .3 remainder .4
    //         :1 / :2 → :3 remainder :4
    //        ::1 / ::2 → ::3 remainder ::4
    // Conceptually uses horizontal mirror (|) for MSB-first iteration
    // and vertical mirror (-) for complement in subtraction.
    // ========================================================================

    public static void DO_DIVIDE16(ComponentCall call)
    {
        var context = call.Context;
        ushort a = (ushort)context[".1"];
        ushort b = (ushort)context[".2"];
        if (b == 0)
            Lib.Fail("13 * (1999)  DOUBLE OR SINGLE PRECISION OVERFLOW");
        context[".3"] = (ulong)(a / b);
        context[".4"] = (ulong)(a % b);
    }

    public static void DO_DIVIDE32(ComponentCall call)
    {
        var context = call.Context;
        uint a = (uint)context[":1"];
        uint b = (uint)context[":2"];
        if (b == 0)
            Lib.Fail("13 * (1999)  DOUBLE OR SINGLE PRECISION OVERFLOW");
        context[":3"] = (ulong)(a / b);
        context[":4"] = (ulong)(a % b);
    }

    public static void DO_DIVIDE64(ComponentCall call)
    {
        var context = call.Context;
        ulong a = context["::1"];
        ulong b = context["::2"];
        if (b == 0)
            Lib.Fail("13 * (1999)  DOUBLE OR SINGLE PRECISION OVERFLOW");
        context["::3"] = a / b;
        context["::4"] = a % b;
    }

    // ========================================================================
    // MODULO: .1 mod .2 → .3  /  :1 mod :2 → :3  /  ::1 mod ::2 → ::3
    // ========================================================================

    public static void DO_MODULO16(ComponentCall call)
    {
        var context = call.Context;
        ushort a = (ushort)context[".1"];
        ushort b = (ushort)context[".2"];
        if (b == 0)
            Lib.Fail("13 * (1999)  DOUBLE OR SINGLE PRECISION OVERFLOW");
        context[".3"] = (ulong)(a % b);
    }

    public static void DO_MODULO32(ComponentCall call)
    {
        var context = call.Context;
        uint a = (uint)context[":1"];
        uint b = (uint)context[":2"];
        if (b == 0)
            Lib.Fail("13 * (1999)  DOUBLE OR SINGLE PRECISION OVERFLOW");
        context[":3"] = (ulong)(a % b);
    }

    public static void DO_MODULO64(ComponentCall call)
    {
        var context = call.Context;
        ulong a = context["::1"];
        ulong b = context["::2"];
        if (b == 0)
            Lib.Fail("13 * (1999)  DOUBLE OR SINGLE PRECISION OVERFLOW");
        context["::3"] = a % b;
    }

    // ========================================================================
    // RANDOM: → .1  /  → :1  /  → ::1
    // Each bit independently set with 50% probability.
    // ========================================================================

    public static void DO_RANDOM16(ComponentCall call)
    {
        ushort result = 0;
        for (int i = 0; i < 16; i++)
        {
            if (random.Next(2) == 1)
                result |= (ushort)(1 << i);
        }
        call.Context[".1"] = (ulong)result;
    }

    public static void DO_RANDOM32(ComponentCall call)
    {
        uint result = 0;
        for (int i = 0; i < 32; i++)
        {
            if (random.Next(2) == 1)
                result |= 1u << i;
        }
        call.Context[":1"] = (ulong)result;
    }

    public static void DO_RANDOM64(ComponentCall call)
    {
        ulong result = 0;
        for (int i = 0; i < 64; i++)
        {
            if (random.Next(2) == 1)
                result |= 1UL << i;
        }
        call.Context["::1"] = result;
    }
}
