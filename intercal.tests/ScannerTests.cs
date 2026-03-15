using INTERCAL;
using Xunit;

namespace intercal.tests
{
    public class ScannerTests
    {
        [Fact]
        public void CreateScanner_RecognizesLabel()
        {
            var scanner = Scanner.CreateScanner("(100)");
            Assert.Equal(TokenType.Label, scanner.Current.Type);
            Assert.Equal("(100)", scanner.Current.Value);
        }

        [Fact]
        public void CreateScanner_RecognizesDoPrefix()
        {
            var scanner = Scanner.CreateScanner("DO");
            Assert.Equal(TokenType.Prefix, scanner.Current.Type);
            Assert.Equal("DO", scanner.Current.Value);
        }

        [Fact]
        public void CreateScanner_RecognizesPleasePrefix()
        {
            var scanner = Scanner.CreateScanner("PLEASE");
            Assert.Equal(TokenType.Prefix, scanner.Current.Type);
            Assert.Equal("PLEASE", scanner.Current.Value);
        }

        [Fact]
        public void CreateScanner_RecognizesStatements()
        {
            var scanner = Scanner.CreateScanner("READ OUT");
            Assert.Equal(TokenType.Statement, scanner.Current.Type);
            Assert.Equal("READ OUT", scanner.Current.Value);
        }

        [Fact]
        public void CreateScanner_RecognizesGiveUp()
        {
            var scanner = Scanner.CreateScanner("GIVE UP");
            Assert.Equal(TokenType.Statement, scanner.Current.Type);
        }

        [Fact]
        public void CreateScanner_RecognizesVariableTypes()
        {
            var scanner = Scanner.CreateScanner(".");
            Assert.Equal(TokenType.Var, scanner.Current.Type);

            scanner = Scanner.CreateScanner(",");
            Assert.Equal(TokenType.Var, scanner.Current.Type);

            scanner = Scanner.CreateScanner(":");
            Assert.Equal(TokenType.Var, scanner.Current.Type);

            scanner = Scanner.CreateScanner(";");
            Assert.Equal(TokenType.Var, scanner.Current.Type);

            scanner = Scanner.CreateScanner("#");
            Assert.Equal(TokenType.Var, scanner.Current.Type);
        }

        [Fact]
        public void CreateScanner_RecognizesUnaryOperators()
        {
            var scanner = Scanner.CreateScanner("&");
            Assert.Equal(TokenType.UnaryOp, scanner.Current.Type);

            scanner = Scanner.CreateScanner("V");
            Assert.Equal(TokenType.UnaryOp, scanner.Current.Type);

            scanner = Scanner.CreateScanner("?");
            Assert.Equal(TokenType.UnaryOp, scanner.Current.Type);
        }

        [Fact]
        public void CreateScanner_RecognizesBinaryOperators()
        {
            var scanner = Scanner.CreateScanner("$");
            Assert.Equal(TokenType.BinaryOp, scanner.Current.Type);

            scanner = Scanner.CreateScanner("~");
            Assert.Equal(TokenType.BinaryOp, scanner.Current.Type);
        }

        [Fact]
        public void CreateScanner_RecognizesGerunds()
        {
            var scanner = Scanner.CreateScanner("NEXTING");
            Assert.Equal(TokenType.Gerund, scanner.Current.Type);

            scanner = Scanner.CreateScanner("CALCULATING");
            Assert.Equal(TokenType.Gerund, scanner.Current.Type);

            scanner = Scanner.CreateScanner("COMING FROM");
            Assert.Equal(TokenType.Gerund, scanner.Current.Type);
        }

        [Fact]
        public void MoveNext_AdvancesToken()
        {
            var scanner = Scanner.CreateScanner("DO GIVE UP");
            Assert.Equal("DO", scanner.Current.Value);
            scanner.MoveNext();
            Assert.Equal("GIVE UP", scanner.Current.Value);
        }

        [Fact]
        public void PeekNext_ShowsNextWithoutAdvancing()
        {
            var scanner = Scanner.CreateScanner("DO GIVE UP");
            Assert.Equal("GIVE UP", scanner.PeekNext.Value);
            Assert.Equal("DO", scanner.Current.Value);
        }

        [Fact]
        public void Scanner_SkipsNewlines_OnMoveNext()
        {
            // Newlines between statements are skipped when MoveNext advances past them
            var scanner = Scanner.CreateScanner("DO GIVE UP\nDO GIVE UP");
            Assert.Equal("DO", scanner.Current.Value);
            scanner.MoveNext(); // -> GIVE UP
            scanner.MoveNext(); // -> DO (skips \n)
            Assert.Equal("DO", scanner.Current.Value);
            scanner.MoveNext(); // -> GIVE UP
            Assert.Equal("GIVE UP", scanner.Current.Value);
        }

        [Fact]
        public void Scanner_RecognizesAssignment()
        {
            var scanner = Scanner.CreateScanner("<-");
            Assert.Equal(TokenType.Statement, scanner.Current.Type);
            Assert.Equal("<-", scanner.Current.Value);
        }

        [Fact]
        public void Scanner_RecognizesDigits()
        {
            var scanner = Scanner.CreateScanner("42");
            Assert.Equal(TokenType.Digits, scanner.Current.Type);
            Assert.Equal("42", scanner.Current.Value);
        }

        [Fact]
        public void ReadGroupValue_ThrowsOnWrongGroup()
        {
            var scanner = Scanner.CreateScanner("DO");
            Assert.ThrowsAny<Exception>(() => scanner.ReadGroupValue("label"));
        }

        [Fact]
        public void VerifyToken_ThrowsOnMismatch()
        {
            var scanner = Scanner.CreateScanner("DO");
            Assert.ThrowsAny<Exception>(() => scanner.VerifyToken("PLEASE"));
        }

        [Fact]
        public void VerifyToken_PassesOnMatch()
        {
            var scanner = Scanner.CreateScanner("DO");
            scanner.VerifyToken("DO"); // should not throw
        }

        // New tests for the hand-written tokenizer
        [Fact]
        public void Tokenizer_RecognizesNot()
        {
            var scanner = Scanner.CreateScanner("N'T");
            Assert.Equal(TokenType.Prefix, scanner.Current.Type);
            Assert.Equal("N'T", scanner.Current.Value);
        }

        [Fact]
        public void Tokenizer_RecognizesSub()
        {
            var scanner = Scanner.CreateScanner("SUB");
            Assert.Equal(TokenType.Sub, scanner.Current.Type);
        }

        [Fact]
        public void Tokenizer_VAsUnaryOp_WhenStandalone()
        {
            // Standalone v is a unary op
            var scanner = Scanner.CreateScanner("v");
            Assert.Equal(TokenType.UnaryOp, scanner.Current.Type);
        }

        [Fact]
        public void Tokenizer_VAsPartOfWord_WhenFollowedByLetters()
        {
            // "var" is a word, not a unary op
            var scanner = Scanner.CreateScanner("var");
            Assert.Equal(TokenType.Word, scanner.Current.Type);
            Assert.Equal("var", scanner.Current.Value);
        }

        [Fact]
        public void Tokenizer_TwoWordStatement_ComeFrom()
        {
            var scanner = Scanner.CreateScanner("COME FROM");
            Assert.Equal(TokenType.Statement, scanner.Current.Type);
            Assert.Equal("COME FROM", scanner.Current.Value);
        }

        [Fact]
        public void Tokenizer_TwoWordStatement_AbstainFrom()
        {
            var scanner = Scanner.CreateScanner("ABSTAIN FROM");
            Assert.Equal(TokenType.Statement, scanner.Current.Type);
            Assert.Equal("ABSTAIN FROM", scanner.Current.Value);
        }

        [Fact]
        public void Tokenizer_TwoWordGerund_ReadingOut()
        {
            var scanner = Scanner.CreateScanner("READING OUT");
            Assert.Equal(TokenType.Gerund, scanner.Current.Type);
            Assert.Equal("READING OUT", scanner.Current.Value);
        }

        [Fact]
        public void Tokenizer_PercentPrefix()
        {
            var scanner = Scanner.CreateScanner("%");
            Assert.Equal(TokenType.Prefix, scanner.Current.Type);
        }

        [Fact]
        public void Tokenizer_ByIsSeparator()
        {
            var scanner = Scanner.CreateScanner("BY");
            Assert.Equal(TokenType.Separator, scanner.Current.Type);
        }

        [Fact]
        public void Tokenizer_EOF_AtEnd()
        {
            var scanner = Scanner.CreateScanner("");
            Assert.Equal(TokenType.EOF, scanner.Current.Type);
        }

        [Fact]
        public void Tokenizer_IndexTracking()
        {
            var scanner = Scanner.CreateScanner("DO .1 <- #1");
            Assert.Equal(0, scanner.Current.Index);   // DO at 0
            scanner.MoveNext();
            Assert.Equal(3, scanner.Current.Index);    // . at 3
        }

        // Four-spot (::) and double-hybrid (;;) token tests
        [Fact]
        public void Tokenizer_RecognizesDoubleColon()
        {
            var scanner = Scanner.CreateScanner("::");
            Assert.Equal(TokenType.Var, scanner.Current.Type);
            Assert.Equal("::", scanner.Current.Value);
        }

        [Fact]
        public void Tokenizer_SingleColonStillWorks()
        {
            var scanner = Scanner.CreateScanner(":1");
            Assert.Equal(TokenType.Var, scanner.Current.Type);
            Assert.Equal(":", scanner.Current.Value);
        }

        [Fact]
        public void Tokenizer_RecognizesDoubleSemicolon()
        {
            var scanner = Scanner.CreateScanner(";;");
            Assert.Equal(TokenType.Var, scanner.Current.Type);
            Assert.Equal(";;", scanner.Current.Value);
        }

        [Fact]
        public void Tokenizer_SingleSemicolonStillWorks()
        {
            var scanner = Scanner.CreateScanner(";1");
            Assert.Equal(TokenType.Var, scanner.Current.Type);
            Assert.Equal(";", scanner.Current.Value);
        }

        [Fact]
        public void Tokenizer_RecognizesDoubleHash()
        {
            var scanner = Scanner.CreateScanner("##");
            Assert.Equal(TokenType.Var, scanner.Current.Type);
            Assert.Equal("##", scanner.Current.Value);
        }

        [Fact]
        public void Tokenizer_RecognizesQuadHash()
        {
            var scanner = Scanner.CreateScanner("####");
            Assert.Equal(TokenType.Var, scanner.Current.Type);
            Assert.Equal("####", scanner.Current.Value);
        }

        [Fact]
        public void Tokenizer_SingleHashStillWorks()
        {
            var scanner = Scanner.CreateScanner("#1");
            Assert.Equal(TokenType.Var, scanner.Current.Type);
            Assert.Equal("#", scanner.Current.Value);
        }

        [Fact]
        public void Tokenizer_FourSpotInStatement()
        {
            var scanner = Scanner.CreateScanner("DO ::1 <- ##100");
            Assert.Equal("DO", scanner.Current.Value);
            scanner.MoveNext();
            Assert.Equal("::", scanner.Current.Value);
            scanner.MoveNext();
            Assert.Equal("1", scanner.Current.Value);
            scanner.MoveNext();
            Assert.Equal("<-", scanner.Current.Value);
            scanner.MoveNext();
            Assert.Equal("##", scanner.Current.Value);
            scanner.MoveNext();
            Assert.Equal("100", scanner.Current.Value);
        }

        [Fact]
        public void Tokenizer_CompleteStatement()
        {
            var scanner = Scanner.CreateScanner("(10) DO .1 <- #42");
            Assert.Equal(TokenType.Label, scanner.Current.Type);
            Assert.Equal("(10)", scanner.Current.Value);
            scanner.MoveNext();
            Assert.Equal(TokenType.Prefix, scanner.Current.Type);
            Assert.Equal("DO", scanner.Current.Value);
            scanner.MoveNext();
            Assert.Equal(TokenType.Var, scanner.Current.Type);
            Assert.Equal(".", scanner.Current.Value);
            scanner.MoveNext();
            Assert.Equal(TokenType.Digits, scanner.Current.Type);
            Assert.Equal("1", scanner.Current.Value);
            scanner.MoveNext();
            Assert.Equal(TokenType.Statement, scanner.Current.Type);
            Assert.Equal("<-", scanner.Current.Value);
            scanner.MoveNext();
            Assert.Equal(TokenType.Var, scanner.Current.Type);
            Assert.Equal("#", scanner.Current.Value);
            scanner.MoveNext();
            Assert.Equal(TokenType.Digits, scanner.Current.Type);
            Assert.Equal("42", scanner.Current.Value);
        }

        // Mirror (|) and Invert (-) tokenization
        [Fact]
        public void Tokenizer_PipeIsUnaryOp()
        {
            var scanner = Scanner.CreateScanner("|");
            Assert.Equal(TokenType.UnaryOp, scanner.Current.Type);
            Assert.Equal("|", scanner.Current.Value);
        }

        [Fact]
        public void Tokenizer_DashIsUnaryOp()
        {
            var scanner = Scanner.CreateScanner("-");
            Assert.Equal(TokenType.UnaryOp, scanner.Current.Type);
            Assert.Equal("-", scanner.Current.Value);
        }

        [Fact]
        public void Tokenizer_StackedUnaryOps()
        {
            // -|? should tokenize as three consecutive UnaryOp tokens
            var scanner = Scanner.CreateScanner("-|?");
            Assert.Equal(TokenType.UnaryOp, scanner.Current.Type);
            Assert.Equal("-", scanner.Current.Value);
            scanner.MoveNext();
            Assert.Equal(TokenType.UnaryOp, scanner.Current.Type);
            Assert.Equal("|", scanner.Current.Value);
            scanner.MoveNext();
            Assert.Equal(TokenType.UnaryOp, scanner.Current.Type);
            Assert.Equal("?", scanner.Current.Value);
        }
    }
}
