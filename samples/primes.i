	DO .10 <- #1
	PLEASE COME FROM (23)
	DO .11 <- !10$#1'~'#32767$#1'
	DO .12 <- #1
	PLEASE COME FROM (16)
	DO .13 <- !12$#1'~'#32767$#1'
	DO .1 <- .11
	DO .2 <- .13
	DO (2030) NEXT
	DO (11) NEXT
(15)	DO (13) NEXT
(13)	DO .3 <- "?!4~.4'$#2"~#3
	DO (14) NEXT
	PLEASE FORGET #1
	DO .1 <- .12
	DO (1020) NEXT
(16)	DO .12 <- .1
(12)	DO .3 <- '?.2$.3'~'#0$#65535'
	DO .3 <- '?"'&"!2~.3'~'"?'?.3~.3'$#32768"~"#0$#65535"'"$
                 ".3~.3"'~#1"$#2'~#3
(14)	PLEASE RESUME .3
(11)	DO (12) NEXT
	DO FORGET #1
	PLEASE READ OUT .11
	DO COME FROM (15)
	DO .1 <- .10
	DO (1020) NEXT
	DO .10 <- .1
(23)	DO (21) NEXT
(22)	PLEASE RESUME "?!10~#32768'$#2"~#3
(21)	DO (22) NEXT
	DO FORGET #1
	PLEASE GIVE UP

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
