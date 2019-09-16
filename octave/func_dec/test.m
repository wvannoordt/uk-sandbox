clear
clc
close all

w = 1

y = some_function(w)

function settings = some_function(input_variable)
  settings.first_value = input_variable;
  settings.second_value = input_variable + 1;
  settings.third_value = input_variable + 2;
  settings.name = 'whatever';
  settings.linecolor = [1 0 0];
  end