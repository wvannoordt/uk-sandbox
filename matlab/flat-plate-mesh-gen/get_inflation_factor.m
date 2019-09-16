function [x] = get_inflation_factor(y0, yn, delta_y_init, N)
R = (yn - y0) / delta_y_init;
x = R^(1 / (N - 2));
epsilon = 100;
k = 0;
kmax = 10000;
while abs(epsilon) > 1e-7
   k = k+1; 
   if (k > kmax)
       msg = ['Could not find a root value: a = ', num2str(y0), ' b = ', num2str(yn), ' dx = ', num2str(delta_y_init), ' N = ', num2str(N)];
      error(msg) 
   end
   epsilon = x^(N - 1) - R*x + R - 1;
   x = x - (epsilon) / ((N-1)*x^(N - 2) - R);
end
end

