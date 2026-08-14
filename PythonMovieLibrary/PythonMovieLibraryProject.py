
import tkinter as tk
from tkinter import ttk
import pyodbc 

    
def Main():
    root = tk.Tk()
    root.title("Insert Name Test")
    root.geometry("400x300")

    frame = tk.Frame(root)
    frame.pack(padx=20, pady=20, fill="both", expand = True)

    label = tk.Label(frame, text = "Movie Library!!!", font = ("Arial", 36))
    label.pack(pady = 10)

    label = tk.Label(frame, text = "Choose an option below!!!", font = ("Arial", 24))
    label.pack(pady = 10)
    
    label = tk.Label(frame, text = "1. Search Movie Library By Name", font = ("Arial", 12))
    label.pack(pady = 10)

    label = tk.Label(frame, text = "2. Search Movie Library By Rating", font = ("Arial", 12))
    label.pack(pady = 10)

    label = tk.Label(frame, text = "3. Search Movie Library By Genre", font = ("Arial", 12))
    label.pack(pady = 10)

    label = tk.Label(frame, text = "4. Search Movie Library By Year", font = ("Arial", 12))
    label.pack(pady = 10)

    label = tk.Label(frame, text = "5. Get A Random Movie", font = ("Arial", 12))
    label.pack(pady = 10)

    label = tk.Label(frame, text = "Enter Selection Below", font = ("Arial", 10))
    label.pack(pady = 10)

    text = tk.IntVar()
    entry = tk.Entry(frame, textvariable=text, width = 30)
    entry.pack(padx = 10, pady = 10, side=tk.TOP)
    
    button = tk.Button(frame, text = "Click Me To Confirm Selection", command=lambda: CheckSelection(text.get()))
    button.pack(pady = 10, side = tk.TOP)

    root.mainloop()
    

def CheckSelection(selection):
    if selection == 1:
        print("Option 1 has been chosen")
        Option1()   
    if selection == 2:
        print("Option 2 has been chosen")
        Option2()
    if selection == 3:
        print("Option 3 has been chosen")
        Option3()
    if selection == 4:
        print("Option 4 has been chosen")
        Option4()
    if selection == 5:
        print("Option 5 has been chosen")
        Option5()

def Option1():
    print("Option 1 function has been reached!!!")

    top = tk.Toplevel()
    top.title("Searching By Name!")
    top.geometry("400x300")

    frame = tk.Frame(top)
    frame.pack(padx = 20, pady = 20, fill = "both", expand = True)

    label = tk.Label(frame, text="Enter Movie Name You Are Looking For:", font = ("Arial", 24))
    label.pack(pady = 10)

    text = tk.StringVar()
    entry = tk.Entry(frame, textvariable=text, width = 30)
    entry.pack(padx = 10, pady = 10, side=tk.TOP)
    
    button = tk.Button(frame, text = "Click Me To Confirm Selection", command=lambda: MovieNameSearch(text.get()))
    button.pack(pady = 10, side = tk.TOP)

    columns = ["MovieName", "MovieRating", "Genre", "YearReleased", "ID"]
    tree = ttk.Treeview(frame, columns=columns, show='headings')
    tree.pack(pady = 5, fill = tk.X, expand = True)

    for col in columns:
        tree.heading(col, text=col)

    def MovieNameSearch(moviename):

        query = "SELECT * FROM MoviesandReviews WHERE MovieName = ?"
        cursor.execute(query, (moviename))

        results = cursor.fetchall()

        print("Query Results: ")
        for row in results:
            print(row)

        for row in tree.get_children():
            tree.delete(row)

        for row in results:
            tree.insert('', tk.END, values=row)


def Option2():
    print("Option 2 function has been reached!!!")       

    top = tk.Toplevel()
    top.title("Searching By Rating!")
    top.geometry("400x300")

    frame = tk.Frame(top)
    frame.pack(padx = 20, pady = 20, fill = "both", expand = True)

    label = tk.Label(frame, text="Enter Rating Range You Are Looking For:", font = ("Arial", 24))
    label.pack(pady = 10)

    label = tk.Label(frame, text="Enter Low End Of Range:", font = ("Arial", 12))
    label.pack(pady = 10)

    LowEnd = tk.IntVar()
    entry = tk.Entry(frame, textvariable=LowEnd, width = 30)
    entry.pack(padx = 10, pady = 10, side=tk.TOP)

    label = tk.Label(frame, text="Enter High End Of Range:", font = ("Arial", 12))
    label.pack(pady = 10)

    HighEnd = tk.IntVar()
    entry = tk.Entry(frame, textvariable=HighEnd, width = 30)
    entry.pack(padx = 10, pady = 10, side=tk.TOP)
    
    button = tk.Button(frame, text = "Click Me To Confirm Selection", command=lambda: MovieRatingSearch(LowEnd.get(), HighEnd.get()))
    button.pack(pady = 10, side = tk.TOP)

    columns = ["MovieName", "MovieRating", "Genre", "YearReleased", "ID"]
    tree = ttk.Treeview(frame, columns=columns, show='headings')
    tree.pack(pady = 5, fill = tk.X, expand = True)

    for col in columns:
        tree.heading(col, text=col)

    def MovieRatingSearch(LowEnd, HighEnd):

        query = "SELECT * FROM MoviesandReviews WHERE MovieRating BETWEEN ? AND ?"
        cursor.execute(query, (LowEnd, HighEnd))

        results = cursor.fetchall()

        print("Query Results: ")
        for row in results:
            print(row)

        for row in tree.get_children():
            tree.delete(row)

        for row in results:
            tree.insert('', tk.END, values=row)

def Option3():
    print("Operation 3 Function Has Been Reached!")

    top = tk.Toplevel()
    top.title("Searching By Genre!")
    top.geometry("400x300")

    frame = tk.Frame(top)
    frame.pack(padx = 20, pady = 20, fill = "both", expand = True)

    label = tk.Label(frame, text="Enter Movie Genre You Are Looking For:", font = ("Arial", 24))
    label.pack(pady = 10)

    text = tk.StringVar()
    entry = tk.Entry(frame, textvariable=text, width = 30)
    entry.pack(padx = 10, pady = 10, side=tk.TOP)
    
    button = tk.Button(frame, text = "Click Me To Confirm Selection", command=lambda: MovieGenreSearch(text.get()))
    button.pack(pady = 10, side = tk.TOP)

    columns = ["MovieName", "MovieRating", "Genre", "YearReleased", "ID"]
    tree = ttk.Treeview(frame, columns=columns, show='headings')
    tree.pack(pady = 5, fill = tk.X, expand = True)

    for col in columns:
        tree.heading(col, text=col)

    def MovieGenreSearch(moviegenre):

        query = "SELECT * FROM MoviesandReviews WHERE Genre LIKE ?"
        cursor.execute(query, (f'%{moviegenre}%'))

        results = cursor.fetchall()

        print("Query Results: ")
        for row in results:
            print(row)

        for row in tree.get_children():
            tree.delete(row)

        for row in results:
            tree.insert('', tk.END, values=row)

def Option4():
    print("Option 4 Function Has Been Reached")

    top = tk.Toplevel()
    top.title("Searching By Year Released!")
    top.geometry("400x300")

    frame = tk.Frame(top)
    frame.pack(padx = 20, pady = 20, fill = "both", expand = True)

    label = tk.Label(frame, text="Enter Year Range You Are Looking For:", font = ("Arial", 24))
    label.pack(pady = 10)

    label = tk.Label(frame, text="From This Year:", font = ("Arial", 12))
    label.pack(pady = 10)

    LowEnd = tk.IntVar()
    entry = tk.Entry(frame, textvariable=LowEnd, width = 30)
    entry.pack(padx = 10, pady = 10, side=tk.TOP)

    label = tk.Label(frame, text="To This Year:", font = ("Arial", 12))
    label.pack(pady = 10)

    HighEnd = tk.IntVar()
    entry = tk.Entry(frame, textvariable=HighEnd, width = 30)
    entry.pack(padx = 10, pady = 10, side=tk.TOP)
    
    button = tk.Button(frame, text = "Click Me To Confirm Selection", command=lambda: YearReleasedSearch(LowEnd.get(), HighEnd.get()))
    button.pack(pady = 10, side = tk.TOP)

    columns = ["MovieName", "MovieRating", "Genre", "YearReleased", "ID"]
    tree = ttk.Treeview(frame, columns=columns, show='headings')
    tree.pack(pady = 5, fill = tk.X, expand = True)

    for col in columns:
        tree.heading(col, text=col)

    def YearReleasedSearch(LowEnd, HighEnd):

        query = "SELECT * FROM MoviesandReviews WHERE YearReleased BETWEEN ? AND ?"
        cursor.execute(query, (LowEnd, HighEnd))

        results = cursor.fetchall()

        print("Query Results: ")
        for row in results:
            print(row)

        for row in tree.get_children():
            tree.delete(row)

        for row in results:
            tree.insert('', tk.END, values=row)

def Option5():
    print("Option 5 Function Has Been Reached!")

    top = tk.Toplevel()
    top.title("RANDOM MOVIE GENERATOR!!!")
    top.geometry("400x300")

    frame = tk.Frame(top)
    frame.pack(padx = 20, pady = 20, fill = "both", expand = True)

    label = tk.Label(frame, text="Click Button Below To Get A Random Movie!", font = ("Arial", 24))
    label.pack(pady = 10)
    
    button = tk.Button(frame, text = "Random Movie!!!", command=lambda: RandomMovieGenerator())
    button.pack(pady = 10, side = tk.TOP)

    columns = ["MovieName", "MovieRating", "Genre", "YearReleased", "ID"]
    tree = ttk.Treeview(frame, columns=columns, show='headings')
    tree.pack(pady = 5, fill = tk.X, expand = True)

    for col in columns:
        tree.heading(col, text=col)

    def RandomMovieGenerator():

        query = "SELECT TOP 1 * FROM MoviesandReviews ORDER BY NEWID()"
        cursor.execute(query)

        results = cursor.fetchall()

        print("Query Results: ")
        for row in results:
            print(row)

        for row in tree.get_children():
            tree.delete(row)

        for row in results:
            tree.insert('', tk.END, values=row)


#CONNECTS TO SQL DATABASE

DRIVER = 'ODBC Driver 17 for SQL Server'
SERVER = r'DESKTOP-L9G3OCN\SQLEXPRESS'
DATABASE = 'PythonMovieDatabase'

  
connectionString = (
    f'DRIVER={DRIVER};'
    f'SERVER={SERVER};'
    f'DATABASE={DATABASE};'
    f'Trusted_Connection=yes;')

conn = pyodbc.connect(connectionString)

cursor = conn.cursor()

print("We have connection to database")

Main()

conn.close()