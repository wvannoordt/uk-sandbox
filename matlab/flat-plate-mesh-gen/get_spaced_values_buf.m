function [x] = get_spaced_values_buf(initial_x, end_x, initial_spacing, N, bufferin)
N_no_buffer = N - bufferin + 1;
xbuf = initial_x + (0:(bufferin - 2))' * initial_spacing; 
xnobuf = get_spaced_values(initial_x + (bufferin-1)*initial_spacing, end_x, initial_spacing, N_no_buffer);
x = [xbuf;xnobuf];
end