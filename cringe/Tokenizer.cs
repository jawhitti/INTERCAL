using System.Collections.Generic;

namespace INTERCAL
{
    public class Tokenizer
    {
        private readonly string input;
        private int pos;
        private Token pending = null;

        // Two-word gerunds: first word -> second word
        static readonly Dictionary<string, string> TwoWordGerunds = new Dictionary<string, string>
        {
            { "READING", "OUT" },
            { "WRITING", "IN" },
            { "COMING", "FROM" }
        };

        // Two-word statements: first word -> second word
        static readonly Dictionary<string, string> TwoWordStatements = new Dictionary<string, string>
        {
            { "READ", "OUT" },
            { "WRITE", "IN" },
            { "COME", "FROM" },
            { "ABSTAIN", "FROM" },
            { "GIVE", "UP" }
        };

        // Single-word gerunds
        static readonly HashSet<string> SingleGerunds = new HashSet<string>
        {
            "ABSTAINING", "REINSTATING", "NEXTING", "STASHING",
            "RESUMING", "FORGETTING", "IGNORING", "REMEMBERING",
            "RETRIEVING", "CALCULATING",
            "BOXING", "FEEDING", "PETTING"
        };

        // Single-word statements
        static readonly HashSet<string> SingleStatements = new HashSet<string>
        {
            "REINSTATE", "NEXT", "STASH", "RESUME", "FORGET",
            "IGNORE", "REMEMBER", "RETRIEVE",
            "FEED", "PET"
        };

        // Prefixes
        static readonly HashSet<string> Prefixes = new HashSet<string>
        {
            "PLEASE", "DO", "NOT"
        };

        public Tokenizer(string input)
        {
            this.input = input ?? "";
            this.pos = 0;
        }

        public Token NextToken()
        {
            if (pending != null)
            {
                var t = pending;
                pending = null;
                return t;
            }

            // Skip whitespace (but not newlines)
            while (pos < input.Length && (input[pos] == ' ' || input[pos] == '\t' || input[pos] == '\r'))
                pos++;

            if (pos >= input.Length)
                return Token.Empty;

            int startPos = pos;
            char c = input[pos];

            // Newline
            if (c == '\n')
            {
                pos++;
                return new Token(TokenType.Newline, "\n", startPos);
            }

            // Label: (digits)
            if (c == '(')
            {
                int start = pos;
                pos++; // skip (
                if (pos < input.Length && IsDigit(input[pos]))
                {
                    while (pos < input.Length && IsDigit(input[pos]))
                        pos++;
                    if (pos < input.Length && input[pos] == ')')
                    {
                        pos++; // skip )
                        return new Token(TokenType.Label, input.Substring(start, pos - start), start);
                    }
                }
                // Not a valid label, reset and fall through
                pos = start;
            }

            // Digits
            if (IsDigit(c))
            {
                int start = pos;
                while (pos < input.Length && IsDigit(input[pos]))
                    pos++;
                return new Token(TokenType.Digits, input.Substring(start, pos - start), start);
            }

            // Arrow <-
            if (c == '<' && pos + 1 < input.Length && input[pos + 1] == '-')
            {
                pos += 2;
                return new Token(TokenType.Statement, "<-", startPos);
            }

            // Single-character tokens (with look-ahead for multi-char variants)
            switch (c)
            {
                case ':':
                    pos++;
                    if (pos < input.Length && input[pos] == ':')
                    {
                        pos++;
                        return new Token(TokenType.Var, "::", startPos);
                    }
                    return new Token(TokenType.Var, ":", startPos);

                case ';':
                    pos++;
                    if (pos < input.Length && input[pos] == ';')
                    {
                        pos++;
                        return new Token(TokenType.Var, ";;", startPos);
                    }
                    return new Token(TokenType.Var, ";", startPos);

                case '#':
                    pos++;
                    if (pos < input.Length && input[pos] == '#')
                    {
                        pos++;
                        if (pos + 1 < input.Length && input[pos] == '#' && input[pos + 1] == '#')
                        {
                            pos += 2;
                            return new Token(TokenType.Var, "####", startPos);
                        }
                        return new Token(TokenType.Var, "##", startPos);
                    }
                    return new Token(TokenType.Var, "#", startPos);

                case '[':
                    pos++;
                    if (pos < input.Length && input[pos] == ']')
                    {
                        pos++;
                        return new Token(TokenType.Var, "[]", startPos);
                    }
                    return new Token(TokenType.Word, c.ToString(), startPos);

                case '.': case ',':
                    pos++;
                    return new Token(TokenType.Var, c.ToString(), startPos);

                case '&': case '?': case '|': case '-':
                    pos++;
                    return new Token(TokenType.UnaryOp, c.ToString(), startPos);

                case '$': case '~': case '=':
                    pos++;
                    return new Token(TokenType.BinaryOp, c.ToString(), startPos);

                case '"': case '\'': case '+':
                    pos++;
                    return new Token(TokenType.Separator, c.ToString(), startPos);

                case '!':
                    // ! is shorthand for '. (spark + spot)
                    pos++;
                    pending = new Token(TokenType.Var, ".", startPos);
                    return new Token(TokenType.Separator, "'", startPos);

                case '%':
                    pos++;
                    return new Token(TokenType.Prefix, "%", startPos);
            }

            // v/V: unary op if standalone, otherwise start of a word
            if ((c == 'v' || c == 'V') && (pos + 1 >= input.Length || !IsLetter(input[pos + 1])))
            {
                pos++;
                return new Token(TokenType.UnaryOp, c.ToString(), startPos);
            }

            // Words and keywords
            if (IsLetter(c))
            {
                return ReadWordToken();
            }

            // Unknown character — skip it and produce a Word token
            pos++;
            return new Token(TokenType.Word, c.ToString(), startPos);
        }

        private Token ReadWordToken()
        {
            int start = pos;
            string word = ReadWord();

            // N'T is a special prefix
            if (word == "N" && pos < input.Length && input[pos] == '\'')
            {
                int savedPos = pos;
                pos++; // skip '
                if (pos < input.Length && input[pos] == 'T' && (pos + 1 >= input.Length || !IsLetter(input[pos + 1])))
                {
                    pos++; // skip T
                    return new Token(TokenType.Prefix, "N'T", start);
                }
                pos = savedPos; // not N'T, restore
            }

            // Check for two-word gerunds (must check before statements since
            // READING OUT could otherwise match READ as a statement start)
            if (TwoWordGerunds.TryGetValue(word, out string gerundSecond))
            {
                int savedPos = pos;
                SkipSpaces();
                if (pos < input.Length && IsLetter(input[pos]))
                {
                    int wordStart = pos;
                    string nextWord = ReadWord();
                    if (nextWord == gerundSecond)
                    {
                        return new Token(TokenType.Gerund, word + " " + nextWord, start);
                    }
                    pos = savedPos; // not a two-word gerund
                }
                else
                {
                    pos = savedPos;
                }
            }

            // Check for two-word statements
            if (TwoWordStatements.TryGetValue(word, out string stmtSecond))
            {
                int savedPos = pos;
                SkipSpaces();
                if (pos < input.Length && IsLetter(input[pos]))
                {
                    string nextWord = ReadWord();
                    if (nextWord == stmtSecond)
                    {
                        return new Token(TokenType.Statement, word + " " + nextWord, start);
                    }
                    pos = savedPos; // not a two-word statement
                }
                else
                {
                    pos = savedPos;
                }
            }

            // Single-word gerunds
            if (SingleGerunds.Contains(word))
                return new Token(TokenType.Gerund, word, start);

            // Single-word statements
            if (SingleStatements.Contains(word))
                return new Token(TokenType.Statement, word, start);

            // Prefixes
            if (Prefixes.Contains(word))
                return new Token(TokenType.Prefix, word, start);

            // SUB
            if (word == "SUB")
                return new Token(TokenType.Sub, word, start);

            // BY (separator)
            if (word == "BY")
                return new Token(TokenType.Separator, "BY", start);

            // Generic word
            return new Token(TokenType.Word, word, start);
        }

        private string ReadWord()
        {
            int start = pos;
            while (pos < input.Length && IsLetter(input[pos]))
                pos++;
            return input.Substring(start, pos - start);
        }

        private void SkipSpaces()
        {
            while (pos < input.Length && (input[pos] == ' ' || input[pos] == '\t' || input[pos] == '\r'))
                pos++;
        }

        private static bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        private static bool IsLetter(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }
    }
}
