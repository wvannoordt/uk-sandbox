function [x] = get_spaced_values_reverse_buf(initial_x, end_x, initial_spacing, N, buffer)
x = get_spaced_values_buf(-end_x, -initial_x, initial_spacing, N, buffer);
x = -flipud(x);
end

