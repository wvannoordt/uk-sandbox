function [x] = get_spaced_values(initial_x, end_x, initial_spacing, N)
k = get_inflation_factor(initial_x, end_x, initial_spacing, N);
x = zeros(N,1);
x(1) = initial_x;
delta = initial_spacing;
for i = 2:(N - 1)
    x(i) = x(i-1) + delta;
    delta = delta  * k;
end
x(N) = end_x;
end

