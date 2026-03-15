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
[assembly: EntryPoint("(4702958889031696384)", "syslib64", "DO_ADD16")]
[assembly: EntryPoint("(4702958897554522112)", "syslib64", "DO_ADD32")]
[assembly: EntryPoint("(4702958910472978432)", "syslib64", "DO_ADD64")]
// MINUS (subtract)
[assembly: EntryPoint("(5569068542595249664)", "syslib64", "DO_MINUS16")]
[assembly: EntryPoint("(5569068542595379712)", "syslib64", "DO_MINUS32")]
[assembly: EntryPoint("(5569068542595576832)", "syslib64", "DO_MINUS64")]
// TIMES (multiply)
[assembly: EntryPoint("(6073470532629640704)", "syslib64", "DO_TIMES16")]
[assembly: EntryPoint("(6073470532629770752)", "syslib64", "DO_TIMES32")]
[assembly: EntryPoint("(6073470532629967872)", "syslib64", "DO_TIMES64")]
// DIVIDE
[assembly: EntryPoint("(4920558940556964150)", "syslib64", "DO_DIVIDE16")]
[assembly: EntryPoint("(4920558940556964658)", "syslib64", "DO_DIVIDE32")]
[assembly: EntryPoint("(4920558940556965428)", "syslib64", "DO_DIVIDE64")]
// MODULO
[assembly: EntryPoint("(5570746397223760182)", "syslib64", "DO_MODULO16")]
[assembly: EntryPoint("(5570746397223760690)", "syslib64", "DO_MODULO32")]
[assembly: EntryPoint("(5570746397223761460)", "syslib64", "DO_MODULO64")]
// RANDOM
[assembly: EntryPoint("(5927104639891484982)", "syslib64", "DO_RANDOM16")]
[assembly: EntryPoint("(5927104639891485490)", "syslib64", "DO_RANDOM32")]
[assembly: EntryPoint("(5927104639891486260)", "syslib64", "DO_RANDOM64")]

[Serializable]
[System.Diagnostics.DebuggerNonUserCode]
public class syslib64 : System.Object
{
    private static Random random = new Random();

    // ========================================================================
    // ADD: .1 + .2 → .3  /  :1 + :2 → :3  /  ::1 + ::2 → ::3
    // ========================================================================

    public static bool DO_ADD16(ExecutionContext context)
    {
        ushort a = (ushort)context[".1"];
        ushort b = (ushort)context[".2"];
        uint result = (uint)a + (uint)b;
        if (result > ushort.MaxValue)
            Lib.Fail("13 * (1999)  DOUBLE OR SINGLE PRECISION OVERFLOW");
        context[".3"] = result;
        return false;
    }

    public static bool DO_ADD32(ExecutionContext context)
    {
        uint a = (uint)context[":1"];
        uint b = (uint)context[":2"];
        ulong result = (ulong)a + (ulong)b;
        if (result > uint.MaxValue)
            Lib.Fail("13 * (1999)  DOUBLE OR SINGLE PRECISION OVERFLOW");
        context[":3"] = result;
        return false;
    }

    public static bool DO_ADD64(ExecutionContext context)
    {
        ulong a = context["::1"];
        ulong b = context["::2"];
        // Overflow check: if a + b wraps around
        ulong result = a + b;
        if (result < a)
            Lib.Fail("13 * (1999)  DOUBLE OR SINGLE PRECISION OVERFLOW");
        context["::3"] = result;
        return false;
    }

    // ========================================================================
    // MINUS: .1 - .2 → .3  /  :1 - :2 → :3  /  ::1 - ::2 → ::3
    // Uses the vertical mirror (ones' complement) conceptually:
    //   -x + 1 = two's complement, then add.
    // ========================================================================

    public static bool DO_MINUS16(ExecutionContext context)
    {
        ushort a = (ushort)context[".1"];
        ushort b = (ushort)context[".2"];
        if (b > a)
            Lib.Fail("13 * (1999)  DOUBLE OR SINGLE PRECISION OVERFLOW");
        context[".3"] = (ulong)(a - b);
        return false;
    }

    public static bool DO_MINUS32(ExecutionContext context)
    {
        uint a = (uint)context[":1"];
        uint b = (uint)context[":2"];
        if (b > a)
            Lib.Fail("13 * (1999)  DOUBLE OR SINGLE PRECISION OVERFLOW");
        context[":3"] = (ulong)(a - b);
        return false;
    }

    public static bool DO_MINUS64(ExecutionContext context)
    {
        ulong a = context["::1"];
        ulong b = context["::2"];
        if (b > a)
            Lib.Fail("13 * (1999)  DOUBLE OR SINGLE PRECISION OVERFLOW");
        context["::3"] = a - b;
        return false;
    }

    // ========================================================================
    // TIMES: .1 * .2 → .3  /  :1 * :2 → :3  /  ::1 * ::2 → ::3
    // ========================================================================

    public static bool DO_TIMES16(ExecutionContext context)
    {
        ushort a = (ushort)context[".1"];
        ushort b = (ushort)context[".2"];
        uint result = (uint)a * (uint)b;
        if (result > uint.MaxValue)
            Lib.Fail("13 * (1999)  DOUBLE OR SINGLE PRECISION OVERFLOW");
        // 16-bit multiply produces 32-bit result in :3
        context[":3"] = (ulong)result;
        return false;
    }

    public static bool DO_TIMES32(ExecutionContext context)
    {
        uint a = (uint)context[":1"];
        uint b = (uint)context[":2"];
        ulong result = (ulong)a * (ulong)b;
        // 32-bit multiply produces 64-bit result in ::3
        context["::3"] = result;
        return false;
    }

    public static bool DO_TIMES64(ExecutionContext context)
    {
        ulong a = context["::1"];
        ulong b = context["::2"];
        // Check for overflow: if a != 0 and result / a != b
        ulong result = a * b;
        if (a != 0 && result / a != b)
            Lib.Fail("13 * (1999)  DOUBLE OR SINGLE PRECISION OVERFLOW");
        context["::3"] = result;
        return false;
    }

    // ========================================================================
    // DIVIDE: .1 / .2 → .3 remainder .4
    //         :1 / :2 → :3 remainder :4
    //        ::1 / ::2 → ::3 remainder ::4
    // Conceptually uses horizontal mirror (|) for MSB-first iteration
    // and vertical mirror (-) for complement in subtraction.
    // ========================================================================

    public static bool DO_DIVIDE16(ExecutionContext context)
    {
        ushort a = (ushort)context[".1"];
        ushort b = (ushort)context[".2"];
        if (b == 0)
            Lib.Fail("13 * (1999)  DOUBLE OR SINGLE PRECISION OVERFLOW");
        context[".3"] = (ulong)(a / b);
        context[".4"] = (ulong)(a % b);
        return false;
    }

    public static bool DO_DIVIDE32(ExecutionContext context)
    {
        uint a = (uint)context[":1"];
        uint b = (uint)context[":2"];
        if (b == 0)
            Lib.Fail("13 * (1999)  DOUBLE OR SINGLE PRECISION OVERFLOW");
        context[":3"] = (ulong)(a / b);
        context[":4"] = (ulong)(a % b);
        return false;
    }

    public static bool DO_DIVIDE64(ExecutionContext context)
    {
        ulong a = context["::1"];
        ulong b = context["::2"];
        if (b == 0)
            Lib.Fail("13 * (1999)  DOUBLE OR SINGLE PRECISION OVERFLOW");
        context["::3"] = a / b;
        context["::4"] = a % b;
        return false;
    }

    // ========================================================================
    // MODULO: .1 mod .2 → .3  /  :1 mod :2 → :3  /  ::1 mod ::2 → ::3
    // ========================================================================

    public static bool DO_MODULO16(ExecutionContext context)
    {
        ushort a = (ushort)context[".1"];
        ushort b = (ushort)context[".2"];
        if (b == 0)
            Lib.Fail("13 * (1999)  DOUBLE OR SINGLE PRECISION OVERFLOW");
        context[".3"] = (ulong)(a % b);
        return false;
    }

    public static bool DO_MODULO32(ExecutionContext context)
    {
        uint a = (uint)context[":1"];
        uint b = (uint)context[":2"];
        if (b == 0)
            Lib.Fail("13 * (1999)  DOUBLE OR SINGLE PRECISION OVERFLOW");
        context[":3"] = (ulong)(a % b);
        return false;
    }

    public static bool DO_MODULO64(ExecutionContext context)
    {
        ulong a = context["::1"];
        ulong b = context["::2"];
        if (b == 0)
            Lib.Fail("13 * (1999)  DOUBLE OR SINGLE PRECISION OVERFLOW");
        context["::3"] = a % b;
        return false;
    }

    // ========================================================================
    // RANDOM: → .1  /  → :1  /  → ::1
    // Each bit independently set with 50% probability.
    // ========================================================================

    public static bool DO_RANDOM16(ExecutionContext context)
    {
        ushort result = 0;
        for (int i = 0; i < 16; i++)
        {
            if (random.Next(2) == 1)
                result |= (ushort)(1 << i);
        }
        context[".1"] = (ulong)result;
        return false;
    }

    public static bool DO_RANDOM32(ExecutionContext context)
    {
        uint result = 0;
        for (int i = 0; i < 32; i++)
        {
            if (random.Next(2) == 1)
                result |= 1u << i;
        }
        context[":1"] = (ulong)result;
        return false;
    }

    public static bool DO_RANDOM64(ExecutionContext context)
    {
        ulong result = 0;
        for (int i = 0; i < 64; i++)
        {
            if (random.Next(2) == 1)
                result |= 1UL << i;
        }
        context["::1"] = result;
        return false;
    }
}
