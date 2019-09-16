#include<stdio.h>
#include<stdlib.h>

void main()
{
	int N = 3;
	
	float *x;
	x = (float*)malloc(N*sizeof(float));
	x[0] = 1.0f;
	x[1] = 2.0f;
	x[2] = 3.0f;
	
	printf("%ld\n", sizeof(x));
	
	free(x);
}
