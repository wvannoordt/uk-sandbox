program main

INTEGER :: x = 1;
INTEGER :: y = 2;

INTEGER :: a = 5;
INTEGER :: b = 2;

!"merge" is the fortran equivalent of the ternary operator.
print *,merge("x > y", "x < y", x.ge.y)
print *,merge("a > b", "a < b", a.ge.b)

end program main
