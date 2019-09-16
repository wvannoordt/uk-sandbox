function fout = f (x)
  fout = zeros(size(x));
  n = length(x);
  for i = 1:n
    if x(i) < 0.3
      fout(i) = 0;
    else if x(i) > 1.2
      fout(i) = 0;
    else
      fout(i) = 0.05*sin((pi * x(i) / 0.9) - (pi / 3))^4; 
    end
  end
end