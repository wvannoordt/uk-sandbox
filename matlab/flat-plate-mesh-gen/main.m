clear
clc
close all

more off

cells_per_block = 16;
blocks_x = 19;
blocks_y = 12;

Nx = 1 + cells_per_block*blocks_x;
Ny = 1 + cells_per_block*blocks_y;

x_l = 50;
x_r = Nx - x_l + 1;
dx_init = 2e-4;
bufx = 5;

dy_init = 1e-6;
bufy = 10;

xs = get_spaced_values_center_buf(-0.3, 0, 2, dx_init, x_l, x_r, bufx, bufx);
ys = get_spaced_values_buf(0, 1, dy_init, Ny, 10);

[X Y] = meshgrid(xs, ys);


if exist('X', 'var') && exist('Y', 'var')
    mesh(X, Y, zeros(size(X)))
    view(0, 90)
    pbaspect([max(max(X)) max(max(Y)) 1])
    tic
    p2dwrite('./output/mesh.x', X, Y);
    toc
else
    printf('Did not find mesh data.');
end