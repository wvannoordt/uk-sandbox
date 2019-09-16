function [fail_out] = p2dwrite_deprecated(filename, X, Y)
  [ny, nx] = size(X);
  [ny2, nx2] = size(Y);
  if ~(ny == ny2) || ~(nx == nx2)
    msg = ['Unequal matrix sizes: X is ' num2str(nx) 'x' num2str(ny) ', but Y is ' num2str(nx2) 'x' num2str(ny2)];
    fail_out = 1;
    error(msg);
  end
  output = zeros(2*ny*nx, 1);
  k = 1;
  for i = 1:ny
    for j = 1:nx
      output(k) = X(i,j);
      k = k+1;
    end
  end
  for i = 1:ny
    for j = 1:nx
      output(k) = Y(i,j);
      k = k+1;
    end
  end
  fid = fopen (filename, "w");
  fdisp(fid,'1');
  dims = [num2str(nx) '    ' num2str(ny)];
  fdisp(fid,dims);
  st = '';
  for l = 1:2*ny*nx
    st = [st '    ' num2str(output(l), "%5.15f")];
    if mod(l,3) == 0
      fdisp(fid, strtrim(st));
      st = '';
    end
  end
  if length(st) > 0
    fdisp(fid, strtrim(st));
  end
  fclose (fid);
  fail_out = 0;
end