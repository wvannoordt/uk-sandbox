import sys

#Define LineOot points
p0 = (0.97, 0, 0);
p1 = (0.97, 0.025, 0);

#Define read from this database
database_filename = "/home/wvn/dirs/sandbox/cfd-data/NASA-comparison/data-raws/data-137x97-2ndorder/mu_t_0050000.hdf5"
mesh = "Mesh"
VAR_IDX = 3
DeleteAllPlots()

#Open the database
print("opening {}".format(database_filename))
print("status code {}".format(OpenDatabase(database_filename)))

#Get metadata
md = GetMetaData(database_filename)
names_st = md.GetNumScalars();

#Print available variable names
namesarr = [];
for i in range(names_st):
	namesarr.append(md.GetScalars(i).name)
	print "{}{}".format(namesarr[i], ":")

#Add a plot
#AddPlot("Pseudocolor", namesarr[variable])

#Add a transform and set properties.
#AddOperator("Displace")
#attributes = DisplaceAttributes()
expname = "phys_transform"
expression = "{<" + namesarr[0] + ">,<" + namesarr[1] + ">,<" + namesarr[2] + ">}"
print "Defining transform expression: " + expname + " = " + expression
print "status code {}".format(DefineVectorExpression(expname, expression))


#attributes.SetTransformType(1)
#attributes.SetDoTranslate(True)
#attributes.SetVectorTransformMethod(1)

#Add a plot
print "plotting " + namesarr[VAR_IDX] + ":"
AddPlot("Pseudocolor", namesarr[VAR_IDX])
#DrawPlots()



AddOperator("Displace")




t = DisplaceAttributes()
t.SetVariable(expname)

SetOperatorOptions(t)

print t

DrawPlots()


#print t
#print dir(t)



Lineout(p0, p1, (namesarr[VAR_IDX]))
loatts = LineoutAttributes()
SetActiveWindow(2)
print loatts
print dir(loatts)
gloatts = GetGlobalLineoutAttributes()
gloatts.SetFreezeInTime(1)
print gloatts
print dir(gloatts)
opts = GetExportOptions("Curve2D")
dbAtts = ExportDBAttributes()
dbAtts.db_type = "Curve2D"
dbAtts.filename = "./data-output/output"
dbAtts.variables = (namesarr[VAR_IDX])
print dbAtts
ExportDatabase(dbAtts)
#AddPlot("Pseudocolor", 'Uvel')


sys.exit()
