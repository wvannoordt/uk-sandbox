program location
	implicit none
	INTEGER :: k, i, j
	INTEGER,PARAMETER :: matsize = 3;
	REAL :: arr(5) = [1.2, 4.5, 2.1, 6.7, 3.4]
	REAL :: arr2(matsize, matsize)
!	REAL*8 :: arr8(5) = [1.2, 4.5, 2.1, 6.7, 3.4] <- allocates 8 bytes (64 bits) for each real instead of 
	k = 9;
	print *, "k = ", k
	print *, "loc(k) = ", loc(k)
	print *, ""
	print *, "arr = ", arr
	do i = 1,5
		print *, "loc(arr(", i, ")) = ", loc(arr(i))
	end do
	do i = 1,matsize
		do j = 1, matsize
			arr2(i,j) = i + j
		end do
	end do
	print *, arr2
	print *, ""
	print *, "rows <_> columns"
	print *, ""
	do i = 1,matsize
		do j = 1, matsize
			print *,loc(arr2(i,j)), " <_> ", loc(arr2(j,i)) ! rows <_> columns
		end do
	end do
	print *, ""
	print *, "Matrix columns are consecutive in memory, while matrix rows are not."
end program location
