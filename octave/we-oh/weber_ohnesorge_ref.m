clear
clc
close all


%engineering toolbox
rho_g = 0.1785

%wikipedia
rho_l = 973;

%Provided
D = 2e-3;


%http://www.inchem.org/documents/ehc/ehc/ehc112.htm
%(SI)
nu = 0.0000035;
mu = rho_l * nu;


%Surface tension:
%https://www.engineeringtoolbox.com/water-surface-tension-d_597.html
sigma_60 = 6.62e-2;
sigma_70 = 6.44e-2;

sigma = sigma_60 + (66.8 - 60)*(sigma_70 - sigma_60)/(70 - 60); %OF WATER, factor of 1/3rd (see Theofaunus)
sig_TBP = 0.3333333333*sigma;





%hyprephysics
c = 972;

%Mach 
U = 1.46 * c;
weber = D * rho_g * U^2 / sig_TBP
ohnesorge = mu / sqrt(rho_l*D*sig_TBP)

