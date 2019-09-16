function [x] = get_spaced_values_reverse(initial_x, end_x, initial_spacing, N)
x = initial_x + (end_x - get_spaced_values(initial_x, end_x, initial_spacing, N));
x = flipud(x);
end

