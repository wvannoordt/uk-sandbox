from tkinter import *
#variables
last_x = 0
last_y = 0
l_down = False
r_down = False
xs = []
n = 0
r = 3
#end variables

m=Tk()

canvas_width = 1620
canvas_height = 3000
w = Canvas(m,width=canvas_width,height=canvas_height)
w.pack()



#widget start
def lclick(event):
	global last_x, last_y, l_down, n, xs
	l_down = True
	if not r_down:
		last_x, last_y = event.x, event.y
		xs.append(last_x)
		xs.append(last_y)
		n = int(len(xs)/2)
		w.create_oval(xs[2*n-2] - r, xs[2*n-1] - r, xs[2*n-2] + r, xs[2*n-1] + r, fill="red")
		if n > 1:
			w.create_line(xs[2*n-2], xs[2*n-1], xs[2*n-4], xs[2*n-3], width=2)

def lrelease(event):
	global l_down
	l_down = False

def rclick(event):
	global r_down
	r_down = True

def rrelease(event):
	global r_down
	r_down = False
#widget end

#binding start
m.bind('<Button-1>', lclick)
m.bind('<ButtonRelease-1>', lrelease)
m.bind('<Button-3>', rclick)
m.bind('<ButtonRelease-3>', rrelease)
#binding end
m.mainloop()
