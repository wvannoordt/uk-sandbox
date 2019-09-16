#This is an example script showing how to extract lineout data automatically in VisIt.
#The specifics of this script arise from the SA turbulence model page on NASA's turbulence modelling website:
#https://turbmodels.larc.nasa.gov/spalart.html

#This script can be run from the terminal, without generating a GUI window, using:
#visit -cli -nowin -s visit-python-example.py

import sys

#Define LineOot points
p0 = (0.97, 0, 0);
p1 = (0.97, 0.025, 0);

#Define input filename
database_filename = "/home/wvn/dirs/sandbox/cfd-data/NASA-comparison/data-raws/data-137x97-2ndorder/mu_t_0050000.hdf5"

#Clear any plots
DeleteAllPlots()

#Index of the variable to do lineout for.
#Note: ideally one can specify the variable name, i.e. "mu_t", but the names output from BITCART are "mu_t         "
#including spaces so indexing / searching may be better.
VAR_IDX = 3 #(Index of mu_t)

#Open the database
OpenDatabase(database_filename)

#Get metadata. This is not strictly necessary but is helpful for demonstration purposes.
#Metadata includes things like variable names.
md = GetMetaData(database_filename)

#Get the number of scalar variables in the database.
scalar_names_count = md.GetNumScalars()

#Print available variable names. Again, just for demonstration purposes.
names_array = [];
for i in range(scalar_names_count):
	names_array.append(md.GetScalars(i).name)
	print "{}{}".format(names_array[i], ":")

#Define a vector expression. In this example, the expression {<x>,<y>,<z>} is used (again using the index trick because of those spaces!)
#to transform from the computational domain into the physical domain. The '<' and '>' symbols are necessary.
expname = "phys_transform"
expression = "{<" + names_array[0] + ">,<" + names_array[1] + ">,<" + names_array[2] + ">}"
DefineVectorExpression(expname, expression)

#Add a Pseudocolor plot to the current environment. VisIt must have an active plot to work with.
AddPlot("Pseudocolor", names_array[VAR_IDX])
#AddPlot("Pseudocolor", "mu_t                ")

#Add a displace operator to the current plot.
AddOperator("Displace")

#Set the vector expression from before as the displacement variable.
t = DisplaceAttributes()
t.SetVariable(expname)
SetOperatorOptions(t)

#"Draw" the plots. If the GUI is not active, then nothing will actually be drawn.
#This step is still necessary even without the GUI, since it creates a "window" object that
#is necessary for the lineout operation.
DrawPlots()

#Generate the lineout window. Again, the window is generated, but nothing is shows graphically if there is no GUI. The GUI can be opened with 'OpenGUI()'.
Lineout(p0, p1, (names_array[VAR_IDX]))

#Set active window to lineout window
SetActiveWindow(2)

#Retrieve the global lineout attributes
global_attributes = GetGlobalLineoutAttributes()

#This is necessary if you don't want to export a lineout for every timestep in a database.
global_attributes.SetFreezeInTime(1)

#Get the attributes of the active database
database_attributes = ExportDBAttributes()

#Set output format. Use any of the supported formats in VisIt.
database_attributes.db_type = "Curve2D"

#Set output filename
database_attributes.filename = "./output_mu_t_nasa"

#Add mu_t to export attributes
database_attributes.variables = (names_array[VAR_IDX])

#Export the database.
ExportDatabase(database_attributes)

sys.exit()
