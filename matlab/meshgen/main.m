clear
clc
close all
Nx = 197;
Ny = 99;

%x
lead_edge_spacing = 6e-3;
half_plate_point_count = 32;

leading_edge_location = 0;
middle_location = 0.75;
inlet_location = -15;
top = 5;

N_plate = 36;
N_offplate = round((Nx + 3 - 2*N_plate)/2);

dy_spacing = 5e-6;

x_inlet = get_spaced_values_reverse_buf(inlet_location, leading_edge_location, lead_edge_spacing, N_offplate, 5);
x_plate = get_spaced_values_buf(leading_edge_location, middle_location, lead_edge_spacing, N_plate, 5);
x_front = join(x_inlet, x_plate);
x_back = 2*middle_location - flipud(x_front);

x = join(x_front, x_back);
X = zeros(Ny, Nx);
Y = zeros(size(X));
for i = 1:Nx
    current_x = x(i);
    X(:, i) = current_x;
    bottom = shape(current_x);
    Y(:, i) = get_spaced_values_buf(bottom, top, dy_spacing, Ny, 6);
end
mesh(X, Y, zeros(size(X)))
view(0, 90)
pbaspect([max(max(X)) max(max(Y)) 1])

outputdata = zeros(Nx*Ny, 2);
k = 1;
for j = 1:Ny
   for i = 1:Nx
      outputdata(k, :) = [X(j, i), Y(j, i)];
      k = k + 1;
   end
end
csvwrite(strcat('./output/mesh', num2str(Nx), 'x', num2str(Ny),'.csv'), outputdata);



function [shape_out] = shape(x)
shape_out = 0;
if x > 0.3 && x < 1.2
    shape_out = 0.05*sin(pi*x / 0.9 - pi / 3)^4;
end
end
