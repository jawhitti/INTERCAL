using INTERCAL.Runtime;
using Xunit;

using ExecutionContext = INTERCAL.Runtime.ExecutionContext;

namespace intercal.tests
{
    /// <summary>
    /// Tests cross-component calls using testlib.dll compiled from testlib.i.
    /// Verifies that the state machine's external call path (NEXT to labels
    /// in another assembly) works correctly for NEXT, RESUME, FORGET, STASH,
    /// and RETRIEVE across component boundaries.
    /// </summary>
    public class ComponentTests
    {
        private testlib lib = new testlib();

        private void Call(int label, ExecutionContext ctx)
        {
            var method = typeof(testlib).GetMethod("DO_" + label);
            Assert.NotNull(method);
            var call = new ComponentCall(ctx);
            method.Invoke(lib, new object[] { call });
        }

        // ================================================================
        // Basic cross-component NEXT/RESUME
        // (3000): copy .1 to .3 and RESUME #1
        // ================================================================

        [Fact]
        public void CrossComponent_CopyDot1()
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = 42;
            Call(3000, ctx);
            Assert.Equal(42UL, ctx[".3"]);
        }

        [Fact]
        public void CrossComponent_CopyDot1_DifferentValue()
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = 65535;
            Call(3000, ctx);
            Assert.Equal(65535UL, ctx[".3"]);
        }

        // ================================================================
        // (3001): copy .2 to .3 and RESUME #1
        // Verifies .2 is correctly passed across component boundary
        // ================================================================

        [Fact]
        public void CrossComponent_CopyDot2()
        {
            var ctx = new ExecutionContext();
            ctx[".2"] = 99;
            Call(3001, ctx);
            Assert.Equal(99UL, ctx[".3"]);
        }

        // ================================================================
        // (3003): return constant 42 in .3
        // Verifies that the component can set variables visible to caller
        // ================================================================

        [Fact]
        public void CrossComponent_ReturnConstant()
        {
            var ctx = new ExecutionContext();
            Call(3003, ctx);
            Assert.Equal(42UL, ctx[".3"]);
        }

        // ================================================================
        // (3002): add .2 + 1 using old syslib (1000) internally
        // Tests: component calls ANOTHER component (syslib) internally
        // ================================================================

        [Fact]
        public void CrossComponent_AddViaSyslib()
        {
            var ctx = new ExecutionContext();
            ctx[".2"] = 10;
            Call(3002, ctx);
            Assert.Equal(11UL, ctx[".3"]);
        }

        [Fact]
        public void CrossComponent_AddViaSyslib_Zero()
        {
            var ctx = new ExecutionContext();
            ctx[".2"] = 0;
            Call(3002, ctx);
            Assert.Equal(1UL, ctx[".3"]);
        }

        // ================================================================
        // Variables set by caller are visible to component
        // ================================================================

        [Fact]
        public void CrossComponent_CallerVarsVisible()
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = 111;
            ctx[".2"] = 222;
            Call(3000, ctx);
            Assert.Equal(111UL, ctx[".3"]);
            // .2 should be unchanged
            Assert.Equal(222UL, ctx[".2"]);
        }

        // ================================================================
        // Variables set by component are visible to caller
        // ================================================================

        [Fact]
        public void CrossComponent_ComponentVarsVisible()
        {
            var ctx = new ExecutionContext();
            Call(3003, ctx);
            Assert.Equal(42UL, ctx[".3"]);
            // Call again — should overwrite
            ctx[".1"] = 7;
            Call(3000, ctx);
            Assert.Equal(7UL, ctx[".3"]);
        }

        // ================================================================
        // Sequential cross-component calls don't interfere
        // ================================================================

        [Fact]
        public void CrossComponent_SequentialCalls()
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = 10;
            Call(3000, ctx);
            Assert.Equal(10UL, ctx[".3"]);

            ctx[".2"] = 20;
            Call(3001, ctx);
            Assert.Equal(20UL, ctx[".3"]);

            Call(3003, ctx);
            Assert.Equal(42UL, ctx[".3"]);
        }

        // ================================================================
        // (3004): FORGET #1 then set .3 = 99
        // Tests that FORGET doesn't break the external call mechanism
        // ================================================================

        [Fact]
        public void CrossComponent_Forget()
        {
            var ctx = new ExecutionContext();
            Call(3004, ctx);
            Assert.Equal(99UL, ctx[".3"]);
        }

        [Fact]
        public void CrossComponent_ForgetAfterLocalNext()
        {
            // (3005) calls (3006) which FORGETs and RESUMEs.
            // .3 should be .1 (set by 3006), NOT 77 (set by dead code in 3005)
            var ctx = new ExecutionContext();
            ctx[".1"] = 55;
            Call(3005, ctx);
            Assert.Equal(55UL, ctx[".3"]);
        }

        // ================================================================
        // STASH/RETRIEVE across component boundary
        // (3002) stashes .1, modifies it, then retrieves
        // Caller's .1 should be unchanged after call
        // ================================================================

        [Fact]
        public void CrossComponent_StashRetrieve_PreservesCallerVars()
        {
            var ctx = new ExecutionContext();
            ctx[".1"] = 999;
            ctx[".2"] = 5;
            Call(3002, ctx);
            Assert.Equal(6UL, ctx[".3"]);
            // .1 should be restored by testlib's STASH/RETRIEVE
            Assert.Equal(999UL, ctx[".1"]);
        }
    }
}
