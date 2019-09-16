clear
clc
close all

lambda = 8;
dnm = 3;
p = rand(dnm,dnm);
d = zeros(dnm,dnm);
for i = 1:dnm
  p(:, i) = p(:,i) / norm(p(:,i));
  d(i,i) = lambda;
end
p
d
A = p*d*p^-1


newp = rand(dnm,dnm);
z = orth(p)
lams = diag(rand(dnm, 1))

B = newp * lams * newp^-1