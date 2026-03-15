	PLEASE NOTE i# SYSTEM LIBRARY
	PLEASE NOTE LABELS ARE ASCII STRINGS PACKED INTO 64-BIT VALUES
	PLEASE NOTE E.G. ADD16 = 0x4144443136000000 = 4702958889031696384

	PLEASE NOTE ================================================================
	PLEASE NOTE ADD16: .1 + .2 -> .3
	PLEASE NOTE ================================================================
(4702958889031696384)	DO STASH .1 + .2
	DO (1000) NEXT
	DO RETRIEVE .1 + .2
	PLEASE RESUME #1

	PLEASE NOTE ================================================================
	PLEASE NOTE ADD32: :1 + :2 -> :3
	PLEASE NOTE ================================================================
(4702958897554522112)	DO STASH :1 + :2
	DO (1500) NEXT
	DO RETRIEVE :1 + :2
	PLEASE RESUME #1

	PLEASE NOTE ================================================================
	PLEASE NOTE ADD64: ::1 + ::2 -> ::3
	PLEASE NOTE SPLITS INTO HIGH AND LOW 32-BIT HALVES AND CHAINS CARRIES
	PLEASE NOTE ================================================================
(4702958910472978432)	DO STASH :1 + :2 + :3 + :4
	DO :1 <- ::1 ~ '#0$#65535$#0$#65535'
	DO :2 <- ::2 ~ '#0$#65535$#0$#65535'
	DO (1500) NEXT
	DO :4 <- :3
	DO :1 <- ::1 ~ '#65535$#0$#65535$#0'
	DO :2 <- ::2 ~ '#65535$#0$#65535$#0'
	DO (1500) NEXT
	PLEASE NOTE ADD CARRY FROM LOW HALF
	DO ::3 <- :3 $ :4
	DO RETRIEVE :1 + :2 + :3 + :4
	PLEASE RESUME #1

	PLEASE NOTE ================================================================
	PLEASE NOTE MINUS16: .1 - .2 -> .3
	PLEASE NOTE USES VERTICAL MIRROR (-) FOR ONES' COMPLEMENT
	PLEASE NOTE THEN ADDS 1 TO GET TWO'S COMPLEMENT AND ADDS
	PLEASE NOTE ================================================================
(5569068542595249664)	DO STASH .1 + .2
	DO .2 <- -.2
	PLEASE NOTE .2 IS NOW ONES' COMPLEMENT
	DO .1 <- .2
	DO .2 <- #1
	DO (1000) NEXT
	PLEASE NOTE .3 IS NOW TWO'S COMPLEMENT OF ORIGINAL .2
	DO .2 <- .3
	DO RETRIEVE .1
	DO (1000) NEXT
	DO RETRIEVE .2
	PLEASE RESUME #1

	PLEASE NOTE ================================================================
	PLEASE NOTE MINUS32: :1 - :2 -> :3
	PLEASE NOTE ================================================================
(5569068542595379712)	DO STASH :1 + :2 + .1 + .2
	PLEASE NOTE INVERT LOW HALF AND HIGH HALF SEPARATELY
	DO .1 <- :2 ~ #65535
	DO .1 <- -.1
	DO .2 <- :2 ~ '#65280$#65280'
	DO .2 <- -.2
	DO (1520) NEXT
	DO :2 <- :1
	PLEASE NOTE ADD 1 FOR TWO'S COMPLEMENT
	DO :1 <- #1
	DO (1500) NEXT
	DO :2 <- :3
	DO RETRIEVE :1
	DO (1500) NEXT
	DO RETRIEVE :2 + .1 + .2
	PLEASE RESUME #1

	PLEASE NOTE ================================================================
	PLEASE NOTE MINUS64: ::1 - ::2 -> ::3
	PLEASE NOTE ================================================================
(5569068542595576832)	DO STASH ::1 + ::2
	PLEASE NOTE INVERT ALL BITS THEN ADD 1
	DO ::2 <- -::2
	PLEASE NOTE TODO: NEED 64-BIT ADD INFRASTRUCTURE
	DO RETRIEVE ::1 + ::2
	PLEASE RESUME #1

	PLEASE NOTE ================================================================
	PLEASE NOTE TIMES16: .1 * .2 -> :3
	PLEASE NOTE RESULT IS 32-BIT
	PLEASE NOTE ================================================================
(6073470532629640704)	DO STASH .1 + .2
	DO (1040) NEXT
	DO :3 <- .3
	DO RETRIEVE .1 + .2
	PLEASE RESUME #1

	PLEASE NOTE ================================================================
	PLEASE NOTE TIMES32: :1 * :2 -> ::3
	PLEASE NOTE RESULT IS 64-BIT
	PLEASE NOTE ================================================================
(6073470532629770752)	DO STASH :1 + :2
	PLEASE NOTE SPLIT EACH 32-BIT VALUE INTO TWO 16-BIT HALVES
	PLEASE NOTE AND USE THE SCHOOLBOOK FOUR-MULTIPLY METHOD
	PLEASE NOTE (A*2^16 + B) * (C*2^16 + D) = AC*2^32 + (AD+BC)*2^16 + BD
	DO STASH .3
	DO .1 <- :1 ~ #65535
	DO .2 <- :2 ~ #65535
	DO (1040) NEXT
	DO STASH .3
	PLEASE NOTE .3 HAS LOW*LOW
	DO .1 <- :1 ~ '#65280$#65280'
	DO .2 <- :2 ~ #65535
	DO (1040) NEXT
	DO STASH .3
	PLEASE NOTE .3 HAS HIGH1*LOW2
	DO .1 <- :1 ~ #65535
	DO .2 <- :2 ~ '#65280$#65280'
	DO (1040) NEXT
	PLEASE NOTE .3 HAS LOW1*HIGH2
	PLEASE NOTE TODO: ASSEMBLE THE 64-BIT RESULT FROM PARTIAL PRODUCTS
	DO RETRIEVE .3 + .3 + .3
	DO RETRIEVE :1 + :2
	PLEASE RESUME #1

	PLEASE NOTE ================================================================
	PLEASE NOTE TIMES64: ::1 * ::2 -> ::3
	PLEASE NOTE ================================================================
(6073470532629967872)	DO STASH ::1 + ::2
	PLEASE NOTE TODO: IMPLEMENT USING 32-BIT MULTIPLY AND ASSEMBLY
	DO RETRIEVE ::1 + ::2
	PLEASE RESUME #1

	PLEASE NOTE ================================================================
	PLEASE NOTE DIVIDE16: .1 / .2 -> .3 REMAINDER .4
	PLEASE NOTE USES HORIZONTAL MIRROR (|) FOR MSB-FIRST BIT ITERATION
	PLEASE NOTE AND VERTICAL MIRROR (-) FOR COMPLEMENT IN SUBTRACTION
	PLEASE NOTE ================================================================
(4920558940556964150)	DO STASH .1 + .2 + .5 + .6
	PLEASE NOTE MIRROR THE DIVIDEND SO MSB IS IN BIT 0
	DO .1 <- |.1
	DO .3 <- #0
	DO .4 <- #0
	DO .6 <- #16
	DO (4920558940556964150) NEXT
(4920558940556964151)	DO FORGET #1
	PLEASE NOTE EXTRACT BOTTOM BIT OF MIRRORED DIVIDEND
	DO .5 <- .1 ~ #1
	PLEASE NOTE SHIFT DIVIDEND RIGHT (CONSUME THAT BIT)
	DO .1 <- '.1$#0' ~ '#65534$#0'
	PLEASE NOTE SHIFT REMAINDER LEFT AND ADD NEW BIT
	DO STASH .1 + .2
	DO .1 <- .4
	DO .2 <- .4
	DO (1000) NEXT
	DO .4 <- .3
	DO .1 <- .4
	DO .2 <- .5
	DO (1000) NEXT
	DO .4 <- .3
	DO RETRIEVE .1 + .2
	PLEASE NOTE COMPARE: IS REMAINDER >= DIVISOR?
	PLEASE NOTE TRY SUBTRACTING USING INVERT
	DO STASH .1 + .2
	DO .1 <- -.2
	DO .2 <- #1
	DO (1000) NEXT
	PLEASE NOTE .3 = -DIVISOR (TWO'S COMPLEMENT)
	DO .2 <- .3
	DO .1 <- .4
	DO (1000) NEXT
	PLEASE NOTE CHECK IF RESULT OVERFLOWED (MEANS .4 < .2)
	DO .5 <- .3 ~ '#65280$#65280'
	PLEASE NOTE TODO: PROPER OVERFLOW DETECTION
	DO RETRIEVE .1 + .2
	PLEASE NOTE SHIFT QUOTIENT LEFT
	DO STASH .1 + .2
	DO .1 <- .3
	DO .2 <- .3
	DO (1000) NEXT
	DO RETRIEVE .1 + .2
	PLEASE NOTE DECREMENT BIT COUNTER
	DO STASH .1 + .2
	DO .1 <- .6
	DO .2 <- #1
	DO (1009) NEXT
	DO .6 <- .3
	DO RETRIEVE .1 + .2
	PLEASE NOTE LOOP IF COUNTER > 0
	DO .5 <- "?'.6~.6'$#1"~#3
	DO .5 <- .5 ~ #1
	DO (4920558940556964152) NEXT
	DO (4920558940556964151) NEXT
(4920558940556964152)	DO .5 <- #0
	DO RESUME .5
	DO RETRIEVE .1 + .2 + .5 + .6
	PLEASE RESUME #1

	PLEASE NOTE ================================================================
	PLEASE NOTE DIVIDE32: :1 / :2 -> :3 REMAINDER :4
	PLEASE NOTE ================================================================
(4920558940556964658)	DO STASH :1 + :2
	PLEASE NOTE TODO: IMPLEMENT 32-BIT DIVISION USING MIRROR
	DO RETRIEVE :1 + :2
	PLEASE RESUME #1

	PLEASE NOTE ================================================================
	PLEASE NOTE DIVIDE64: ::1 / ::2 -> ::3 REMAINDER ::4
	PLEASE NOTE ================================================================
(4920558940556965428)	DO STASH ::1 + ::2
	PLEASE NOTE TODO: IMPLEMENT 64-BIT DIVISION USING MIRROR
	DO RETRIEVE ::1 + ::2
	PLEASE RESUME #1

	PLEASE NOTE ================================================================
	PLEASE NOTE MODULO16: .1 MOD .2 -> .3
	PLEASE NOTE CALLS DIVIDE16 AND RETURNS REMAINDER
	PLEASE NOTE ================================================================
(5570746397223760182)	DO STASH .1 + .2 + .4
	DO (4920558940556964150) NEXT
	DO .3 <- .4
	DO RETRIEVE .1 + .2 + .4
	PLEASE RESUME #1

	PLEASE NOTE ================================================================
	PLEASE NOTE MODULO32: :1 MOD :2 -> :3
	PLEASE NOTE ================================================================
(5570746397223760690)	DO STASH :1 + :2 + :4
	DO (4920558940556964658) NEXT
	DO :3 <- :4
	DO RETRIEVE :1 + :2 + :4
	PLEASE RESUME #1

	PLEASE NOTE ================================================================
	PLEASE NOTE MODULO64: ::1 MOD ::2 -> ::3
	PLEASE NOTE ================================================================
(5570746397223761460)	DO STASH ::1 + ::2 + ::4
	DO (4920558940556965428) NEXT
	DO ::3 <- ::4
	DO RETRIEVE ::1 + ::2 + ::4
	PLEASE RESUME #1

	PLEASE NOTE ================================================================
	PLEASE NOTE RANDOM16: -> .1
	PLEASE NOTE EACH BIT INDEPENDENTLY 50 PERCENT PROBABILITY
	PLEASE NOTE ================================================================
(5927104639891484982)	DO STASH .2 + .3 + .5
	DO .1 <- #0
	DO .2 <- #1
	DO (5927104639891484983) NEXT
(5927104639891484983)	DO FORGET #1
	DO %50 .1 <- 'V.1$.2'~'#0$#65535'
	DO .2 <- '.2$#0'~'#32767$#1'
	PLEASE DO .5 <- "?'.2~.2'$#1"~#3
	DO (5927104639891484984) NEXT
	DO (5927104639891484983) NEXT
(5927104639891484984)	DO (1001) NEXT
	DO RETRIEVE .2 + .3 + .5
	PLEASE RESUME #2

	PLEASE NOTE ================================================================
	PLEASE NOTE RANDOM32: -> :1
	PLEASE NOTE CALLS RANDOM16 TWICE AND MINGLES
	PLEASE NOTE ================================================================
(5927104639891485490)	DO STASH .1 + .2
	DO (5927104639891484982) NEXT
	DO .2 <- .1
	DO (5927104639891484982) NEXT
	DO (1520) NEXT
	DO RETRIEVE .1 + .2
	PLEASE RESUME #1

	PLEASE NOTE ================================================================
	PLEASE NOTE RANDOM64: -> ::1
	PLEASE NOTE CALLS RANDOM32 TWICE AND PACKS
	PLEASE NOTE ================================================================
(5927104639891486260)	DO STASH :1 + :2
	DO (5927104639891485490) NEXT
	DO :2 <- :1
	DO (5927104639891485490) NEXT
	DO ::1 <- :1 $ :2
	DO RETRIEVE :1 + :2
	PLEASE RESUME #1
