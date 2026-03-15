using INTERCAL;
using Xunit;
using System.IO;
using System.Linq;

namespace intercal.tests
{
    public class ProgramTests
    {
        private Program ParseSource(string source)
        {
            // Write source to a temp file, parse, then clean up
            string tmpFile = Path.GetTempFileName();
            try
            {
                File.WriteAllText(tmpFile, source);
                return Program.CreateFromFile(tmpFile);
            }
            finally
            {
                File.Delete(tmpFile);
            }
        }

        [Fact]
        public void Parse_GiveUp_ProducesOneStatement()
        {
            var p = ParseSource("DO GIVE UP");
            Assert.Equal(1, p.StatementCount);
        }

        [Fact]
        public void Parse_PleaseGiveUp_ProducesOneStatement()
        {
            var p = ParseSource("PLEASE GIVE UP");
            Assert.Equal(1, p.StatementCount);
        }

        [Fact]
        public void Parse_LabeledStatement()
        {
            var p = ParseSource("(1) DO GIVE UP");
            Assert.Equal(1, p.StatementCount);
        }

        [Fact]
        public void Parse_SimpleCalculation()
        {
            var p = ParseSource(
                "DO .1 <- #1\n" +
                "DO GIVE UP\n");
            Assert.Equal(2, p.StatementCount);
        }

        [Fact]
        public void Parse_ReadOut()
        {
            var p = ParseSource(
                "DO .1 <- #42\n" +
                "DO READ OUT .1\n" +
                "DO GIVE UP\n");
            Assert.Equal(3, p.StatementCount);
        }

        [Fact]
        public void Parse_Next()
        {
            var p = ParseSource(
                "(1) DO (2) NEXT\n" +
                "(2) DO GIVE UP\n");
            Assert.Equal(2, p.StatementCount);
        }

        [Fact]
        public void Parse_Abstain()
        {
            var p = ParseSource(
                "DO ABSTAIN FROM (1)\n" +
                "(1) DO .1 <- #1\n" +
                "DO GIVE UP\n");
            Assert.Equal(3, p.StatementCount);
        }

        [Fact]
        public void Parse_ComeFrom()
        {
            var p = ParseSource(
                "(1) DO .1 <- #1\n" +
                "DO COME FROM (1)\n" +
                "DO GIVE UP\n");
            Assert.Equal(3, p.StatementCount);
        }

        [Fact]
        public void Parse_StashRetrieve()
        {
            var p = ParseSource(
                "DO .1 <- #5\n" +
                "DO STASH .1\n" +
                "DO .1 <- #10\n" +
                "DO RETRIEVE .1\n" +
                "DO GIVE UP\n");
            Assert.Equal(5, p.StatementCount);
        }

        [Fact]
        public void Parse_NotStatement_IsDisabled()
        {
            var p = ParseSource(
                "DO NOT .1 <- #1\n" +
                "DO GIVE UP\n");
            Assert.Equal(2, p.StatementCount);
            Assert.False(p.Statements[0].bEnabled);
        }

        [Fact]
        public void Politeness_AllDo_IsZero()
        {
            var p = ParseSource(
                "DO .1 <- #1\n" +
                "DO .2 <- #2\n" +
                "DO .3 <- #3\n" +
                "DO .4 <- #4\n" +
                "DO GIVE UP\n");
            Assert.Equal(0, p.Politeness);
        }

        [Fact]
        public void Politeness_AllPlease_Is100()
        {
            var p = ParseSource(
                "PLEASE .1 <- #1\n" +
                "PLEASE .2 <- #2\n" +
                "PLEASE .3 <- #3\n" +
                "PLEASE .4 <- #4\n" +
                "PLEASE GIVE UP\n");
            Assert.Equal(100, p.Politeness);
        }

        [Fact]
        public void Politeness_MixedPoliteness()
        {
            // 1 PLEASE out of 4 statements = 25%
            var p = ParseSource(
                "PLEASE .1 <- #1\n" +
                "DO .2 <- #2\n" +
                "DO .3 <- #3\n" +
                "DO GIVE UP\n");
            Assert.Equal(25, p.Politeness);
        }

        [Fact]
        public void Parse_ForgetStatement()
        {
            var p = ParseSource(
                "DO FORGET #1\n" +
                "DO GIVE UP\n");
            Assert.Equal(2, p.StatementCount);
        }

        [Fact]
        public void Parse_IgnoreRemember()
        {
            var p = ParseSource(
                "DO IGNORE .1\n" +
                "DO REMEMBER .1\n" +
                "DO GIVE UP\n");
            Assert.Equal(3, p.StatementCount);
        }

        [Fact]
        public void Parse_DuplicateLabel_Throws()
        {
            Assert.Throws<CompilationException>(() =>
                ParseSource(
                    "(1) DO .1 <- #1\n" +
                    "(1) DO .2 <- #2\n" +
                    "DO GIVE UP\n"));
        }

        [Fact]
        public void Parse_AbstainFromGerund()
        {
            var p = ParseSource(
                "DO ABSTAIN FROM CALCULATING\n" +
                "DO .1 <- #1\n" +
                "DO GIVE UP\n");
            Assert.Equal(3, p.StatementCount);
        }

        [Fact]
        public void Parse_ReinstateGerund()
        {
            var p = ParseSource(
                "DO ABSTAIN FROM CALCULATING\n" +
                "DO REINSTATE CALCULATING\n" +
                "DO .1 <- #1\n" +
                "DO GIVE UP\n");
            Assert.Equal(4, p.StatementCount);
        }

        // Four-spot (::) parsing tests
        [Fact]
        public void Parse_FourSpotCalculation()
        {
            var p = ParseSource(
                "DO ::1 <- ####1\n" +
                "DO GIVE UP\n");
            Assert.Equal(2, p.StatementCount);
        }

        [Fact]
        public void Parse_FourSpotWithConstant()
        {
            var p = ParseSource(
                "DO ::1 <- ####1\n" +
                "DO ::2 <- #1$#2\n" +
                "DO GIVE UP\n");
            Assert.Equal(3, p.StatementCount);
        }

        // E533: "64 BITS SHOULD BE ENOUGH FOR ANYONE" — mingle two 64-bit values
        [Fact]
        public void Parse_MingleTwoFourSpots_ThrowsE533()
        {
            var ex = Assert.Throws<CompilationException>(() =>
                ParseSource(
                    "DO ::1 <- ####1\n" +
                    "DO ::2 <- ::1$::1\n" +
                    "DO GIVE UP\n"));
            Assert.Contains("E533", ex.Message);
            Assert.Contains("64 BITS SHOULD BE ENOUGH FOR ANYONE", ex.Message);
        }

        // Classic INTERCAL: mingle always truncates to 16 bits, even with two-spot operands
        [Fact]
        public void Parse_MingleMixedSpotTwoSpot_Succeeds()
        {
            var p = ParseSource(
                "DO .1 <- #1\n" +
                "DO :1 <- .1$:1\n" +
                "DO GIVE UP\n");
            Assert.Equal(3, p.StatementCount);
        }

        // Mirror operator (|) — horizontal mirror / staple
        [Fact]
        public void Parse_MirrorConstant()
        {
            var p = ParseSource(
                "DO .1 <- #|1\n" +
                "DO GIVE UP\n");
            Assert.Equal(2, p.StatementCount);
        }

        [Fact]
        public void Parse_MirrorVariable()
        {
            var p = ParseSource(
                "DO .1 <- #5\n" +
                "DO .2 <- .|1\n" +
                "DO GIVE UP\n");
            Assert.Equal(3, p.StatementCount);
        }

        [Fact]
        public void Parse_MirrorQuotedExpression()
        {
            var p = ParseSource(
                "DO .1 <- #5\n" +
                "DO .2 <- '|.1'\n" +
                "DO GIVE UP\n");
            Assert.Equal(3, p.StatementCount);
        }

        // Invert operator (-) — vertical mirror / worm
        [Fact]
        public void Parse_InvertConstant()
        {
            var p = ParseSource(
                "DO .1 <- #-1\n" +
                "DO GIVE UP\n");
            Assert.Equal(2, p.StatementCount);
        }

        [Fact]
        public void Parse_InvertVariable()
        {
            var p = ParseSource(
                "DO .1 <- #5\n" +
                "DO .2 <- .-1\n" +
                "DO GIVE UP\n");
            Assert.Equal(3, p.StatementCount);
        }

        // Stacked unary operators
        [Fact]
        public void Parse_StackedMirrorInvert_Constant()
        {
            // #-|5 means: mirror 5, then invert
            var p = ParseSource(
                "DO .1 <- #-|5\n" +
                "DO GIVE UP\n");
            Assert.Equal(2, p.StatementCount);
        }

        [Fact]
        public void Parse_StackedMirrorInvert_QuotedExpression()
        {
            // '-|.1' means: mirror .1, then invert
            var p = ParseSource(
                "DO .1 <- #5\n" +
                "DO .2 <- '-|.1'\n" +
                "DO GIVE UP\n");
            Assert.Equal(3, p.StatementCount);
        }

        [Fact]
        public void Parse_IdentityStack_MirrorInvertMirrorInvert()
        {
            // |-|- is identity (the # revelation)
            var p = ParseSource(
                "DO .1 <- #|-|-5\n" +
                "DO GIVE UP\n");
            Assert.Equal(2, p.StatementCount);
        }
    }
}
