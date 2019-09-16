clear
clc
close all


%Provided
rho_air = 1.2;
T_C = 66.85;

%https://antoine.frostburg.edu/chem/senese/javascript/water-density.html
rho_water = 979.5433;

%Viscosity
%https://www.engineersedge.com/physics/water__density_viscosity_specific_weight_13146.htm#targetText=Water%20has%20a%20viscosity%20of,centipoise%20at%2020%20%C2%B0C.&targetText=where%20A%3D2.414%20%C3%97%2010%E2%88%925%20Pa&targetText=s%20%3B%20B%20%3D%20247.8%20K%20%3B,boiling%20point%20is%20listed%20below.
mu_60 = 0.4658e-3;
mu_70 = 0.4044e-3;
mu_water = mu_60 + (T_C - 60)*(mu_70 - mu_60)/(70 - 60);

%Surface tension: T = 340 K = 66.85C
%https://www.engineeringtoolbox.com/water-surface-tension-d_597.html
sigma_60 = 6.62e-2;
sigma_70 = 6.44e-2;

sigma = sigma_60 + (T_C - 60)*(sigma_70 - sigma_60)/(70 - 60);



T_K = 340;


%See http://hyperphysics.phy-astr.gsu.edu/hbase/Sound/souspe3.html#targetText=Therefore%20for%20air%20at%20T,result%20of%20their%20thermal%20energy.

%Mach 5
U_M5 = 5 * 20.05*sqrt(T_K);
D_bore_M5 = 20e-3;
weber_M5 = D_bore_M5 * rho_air * U_M5^2 / sigma
ohnesorge_M5 = mu_water / sqrt(rho_water*D_bore_M5*sigma)

%Mach 2
U_M2 = 2 * 20.05*sqrt(T_K);
D_bore_M2 = 65e-3;
weber_M2 = D_bore_M2 * rho_air * U_M2^2 / sigma
ohnesorge_M2 = mu_water / sqrt(rho_water*D_bore_M2*sigma)

