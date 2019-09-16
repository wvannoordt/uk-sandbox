program m
	COMPLEX :: a = (1,5)
	INTEGER :: b = 1

! will compile but will not give the correct representation.

	call TestFunc(a) 
	call TestFunc(b)
	call T
end program m

include "testheader.h"

subroutine TestFunc(n)
	INTEGER :: n
	print *,"hello. the passed value is ", n
end subroutine TestFunc
