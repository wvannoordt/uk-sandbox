function [z] = join(x,y)
z = zeros(length(y) + length(x) - 1, 1);
for i = 1:length(x)
    z(i) = x(i);
end
for j = (length(x) + 1): length(z)
   z(j) = y(j - length(x) + 1); 
end
end

