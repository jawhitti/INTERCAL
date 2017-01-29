	PLEASE WRITE IN .10
	DO .1 <- .10~#65534
	DO (1020) NEXT
	DO .10 <- !1$.10'~'#32767$#1'
	DO .1 <- .10
	DO .2 <- #10
	PLEASE DO (1030) NEXT
	DO .1 <- .3
	DO .2 <- #3
	DO (1040) NEXT
	DO .9 <- .3

	DO ,1 <- .9
	DO .1 <- .9
	PLEASE COME FROM (13)
	DO ,1 SUB .1 <- #2
	DO (11) NEXT
(12)	DO (2010) NEXT
(13)	PLEASE FORGET #2
(11)	DO (12) NEXT

	DO .15 <- #0
	DO .16 <- #0
	DO (100) NEXT
	DO .1 <- .10
	DO (2000) NEXT
	PLEASE COME FROM (23)
	DO .10 <- .1
	DO (100) NEXT
	PLEASE READ OUT .17
	DO .1 <- .10
	DO (21) NEXT
(22)	DO (2010) NEXT
(23)	PLEASE FORGET #2
(21)	DO (22) NEXT
	PLEASE FORGET #2
	PLEASE GIVE UP

(100)	DO .14 <- #0
	DO .12 <- .9
	PLEASE COME FROM (103)
	DO .1 <- .14
	DO .2 <- .12
	DO (1030) NEXT
	DO .14 <- .3
	DO .1 <- ,1 SUB .12
	DO .2 <- #10
	DO (1030) NEXT
	DO .1 <- .3
	DO .2 <- .14
	DO (1000) NEXT
	DO .1 <- !12$#0'~'#32767$#1'
	DO (2000) NEXT
	DO .2 <- .1
	DO .1 <- .3
	DO (2030) NEXT
	DO .14 <- .3
	DO ,1 SUB .12 <- .4
	DO .1 <- .12
	DO (101) NEXT
(102)	DO (2010) NEXT
	DO .12 <- .1
(103)	PLEASE FORGET #2
(101)	DO (102) NEXT
	DO .17 <- .16
	DO .1 <- .14
	DO .2 <- #10
	DO (2030) NEXT
	DO .1 <- .15
	DO .2 <- .3
	DO (1000) NEXT
	DO .16 <- .3
	DO .15 <- .4
	DO .5 <- '?.16$#10'~#85
	DO .5 <- "?!5~.5'$#2"~#3
	DO (104) NEXT
	DO .16 <- #0
	DO .1 <- .17
	DO (1020) NEXT
	DO .17 <- .1
	PLEASE RESUME #2
(104)   DO (1001) NEXT
	PLEASE RESUME #3


(2010)  PLEASE ABSTAIN FROM (2004)
(2000)  PLEASE STASH .2
        DO .2 <- #1
        DO (2001) NEXT
(2001)  PLEASE FORGET #1
        DO .1 <- '?.1$.2'~'#0$#65535'
        DO (2002) NEXT
        DO .2 <- !2$#0'~'#32767$#1'
        DO (2001) NEXT
(2003)  PLEASE RESUME "?!1~.2'$#1"~#3
(2002)  DO (2003) NEXT
        PLEASE RETRIEVE .2
(2004)	PLEASE RESUME #2
	PLEASE DO REINSTATE (2004)
	PLEASE RESUME '?"!1~.1'~#1"$#2'~#6
(2020)  PLEASE STASH .2 + .3
	DO (1021) NEXT
(2030)	DO STASH .1 + .5
	DO .3 <- #0
	DO .5 <- '?"!2~.2'~#1"$#1'~#3
	PLEASE DO (2031) NEXT
	DO .4 <- #1
	PLEASE DO (2033) NEXT
(2033)	DO FORGET #1
	DO .5 <- '?".2~#32768"$#2'~#3
	DO (2032) NEXT
	DO .2 <- !2$#0'~'#32767$#1'
	PLEASE DO .4 <- !4$#0'~'#32767$#1'
	DO (2033) NEXT
(2032)	DO (1001) NEXT
(2036)	PLEASE FORGET #1
        DO .5 <- '?.1$.2'~'#0$#65535'
        DO .5 <- '?"'&"!2~.5'~'"?'?.5~.5'$#32768"~"#0$#65535"'"$
                 ".5~.5"'~#1"$#2'~#3
	DO (2034) NEXT
	DO .5 <- .3
	DO (1010) NEXT
	PLEASE DO .1 <- .3
        DO .3 <- 'V.4$.5'~'#0$#65535'
	DO (2035) NEXT
(2034)	PLEASE DO (1001) NEXT
(2035)	DO FORGET #1
	DO .5 <- "?'.4~#1'$#2"~#3
	DO (2031) NEXT
	DO .2 <- .2~#65534
	DO .4 <- .4~#65534
	PLEASE DO (2036) NEXT
(2031)	DO (1001) NEXT
	PLEASE DO .4 <- .1
	PLEASE RETRIEVE .1 + .5
	PLEASE RESUME #2
