<%@Webservice Language="C#" class="IntercalService"%>

using System.Web.Services;
using INTERCAL.Runtime;


[WebService(Namespace="INTERCAL")]
class IntercalService : WebService
{
/*
(1000) .3 <- .1 plus .2, error exit on overflow 
(1009) .3 <- .1 plus .2 
.4 <- #1 if no overflow, else .4 <- #2 
(1010) .3 <- .1 minus .2, no action on overflow 
(1020) .1 <- .1 plus #1, no action on overflow 
(1030) .3 <- .1 times .2, error exit on overflow 
(1039) .3 <- .1 times .2 
.4 <- #1 if no overflow, else .4 <- #2 
(1040) .3 <- .1 divided by .2 
.3 <- #0 if .2 is #0 
(1050) .2 <- :1 divided by .1, error exit on overflow 
.2 <- #0 if .1 is #0

(1500) :3 <- :1 plus :2, error exit on overflow 
(1509) :3 <- :1 plus :2 
:4 <- #1 if no overflow, else :4 <- #2 
(1510) :3 <- :1 minus :2, no action on overflow 
(1520) :1 <- .1 concatenated with .2 
(1525) This subroutine is intended solely for internal use within the subroutine library and is therefore not described here. Its effect is to shift .3 logically 8 bits to the left. 
(1530) :1 <- .1 times .2 
(1540) :3 <- :1 times :2, error exit on overflow 
(1549) :3 <- :1 times :2 
:4 <- #1 if no overflow, else :4 <- #2 
(1550) :3 <- :1 divided by :2 
:3 <- #0 if :2 is #0

(1900) .1 <- uniform random no. from #0 to #65535 
(1910) .2 <- normal random no. from #0 to .1, with standard deviation .1 divided by #12 
}