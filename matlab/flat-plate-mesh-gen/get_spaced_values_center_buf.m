function [x] = get_spaced_values_center_buf(x_init, x_med, x_end, initial_spacing, N1, N2, buffer1, buffer2)
    x1 = get_spaced_values_reverse_buf(x_init, x_med, initial_spacing, N1, buffer1);
    x2 = get_spaced_values_buf(x_med, x_end, initial_spacing, N2, buffer2);
    x = join(x1, x2);
end