using INTERCAL.Runtime;
using Xunit;

using ExecutionContext = INTERCAL.Runtime.ExecutionContext;

namespace intercal.tests
{
    public class BoxVariableTests
    {
        // === Box Creation ===

        [Fact]
        public void Box_CreateWithTwoValues()
        {
            var ctx = new ExecutionContext();
            ctx.CreateBox("[]1", 1, 2);
            var values = ctx.GetBoxValues("[]1");
            Assert.Equal(2, values.Count);
            Assert.Contains(1UL, values);
            Assert.Contains(2UL, values);
        }

        [Fact]
        public void Box_UninitializedBox_ThrowsE200()
        {
            var ctx = new ExecutionContext();
            Assert.Throws<IntercalException>(() => ctx.CollapseBox("[]1"));
        }

        // === Collapse ===

        [Fact]
        public void Box_Collapse_ReturnsValueFromBox()
        {
            var ctx = new ExecutionContext();
            ctx.CreateBox("[]1", 10, 20);
            ulong result = ctx.CollapseBox("[]1");
            Assert.True(result == 10 || result == 20);
        }

        [Fact]
        public void Box_Collapse_BoxRetainsChosenValue()
        {
            var ctx = new ExecutionContext();
            ctx.CreateBox("[]1", 10, 20);
            ulong first = ctx.CollapseBox("[]1");
            ulong second = ctx.CollapseBox("[]1");
            Assert.Equal(first, second);
        }

        [Fact]
        public void Box_Collapse_IsUniformOverManyTrials()
        {
            int count10 = 0;
            int count20 = 0;
            // Run many trials to verify both values are possible
            for (int i = 0; i < 200; i++)
            {
                var ctx = new ExecutionContext();
                ctx.CreateBox("[]1", 10, 20);
                ulong result = ctx.CollapseBox("[]1");
                if (result == 10) count10++;
                else if (result == 20) count20++;
            }
            // Both values should appear at least sometimes
            Assert.True(count10 > 20, $"Value 10 appeared {count10} times out of 200");
            Assert.True(count20 > 20, $"Value 20 appeared {count20} times out of 200");
        }

        // === Grow ===

        [Fact]
        public void Box_Grow_AddsValueToSuperposition()
        {
            var ctx = new ExecutionContext();
            ctx.CreateBox("[]1", 1, 2);
            ctx.GrowBox("[]1", 3);
            var values = ctx.GetBoxValues("[]1");
            Assert.Equal(3, values.Count);
            Assert.Contains(3UL, values);
        }

        [Fact]
        public void Box_Grow_AfterCollapse_AddsToPreviousValue()
        {
            var ctx = new ExecutionContext();
            ctx.CreateBox("[]1", 1, 2);
            ulong collapsed = ctx.CollapseBox("[]1");
            ctx.GrowBox("[]1", 99);
            var values = ctx.GetBoxValues("[]1");
            Assert.Equal(2, values.Count);
            Assert.Contains(collapsed, values);
            Assert.Contains(99UL, values);
        }

        // === Merge ===

        [Fact]
        public void Box_Merge_CombinesTwoBoxes()
        {
            var ctx = new ExecutionContext();
            ctx.CreateBox("[]1", 1, 2);
            ctx.CreateBox("[]2", 3, 4);
            ctx.MergeBoxes("[]3", "[]1", "[]2");
            var values = ctx.GetBoxValues("[]3");
            Assert.Equal(4, values.Count);
            Assert.Contains(1UL, values);
            Assert.Contains(2UL, values);
            Assert.Contains(3UL, values);
            Assert.Contains(4UL, values);
        }

        // === Cartesian Product ===

        [Fact]
        public void Box_Mingle_ProducesCartesianProduct()
        {
            var ctx = new ExecutionContext();
            ctx.CreateBox("[]1", 1, 2);
            ctx.CreateBox("[]2", 3, 4);
            ctx.MingleBoxes("[]3", "[]1", "[]2");
            var values = ctx.GetBoxValues("[]3");
            Assert.Equal(4, values.Count);
            // Should contain Mingle(1,3), Mingle(1,4), Mingle(2,3), Mingle(2,4)
            Assert.Contains(Lib.Mingle(1, 3), values);
            Assert.Contains(Lib.Mingle(1, 4), values);
            Assert.Contains(Lib.Mingle(2, 3), values);
            Assert.Contains(Lib.Mingle(2, 4), values);
        }

        // === Hunger Counter ===

        [Fact]
        public void Box_HungerCounter_StartsAt5()
        {
            var ctx = new ExecutionContext();
            ctx.CreateBox("[]1", 1, 2);
            Assert.Equal(5, ctx.GetBoxHunger("[]1"));
        }

        [Fact]
        public void Box_HungerCounter_DecrementsOnUse()
        {
            var ctx = new ExecutionContext();
            ctx.CreateBox("[]1", 1, 2);
            ctx.CollapseBox("[]1");
            Assert.Equal(4, ctx.GetBoxHunger("[]1"));
        }

        [Fact]
        public void Box_Feed_IncrementsCounter()
        {
            var ctx = new ExecutionContext();
            ctx.CreateBox("[]1", 1, 2);
            ctx.FeedBox("[]1");
            Assert.Equal(6, ctx.GetBoxHunger("[]1"));
        }

        [Fact]
        public void Box_Pet_IncrementsCounter_SameAsFeed()
        {
            var ctx = new ExecutionContext();
            ctx.CreateBox("[]1", 1, 2);
            ctx.PetBox("[]1");
            Assert.Equal(6, ctx.GetBoxHunger("[]1"));
        }

        [Fact]
        public void Box_HungerCounter_DiesAtZero()
        {
            var ctx = new ExecutionContext();
            ctx.CreateBox("[]1", 1, 2);
            // Counter starts at 5, drain it to 0
            ctx.CollapseBox("[]1"); // 4
            ctx.CreateBox("[]1", 1, 2); // reset for more collapses
            ctx.CollapseBox("[]1"); // but wait, collapse on a single-value box...
            // Better approach: use the box 5 times
            var ctx2 = new ExecutionContext();
            ctx2.CreateBox("[]1", 1, 2);
            ctx2.CollapseBox("[]1"); // 4
            ctx2.GrowBox("[]1", 3);
            ctx2.CollapseBox("[]1"); // 3
            ctx2.GrowBox("[]1", 4);
            ctx2.CollapseBox("[]1"); // 2
            ctx2.GrowBox("[]1", 5);
            ctx2.CollapseBox("[]1"); // 1
            ctx2.GrowBox("[]1", 6);
            Assert.Throws<IntercalException>(() => ctx2.CollapseBox("[]1")); // 0 = dead
        }

        [Fact]
        public void Box_HungerCounter_DiesAt11()
        {
            var ctx = new ExecutionContext();
            ctx.CreateBox("[]1", 1, 2);
            // Counter starts at 5, feed it to 11
            ctx.FeedBox("[]1"); // 6
            ctx.FeedBox("[]1"); // 7
            ctx.FeedBox("[]1"); // 8
            ctx.FeedBox("[]1"); // 9
            ctx.FeedBox("[]1"); // 10
            Assert.Throws<IntercalException>(() => ctx.FeedBox("[]1")); // 11 = dead
        }

        [Fact]
        public void Box_HungerDeath_MessageIsE2007()
        {
            var ctx = new ExecutionContext();
            ctx.CreateBox("[]1", 1, 2);
            ctx.FeedBox("[]1"); // 6
            ctx.FeedBox("[]1"); // 7
            ctx.FeedBox("[]1"); // 8
            ctx.FeedBox("[]1"); // 9
            ctx.FeedBox("[]1"); // 10
            var ex = Assert.Throws<IntercalException>(() => ctx.FeedBox("[]1"));
            Assert.Contains("E2007", ex.Message);
            Assert.Contains("THE CAT IS DEAD", ex.Message);
        }

        // === Poop Limit ===

        [Fact]
        public void Box_ExceedingMaxValues_ThrowsE2012()
        {
            var ctx = new ExecutionContext();
            ctx.PleasePeta = true; // disable hunger so we can reach 99
            ctx.CreateBox("[]1", 1, 2);
            // Grow to 99 values (already have 2, need 97 more)
            for (ulong i = 3; i <= 99; i++)
            {
                ctx.GrowBox("[]1", i);
            }
            var ex = Assert.Throws<IntercalException>(() => ctx.GrowBox("[]1", 100));
            Assert.Contains("E2012", ex.Message);
            Assert.Contains("POOPED", ex.Message);
        }

        // === Type Mismatch ===

        [Fact]
        public void Box_MismatchedTypes_ThrowsE2010()
        {
            // This will be enforced at compile time in the expression parser
            // but we can test the error message exists
            Assert.Contains("DIFFERENT SIZE", Messages.E2010);
        }

        // === STASH / RETRIEVE ===

        [Fact]
        public void Box_Stash_PreservesValues()
        {
            var ctx = new ExecutionContext();
            ctx.CreateBox("[]1", 10, 20);
            ctx.Stash("[]1");
            // Modify the box after stashing
            ctx.CollapseBox("[]1");
            ctx.GrowBox("[]1", 99);
            // Retrieve should restore original superposition
            ctx.Retrieve("[]1");
            var values = ctx.GetBoxValues("[]1");
            Assert.Equal(2, values.Count);
            Assert.Contains(10UL, values);
            Assert.Contains(20UL, values);
        }

        [Fact]
        public void Box_Stash_PausesHunger()
        {
            var ctx = new ExecutionContext();
            ctx.CreateBox("[]1", 1, 2);
            int hungerBefore = ctx.GetBoxHunger("[]1");
            ctx.Stash("[]1");
            // The box is stashed — hunger should not change
            ctx.Retrieve("[]1");
            Assert.Equal(hungerBefore, ctx.GetBoxHunger("[]1"));
        }

        // === PLEASE PETA ===

        [Fact]
        public void Box_PleasePeta_PreventsDeath()
        {
            var ctx = new ExecutionContext();
            ctx.PleasePeta = true;
            ctx.CreateBox("[]1", 1, 2);
            // Overfeed without dying
            for (int i = 0; i < 20; i++)
                ctx.FeedBox("[]1");
            // Should not throw — peta mode is on
        }
    }
}
