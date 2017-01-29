(1900)  DO STASH .2 + .3
        DO .3 <- #65535
        DO (1903) NEXT
(1903)  PLEASE FORGET #1
        DO .2 <- #0
        PLEASE %50 IGNORE .2
        DO .2 <- #1
        PLEASE REMEMBER .2
        DO .1 <- !1$.2'~"#65535$#1"
        DO .3 <- .3~#65534
        DO (1902) NEXT
        DO (1903) NEXT
(1902)  DO (1904) NEXT
        DO RETRIEVE .2 + .3
        DO FORGET #1
        PLEASE RESUME #1
(1904)  PLEASE RESUME '?"!3~.3'~#1"$#1'~#3
