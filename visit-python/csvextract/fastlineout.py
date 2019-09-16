##########
#this method does not work. for whatever reason, accessing the mu_t data directly
#gives some random, nonsenical values. will have to resort to exporting curve
#files and reading from them. VisIt is stupid.
##########
def main():	
	variable = "mu_t"
	access_mut_database = ("mu_t" in variable.lower())
	input_dirname = "./sample_data"
	outputfilename = "output.csv"
	#listing = os.listdir(targetdir)
	p0 = (0.97, 0, 0)
	p1 = (0.97, 0.040, 0)
	
	inputfilename = get_relevant_data_filename(input_dirname, access_mut_database)
	print "fetching data from " + inputfilename + ":"
	
	outputfilename = input_dirname + "-" + variable + ".csv"
	print "writing" + outputfilename + ":"
	
	data = lineout(inputfilename, p0, p1, variable)
	write_csv_x_y_z_var(outputfilename, data)
	
	octave_title_filename = "NONE"
	has_octave_title = os.path.exists(os.path.join(input_dirname,"OCTAVE_TITLE"))
	if has_octave_title:
		octave_title_filename = os.path.join(input_dirname,"OCTAVE_TITLE")
	
	print octave_title_filename
	
	

def get_relevant_data_filename(directoryname, seeking_mut):
	all_filenames = sorted(os.listdir(directoryname))
	searchterm = "azesc_flow";
	filename_output = "NO_FILE_FOUND";
	if seeking_mut:
		searchterm = "mu_t"
	for filename in all_filenames:
		if filename.endswith(".hdf5") and (searchterm in filename):
		    filename_output = filename
	return os.path.join(directoryname, filename_output)

def write_csv_x_y_z_var(filename, data):

	f = open(filename, "w")
	for i in range(len(data[0])):
		f.write("{},{},{},{}\n".format(data[0][i], data[1][i], data[2][i], data[3][i]))
	f.close()

def lineout(inputfilename, p0, p1, outputvar):
	DeleteAllPlots()
	VAR_IDX = -1
	OpenDatabase(inputfilename)
	md = GetMetaData(inputfilename)
	scalar_names_count = md.GetNumScalars()
	print "{} scalars found".format(scalar_names_count)
	names_array = [];
	for i in range(scalar_names_count):
		names_array.append(md.GetScalars(i).name)
		if outputvar.lower() in names_array[i].lower():
			VAR_IDX = i;
		print "{}{}".format(names_array[i], ":")
	print "Retrieving " + names_array[VAR_IDX] + ":"
	expname = "phys_transform"
	expression = "{<" + names_array[0] + ">,<" + names_array[1] + ">,<" + names_array[2] + ">}"
	DefineVectorExpression(expname, expression)
	AddPlot("Pseudocolor", names_array[VAR_IDX])
	AddOperator("Displace")
	t = DisplaceAttributes()
	t.SetVariable(expname)
	SetOperatorOptions(t)
	DrawPlots()
	Lineout(p0, p1, (names_array[VAR_IDX]))
	SetActiveWindow(2)
	global_attributes = GetGlobalLineoutAttributes()
	global_attributes.SetFreezeInTime(1)
	database_attributes = ExportDBAttributes()
	database_attributes.db_type = "Curve2D"
	database_attributes.filename = "."
	database_attributes.variables = (names_array[VAR_IDX])
	#ExportDatabase(database_attributes)
	#variable = GetPlotInformation()["Curve"]
	SetActivePlots(0)
	vals = GetPlotInformation()["Curve"]
	SetActivePlots(1)
	xc = GetPlotInformation()["Curve"]
	SetActivePlots(2)
	yc = GetPlotInformation()["Curve"]
	SetActivePlots(3)
	zc = GetPlotInformation()["Curve"]
	DeleteWindow()
	#DeleteWindow()
	
	return (xc,yc,zc,vals)
	
#MAIN
import sys
import os
import csv
main()
os.remove("./visitlog.py")
sys.exit()
