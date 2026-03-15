namespace INTERCAL
{
    public enum TokenType
    {
        Label,      // (digits)
        Digits,     // 42
        Prefix,     // PLEASE, DO, N'T, NOT, %
        Gerund,     // READING OUT, WRITING IN, COMING FROM, ABSTAINING, etc.
        Statement,  // READ OUT, WRITE IN, COME FROM, ABSTAIN FROM, GIVE UP, NEXT, <-, etc.
        Separator,  // " ' + BY
        Var,        // . , ; : #
        UnaryOp,    // & v V ? | (rotate/stripper pole) - (flip/monkey bar)
        BinaryOp,   // $ ~
        Sub,        // SUB
        Word,       // any other [a-zA-Z]+ sequence
        Newline,    // \n
        EOF         // end of input
    }

    public class Token
    {
        public TokenType Type { get; }
        public string Value { get; }
        public int Index { get; }

        public Token(TokenType type, string value, int index)
        {
            Type = type;
            Value = value;
            Index = index;
        }

        public static readonly Token Empty = new Token(TokenType.EOF, "", -1);

        public override string ToString()
        {
            return $"{Type}: \"{Value}\"";
        }
    }
}
