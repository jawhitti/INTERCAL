using INTERCAL;
using Xunit;

namespace intercal.tests
{
    public class BoxScannerTests
    {
        // === Tokenizer: [] cat box prefix ===

        [Fact]
        public void Tokenizer_RecognizesCatBox()
        {
            var scanner = Scanner.CreateScanner("[]");
            Assert.Equal(TokenType.Var, scanner.Current.Type);
            Assert.Equal("[]", scanner.Current.Value);
        }

        [Fact]
        public void Tokenizer_CatBoxWithDigits()
        {
            var scanner = Scanner.CreateScanner("DO []1 <- #1 = #2");
            Assert.Equal("DO", scanner.Current.Value);
            scanner.MoveNext();
            Assert.Equal("[]", scanner.Current.Value);
            Assert.Equal(TokenType.Var, scanner.Current.Type);
            scanner.MoveNext();
            Assert.Equal("1", scanner.Current.Value);
            Assert.Equal(TokenType.Digits, scanner.Current.Type);
        }

        // === Tokenizer: = double worm operator ===

        [Fact]
        public void Tokenizer_RecognizesDoubleWorm()
        {
            var scanner = Scanner.CreateScanner("=");
            Assert.Equal(TokenType.BinaryOp, scanner.Current.Type);
            Assert.Equal("=", scanner.Current.Value);
        }

        [Fact]
        public void Tokenizer_DoubleWormInExpression()
        {
            var scanner = Scanner.CreateScanner("#1 = #2");
            Assert.Equal("#", scanner.Current.Value);
            scanner.MoveNext();
            Assert.Equal("1", scanner.Current.Value);
            scanner.MoveNext();
            Assert.Equal("=", scanner.Current.Value);
            Assert.Equal(TokenType.BinaryOp, scanner.Current.Type);
            scanner.MoveNext();
            Assert.Equal("#", scanner.Current.Value);
        }

        // === Tokenizer: FEED and PET statements ===

        [Fact]
        public void Tokenizer_RecognizesFeed()
        {
            var scanner = Scanner.CreateScanner("FEED");
            Assert.Equal(TokenType.Statement, scanner.Current.Type);
            Assert.Equal("FEED", scanner.Current.Value);
        }

        [Fact]
        public void Tokenizer_RecognizesPet()
        {
            var scanner = Scanner.CreateScanner("PET");
            Assert.Equal(TokenType.Statement, scanner.Current.Type);
            Assert.Equal("PET", scanner.Current.Value);
        }

        // === Tokenizer: BOXING and FEEDING gerunds ===

        [Fact]
        public void Tokenizer_RecognizesBoxingGerund()
        {
            var scanner = Scanner.CreateScanner("BOXING");
            Assert.Equal(TokenType.Gerund, scanner.Current.Type);
            Assert.Equal("BOXING", scanner.Current.Value);
        }

        [Fact]
        public void Tokenizer_RecognizesFeedingGerund()
        {
            var scanner = Scanner.CreateScanner("FEEDING");
            Assert.Equal(TokenType.Gerund, scanner.Current.Type);
            Assert.Equal("FEEDING", scanner.Current.Value);
        }

        [Fact]
        public void Tokenizer_RecognizesPettingGerund()
        {
            var scanner = Scanner.CreateScanner("PETTING");
            Assert.Equal(TokenType.Gerund, scanner.Current.Type);
            Assert.Equal("PETTING", scanner.Current.Value);
        }
    }
}
