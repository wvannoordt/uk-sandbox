function [fail_out] = p2dwrite(filename, X, Y)
  [ny, nx] = size(X);
  [ny2, nx2] = size(Y);
  if ~(ny == ny2) || ~(nx == nx2)
    msg = ['Unequal matrix sizes: X is ' num2str(nx) 'x' num2str(ny) ', but Y is ' num2str(nx2) 'x' num2str(ny2)];
    fail_out = 1;
    error(msg);
  end
  N = 2*ny*nx;
  nline = 3;
  nrowfull = floor(N / nline);
  nres = mod(N, nline);
  output = [reshape(X', ny*nx, 1); reshape(Y', ny*nx, 1)];
  fid = fopen (filename, "w");
  fdisp(fid,'1');
  dims = [num2str(nx) '    ' num2str(ny)];
  fdisp(fid,dims);
  inext = -1;
  for i = 0:(nrowfull - 1)
    n1 = i*nline + 1;
    n2 = n1 + nline - 1;
    inext = n2 +1;
    st = num2str((output(n1:n2))', "%20.15f");
    fdisp(fid,st);
  end
  if inext < N
    st = num2str((output(inext:N))', "%20.15f");
    fdisp(fid,st);
  end
  if length(st) > 0
    fdisp(fid, strtrim(st));
  end
  fclose (fid);
  fail_out = 0;
end