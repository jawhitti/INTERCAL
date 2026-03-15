using INTERCAL.Runtime;
using Xunit;

namespace intercal.tests
{
    public class LibTests
    {
        // Mingle interleaves the bits of two 16-bit values into a 32-bit result.
        // E.g. Mingle(0, 0xFFFF) puts all 1s in the even bit positions.
        [Fact]
        public void Mingle_ZeroWithZero_ReturnsZero()
        {
            Assert.Equal(0u, Lib.Mingle(0, 0));
        }

        [Fact]
        public void Mingle_OneWithZero_ReturnsTwo()
        {
            // bit 0 of 'men' goes to bit 1 of result
            Assert.Equal(2u, Lib.Mingle(1, 0));
        }

        [Fact]
        public void Mingle_ZeroWithOne_ReturnsOne()
        {
            // bit 0 of 'ladies' goes to bit 0 of result
            Assert.Equal(1u, Lib.Mingle(0, 1));
        }

        [Fact]
        public void Mingle_OneWithOne_ReturnsThree()
        {
            Assert.Equal(3u, Lib.Mingle(1, 1));
        }

        [Fact]
        public void Mingle_MaxValues_ReturnsAllOnes()
        {
            Assert.Equal(0xFFFFFFFF, Lib.Mingle(0xFFFF, 0xFFFF));
        }

        [Fact]
        public void Mingle_ZeroWithMax_ReturnsEvenBits()
        {
            // All 'ladies' bits set, all 'men' bits zero → every even bit set
            Assert.Equal(0x55555555u, Lib.Mingle(0, 0xFFFF));
        }

        [Fact]
        public void Mingle_MaxWithZero_ReturnsOddBits()
        {
            Assert.Equal(0xAAAAAAAAu, Lib.Mingle(0xFFFF, 0));
        }

        // Select extracts bits from 'a' at positions where 'b' has 1s,
        // then packs them into the low-order bits.
        [Fact]
        public void Select32_AllZeros_ReturnsZero()
        {
            Assert.Equal(0u, Lib.Select(0u, 0u));
        }

        [Fact]
        public void Select32_AllOnes_MaskAllOnes_ReturnsSame()
        {
            Assert.Equal(0xFFFFFFFFu, Lib.Select(0xFFFFFFFF, 0xFFFFFFFF));
        }

        [Fact]
        public void Select32_ExtractsCorrectBits()
        {
            // a = 0b1010, b = 0b1100 → selected bits are {1,0} → packed = 0b10 = 2
            Assert.Equal(2u, Lib.Select(0b1010u, 0b1100u));
        }

        [Fact]
        public void Select16_ExtractsCorrectBits()
        {
            Assert.Equal((ushort)2, Lib.Select((ushort)0b1010, (ushort)0b1100));
        }

        // Rotate shifts right by 1, wrapping the low bit to the high bit.
        [Fact]
        public void Rotate32_One_WrapsToHighBit()
        {
            Assert.Equal(0x80000000u, Lib.Rotate(1u));
        }

        [Fact]
        public void Rotate32_Two_ReturnsOne()
        {
            Assert.Equal(1u, Lib.Rotate(2u));
        }

        [Fact]
        public void Rotate32_Zero_ReturnsZero()
        {
            Assert.Equal(0u, Lib.Rotate(0u));
        }

        [Fact]
        public void Rotate16_One_WrapsToHighBit()
        {
            Assert.Equal((ushort)0x8000, Lib.Rotate((ushort)1));
        }

        [Fact]
        public void Rotate16_Two_ReturnsOne()
        {
            Assert.Equal((ushort)1, Lib.Rotate((ushort)2));
        }

        // Reverse reverses the bit order of a 16-bit value.
        [Fact]
        public void Reverse_One_Returns0x8000()
        {
            Assert.Equal((ushort)0x8000, Lib.Reverse(1));
        }

        [Fact]
        public void Reverse_Zero_ReturnsZero()
        {
            Assert.Equal((ushort)0, Lib.Reverse(0));
        }

        [Fact]
        public void Reverse_IsOwnInverse()
        {
            ushort val = 0x1234;
            Assert.Equal(val, Lib.Reverse(Lib.Reverse(val)));
        }

        // And = val & Rotate(val)
        [Fact]
        public void And_Zero_ReturnsZero()
        {
            Assert.Equal(0u, Lib.And(0));
        }

        [Fact]
        public void And_AllOnes32_ReturnsAllOnes()
        {
            Assert.Equal(0xFFFFFFFFu, Lib.And(0xFFFFFFFF));
        }

        // Or = val | Rotate(val)
        [Fact]
        public void Or_Zero_ReturnsZero()
        {
            Assert.Equal(0u, Lib.Or(0));
        }

        [Fact]
        public void Or_AllOnes32_ReturnsAllOnes()
        {
            Assert.Equal(0xFFFFFFFFu, Lib.Or(0xFFFFFFFF));
        }

        [Fact]
        public void Or_One_Expands()
        {
            // Or(1) = 1 | Rotate(1) = 1 | 0x8000 = 0x8001 (16-bit path)
            Assert.Equal(0x8001u, Lib.Or(1));
        }

        // Xor = val ^ Rotate(val)
        [Fact]
        public void Xor_Zero_ReturnsZero()
        {
            Assert.Equal(0u, Lib.Xor(0));
        }

        [Fact]
        public void Xor_AllOnes32_ReturnsZero()
        {
            // 0xFFFFFFFF ^ Rotate(0xFFFFFFFF) = 0xFFFFFFFF ^ 0xFFFFFFFF = 0
            Assert.Equal(0u, Lib.Xor(0xFFFFFFFF));
        }

        // Mingle32 interleaves the bits of two 32-bit values into a 64-bit result.
        [Fact]
        public void Mingle32_ZeroWithZero_ReturnsZero()
        {
            Assert.Equal(0UL, Lib.Mingle32(0, 0));
        }

        [Fact]
        public void Mingle32_OneWithZero_ReturnsTwo()
        {
            Assert.Equal(2UL, Lib.Mingle32(1, 0));
        }

        [Fact]
        public void Mingle32_ZeroWithOne_ReturnsOne()
        {
            Assert.Equal(1UL, Lib.Mingle32(0, 1));
        }

        [Fact]
        public void Mingle32_MaxValues_ReturnsAllOnes()
        {
            Assert.Equal(0xFFFFFFFFFFFFFFFFUL, Lib.Mingle32(0xFFFFFFFF, 0xFFFFFFFF));
        }

        [Fact]
        public void Mingle32_ZeroWithMax_ReturnsEvenBits()
        {
            Assert.Equal(0x5555555555555555UL, Lib.Mingle32(0, 0xFFFFFFFF));
        }

        [Fact]
        public void Mingle32_MaxWithZero_ReturnsOddBits()
        {
            Assert.Equal(0xAAAAAAAAAAAAAAAAUL, Lib.Mingle32(0xFFFFFFFF, 0));
        }

        // Select64
        [Fact]
        public void Select64_AllZeros_ReturnsZero()
        {
            Assert.Equal(0UL, Lib.Select(0UL, 0UL));
        }

        [Fact]
        public void Select64_AllOnes_MaskAllOnes_ReturnsSame()
        {
            Assert.Equal(ulong.MaxValue, Lib.Select(ulong.MaxValue, ulong.MaxValue));
        }

        [Fact]
        public void Select64_ExtractsCorrectBits()
        {
            Assert.Equal(2UL, Lib.Select(0b1010UL, 0b1100UL));
        }

        // Rotate64
        [Fact]
        public void Rotate64_One_WrapsToHighBit()
        {
            Assert.Equal(0x8000000000000000UL, Lib.Rotate(1UL));
        }

        [Fact]
        public void Rotate64_Two_ReturnsOne()
        {
            Assert.Equal(1UL, Lib.Rotate(2UL));
        }

        // 64-bit unary ops
        [Fact]
        public void And64_AllOnes_ReturnsAllOnes()
        {
            Assert.Equal(ulong.MaxValue, Lib.UnaryAnd64(ulong.MaxValue));
        }

        [Fact]
        public void Or64_AllOnes_ReturnsAllOnes()
        {
            Assert.Equal(ulong.MaxValue, Lib.UnaryOr64(ulong.MaxValue));
        }

        [Fact]
        public void Xor64_AllOnes_ReturnsZero()
        {
            Assert.Equal(0UL, Lib.UnaryXor64(ulong.MaxValue));
        }

        [Fact]
        public void And_Dispatches_To64Bit_ForLargeValues()
        {
            ulong val = 1UL << 40;
            Assert.Equal(Lib.UnaryAnd64(val), Lib.And(val));
        }

        [Fact]
        public void Or_Dispatches_To64Bit_ForLargeValues()
        {
            ulong val = 1UL << 40;
            Assert.Equal(Lib.UnaryOr64(val), Lib.Or(val));
        }

        [Fact]
        public void Xor_Dispatches_To64Bit_ForLargeValues()
        {
            ulong val = 1UL << 40;
            Assert.Equal(Lib.UnaryXor64(val), Lib.Xor(val));
        }

        [Fact]
        public void Fail_ThrowsIntercalException()
        {
            Assert.Throws<IntercalException>(() => Lib.Fail("test error"));
        }

        [Fact]
        public void Fail_IncludesErrorCode()
        {
            var ex = Assert.Throws<IntercalException>(() => Lib.Fail("E999"));
            Assert.Contains("E999", ex.Message);
        }

        // Rotate (| = stripper pole: reverse + invert)
        [Fact]
        public void Mirror16_One_ReturnsInvertedHighBit()
        {
            // reverse(1) = 0x8000, invert = 0x7FFF
            Assert.Equal((ushort)0x7FFF, Lib.Mirror16(1));
        }

        [Fact]
        public void Mirror16_HighBit_ReturnsInvertedOne()
        {
            // reverse(0x8000) = 1, invert = 0xFFFE
            Assert.Equal((ushort)0xFFFE, Lib.Mirror16(0x8000));
        }

        [Fact]
        public void Mirror16_Zero_ReturnsAllOnes()
        {
            // reverse(0) = 0, invert = 0xFFFF
            Assert.Equal((ushort)0xFFFF, Lib.Mirror16(0));
        }

        [Fact]
        public void Mirror16_AllOnes_ReturnsZero()
        {
            // reverse(0xFFFF) = 0xFFFF, invert = 0
            Assert.Equal((ushort)0, Lib.Mirror16(0xFFFF));
        }

        [Fact]
        public void Mirror16_IsOwnInverse()
        {
            // ||x == x for all values (reverse+invert twice = identity)
            ushort val = 0x1234;
            Assert.Equal(val, Lib.Mirror16(Lib.Mirror16(val)));
        }

        [Fact]
        public void Mirror32_One_ReturnsInvertedHighBit()
        {
            // reverse(1) = 0x80000000, invert = 0x7FFFFFFF
            Assert.Equal(0x7FFFFFFFu, Lib.Mirror32(1));
        }

        [Fact]
        public void Mirror32_IsOwnInverse()
        {
            uint val = 0xDEADBEEF;
            Assert.Equal(val, Lib.Mirror32(Lib.Mirror32(val)));
        }

        [Fact]
        public void Mirror64_One_ReturnsInvertedHighBit()
        {
            // reverse(1) = 0x8000..., invert = 0x7FFF...
            Assert.Equal(0x7FFFFFFFFFFFFFFFUL, Lib.Mirror64(1));
        }

        [Fact]
        public void Mirror64_IsOwnInverse()
        {
            ulong val = 0xCAFEBABEDEADBEEFUL;
            Assert.Equal(val, Lib.Mirror64(Lib.Mirror64(val)));
        }

        [Fact]
        public void Mirror_Dispatcher_16bit()
        {
            Assert.Equal((ulong)0x7FFF, Lib.Mirror(1));
        }

        [Fact]
        public void Mirror_Dispatcher_32bit()
        {
            // 0x10000 reversed in 32 bits = 0x00008000, inverted = 0xFFFF7FFF
            Assert.Equal((ulong)0xFFFF7FFFu, Lib.Mirror(0x10000));
        }

        // Pure reversal is now |- or -| (rotate then flip, or vice versa)
        [Fact]
        public void MirrorInvert_GivesPureReversal()
        {
            // |- (rotate then flip) reverses without inverting
            ushort val = 1;
            Assert.Equal((ushort)0x8000, Lib.Invert16(Lib.Mirror16(val)));
        }

        [Fact]
        public void InvertMirror_AlsoGivesPureReversal()
        {
            // -| (flip then rotate) also reverses without inverting (they commute)
            ushort val = 1;
            Assert.Equal((ushort)0x8000, Lib.Mirror16(Lib.Invert16(val)));
        }

        // Flip (- = monkey bar: 0↔1)
        [Fact]
        public void Invert16_Zero_ReturnsAllOnes()
        {
            Assert.Equal((ushort)0xFFFF, Lib.Invert16(0));
        }

        [Fact]
        public void Invert16_AllOnes_ReturnsZero()
        {
            Assert.Equal((ushort)0, Lib.Invert16(0xFFFF));
        }

        [Fact]
        public void Invert16_One_ReturnsComplement()
        {
            Assert.Equal((ushort)0xFFFE, Lib.Invert16(1));
        }

        [Fact]
        public void Invert16_IsOwnInverse()
        {
            ushort val = 0x1234;
            Assert.Equal(val, Lib.Invert16(Lib.Invert16(val)));
        }

        [Fact]
        public void Invert32_IsOwnInverse()
        {
            uint val = 0xDEADBEEF;
            Assert.Equal(val, Lib.Invert32(Lib.Invert32(val)));
        }

        [Fact]
        public void Invert64_IsOwnInverse()
        {
            ulong val = 0xCAFEBABEDEADBEEFUL;
            Assert.Equal(val, Lib.Invert64(Lib.Invert64(val)));
        }

        // Rotate and Flip commute
        [Fact]
        public void MirrorInvert_Commute_16bit()
        {
            ushort val = 0x1234;
            Assert.Equal(
                Lib.Mirror16(Lib.Invert16(val)),
                Lib.Invert16(Lib.Mirror16(val)));
        }

        // |-|- is identity (the # revelation: rotate-flip-rotate-flip)
        [Fact]
        public void MirrorInvertMirrorInvert_IsIdentity()
        {
            ushort val = 0xABCD;
            ushort result = Lib.Mirror16(Lib.Invert16(Lib.Mirror16(Lib.Invert16(val))));
            Assert.Equal(val, result);
        }

        // ================================================================
        // Array rotate (stripper pole) tests
        // Rotating an array treats it as one concatenated bit string,
        // reverses all bit positions, and inverts all bits.
        // This is equivalent to: reverse element order, rotate each element.
        //
        // Helper: simulates array rotation by concatenating 16-bit values
        // into a wider value, applying rotation, and splitting back.
        // ================================================================

        private static ushort[] RotateArray16(ushort[] elements)
        {
            int n = elements.Length;
            // Concatenate: first element = high bits
            // For 2 elements: combined = (e[0] << 16) | e[1] as a 32-bit value
            // For N elements: use a byte array and manual bit ops
            int totalBits = n * 16;

            // Build a big-endian bit array
            bool[] bits = new bool[totalBits];
            for (int e = 0; e < n; e++)
            {
                for (int b = 0; b < 16; b++)
                {
                    bits[e * 16 + (15 - b)] = (elements[e] & (1 << b)) != 0;
                }
            }

            // Reverse all bit positions AND invert (rotation)
            bool[] rotated = new bool[totalBits];
            for (int i = 0; i < totalBits; i++)
            {
                rotated[i] = !bits[totalBits - 1 - i];
            }

            // Split back into 16-bit elements
            ushort[] result = new ushort[n];
            for (int e = 0; e < n; e++)
            {
                ushort val = 0;
                for (int b = 0; b < 16; b++)
                {
                    if (rotated[e * 16 + (15 - b)])
                        val |= (ushort)(1 << b);
                }
                result[e] = val;
            }
            return result;
        }

        [Fact]
        public void RotateArray16_TwoElements_BasicCase()
        {
            // { 0x0004, 0x0001 } as concatenated 32-bit: 0x00040001
            // Reverse 32 bits: 0x80002000
            // Invert: 0x7FFFDFFF
            // Split: { 0x7FFF, 0xDFFF }
            var result = RotateArray16(new ushort[] { 0x0004, 0x0001 });
            Assert.Equal(new ushort[] { 0x7FFF, 0xDFFF }, result);
        }

        [Fact]
        public void RotateArray16_TwoElements_AllZeros()
        {
            // { 0, 0 } → reverse = { 0, 0 } → invert = { 0xFFFF, 0xFFFF }
            var result = RotateArray16(new ushort[] { 0, 0 });
            Assert.Equal(new ushort[] { 0xFFFF, 0xFFFF }, result);
        }

        [Fact]
        public void RotateArray16_TwoElements_AllOnes()
        {
            // { 0xFFFF, 0xFFFF } → reverse = same → invert = { 0, 0 }
            var result = RotateArray16(new ushort[] { 0xFFFF, 0xFFFF });
            Assert.Equal(new ushort[] { 0, 0 }, result);
        }

        [Fact]
        public void RotateArray16_IsOwnInverse()
        {
            // Rotating twice = identity
            ushort[] original = { 0x1234, 0x5678, 0x9ABC };
            var rotated = RotateArray16(original);
            var back = RotateArray16(rotated);
            Assert.Equal(original, back);
        }

        [Fact]
        public void RotateArray16_SingleElement_MatchesScalarMirror()
        {
            // A one-element array rotation should equal scalar Mirror16
            ushort val = 0x1234;
            var result = RotateArray16(new ushort[] { val });
            Assert.Equal(Lib.Mirror16(val), result[0]);
        }

        [Fact]
        public void RotateArray16_EqualsReverseOrderThenRotateEach()
        {
            // Rotating a concatenated bit string is equivalent to:
            // reverse element order, then rotate (|) each element
            ushort[] input = { 0x00FF, 0xFF00, 0x0F0F };
            var concatenated = RotateArray16(input);

            // Alternative: reverse order then Rotate each
            ushort[] alternative = new ushort[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                alternative[i] = Lib.Mirror16(input[input.Length - 1 - i]);
            }

            Assert.Equal(alternative, concatenated);
        }

        [Fact]
        public void RotateArray16_ThreeElements()
        {
            // { 1, 0, 0 }: bit 0 of element 0, all others zero
            // Concatenated 48 bits, reversed, inverted
            // Verify via round-trip: rotate twice = identity
            ushort[] input = { 1, 0, 0 };
            var rotated = RotateArray16(input);
            var back = RotateArray16(rotated);
            Assert.Equal(input, back);
            // And verify it's not the same as input (rotation changed it)
            Assert.NotEqual(input, rotated);
        }

        // ================================================================
        // Array flip (monkey bar) tests
        // Flipping over the monkey bar inverts each element in place
        // without changing element order.
        // ================================================================

        private static ushort[] InvertArray16(ushort[] elements)
        {
            ushort[] result = new ushort[elements.Length];
            for (int i = 0; i < elements.Length; i++)
            {
                result[i] = (ushort)~elements[i];
            }
            return result;
        }

        [Fact]
        public void InvertArray16_BasicCase()
        {
            // { 0100, 0001 } = { 4, 1 } → { 1011, 1110 } = { 0xFFFB, 0xFFFE }
            var result = InvertArray16(new ushort[] { 4, 1 });
            Assert.Equal(new ushort[] { 0xFFFB, 0xFFFE }, result);
        }

        [Fact]
        public void InvertArray16_AllZeros()
        {
            var result = InvertArray16(new ushort[] { 0, 0 });
            Assert.Equal(new ushort[] { 0xFFFF, 0xFFFF }, result);
        }

        [Fact]
        public void InvertArray16_AllOnes()
        {
            var result = InvertArray16(new ushort[] { 0xFFFF, 0xFFFF });
            Assert.Equal(new ushort[] { 0, 0 }, result);
        }

        [Fact]
        public void InvertArray16_IsOwnInverse()
        {
            ushort[] input = { 0x1234, 0x5678, 0x9ABC };
            var inverted = InvertArray16(input);
            var back = InvertArray16(inverted);
            Assert.Equal(input, back);
        }

        [Fact]
        public void InvertArray16_PreservesOrder()
        {
            // Invert doesn't reorder — first element stays first
            ushort[] input = { 1, 2, 3 };
            var result = InvertArray16(input);
            Assert.Equal(Lib.Invert16(1), result[0]);
            Assert.Equal(Lib.Invert16(2), result[1]);
            Assert.Equal(Lib.Invert16(3), result[2]);
        }

        [Fact]
        public void InvertArray16_SingleElement_MatchesScalarInvert()
        {
            ushort val = 0x1234;
            var result = InvertArray16(new ushort[] { val });
            Assert.Equal(Lib.Invert16(val), result[0]);
        }

        // ================================================================
        // Rotate and flip on arrays commute
        // ================================================================

        [Fact]
        public void ArrayRotateThenInvert_EqualsInvertThenRotate()
        {
            ushort[] input = { 0x00FF, 0xFF00, 0x0F0F };
            var rotateThenInvert = InvertArray16(RotateArray16(input));
            var invertThenRotate = RotateArray16(InvertArray16(input));
            Assert.Equal(rotateThenInvert, invertThenRotate);
        }

        [Fact]
        public void ArrayRotateInvertRotateInvert_IsIdentity()
        {
            // |-|- on arrays is still identity
            ushort[] input = { 0xDEAD, 0xBEEF, 0xCAFE };
            var result = RotateArray16(InvertArray16(RotateArray16(InvertArray16(input))));
            Assert.Equal(input, result);
        }
    }
}
