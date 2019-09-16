# Visit 2.13.3 log file
ScriptVersion = "2.13.3"
if ScriptVersion != Version():
    print "This script is for VisIt %s. It may not work with version %s" % (ScriptVersion, Version())
ShowAllWindows()
# The UpdateDBPluginInfo RPC is not supported in the VisIt module so it will not be logged.
SetWindowArea(1280, 988 ,573, 65)
ShowAllWindows()
SetWindowLayout(1)
