program prog
	implicit none
	!print *,"all variable need to be declared!"; will give compile error declarations must come before
	INTEGER :: a = 1
	INTEGER :: b = 2
	INTEGER :: c = 0
	INTEGER :: noinit
	INTEGER :: manip = -100
	print *,"variables have been declared"
	call print_vars(a,b,c,manip)
	call test_intent(a, b, c, manip)
	print *,"variables have been manipulated"
	call print_vars(a, b, c, manip)
	call test_intent(a, b, noinit, manip)
	print *,"noinit=", noinit
end program prog

subroutine test_intent(a, b, c1, manip)
	implicit none
	INTEGER,INTENT(IN) :: a,b
	INTEGER,INTENT(OUT) :: c1
	INTEGER,INTENT(INOUT) :: manip
	!a = 0 will cause compile error
	c1 = a + b
	manip = a - b
end subroutine test_intent

subroutine print_vars(a, b, c, manip)
	implicit none
	INTEGER,INTENT(IN) :: a, b, c, manip
	print *,"variables:"
        print *,"a=", a
	print *, "b=", b 
	print *, "c=", c 
	print *, "manip=", manip
end subroutine print_vars
