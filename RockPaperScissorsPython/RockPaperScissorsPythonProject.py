import random

def RunGame():
    x = "Rock"
    y = "Paper"
    z = "Scissors"

    PlayerChoice = input("Enter Rock, Paper, or Scissors: ")
    print("\n")

    Choices = ["Rock", "Paper", "Scissors"]

    ComputerChoice = random.choice(Choices)
    print("Computer Selected: " + ComputerChoice)
    print("\n")



    if PlayerChoice == x and ComputerChoice == "Scissors":
        print("Player Wins!!!\n\n")
    elif PlayerChoice == x and ComputerChoice == "Rock":
        print("IT'S A DRAW!!!\n\n")
    elif PlayerChoice == x and ComputerChoice == "Paper":
        print("Computer Wins!!!\n\n")

    if PlayerChoice == y and ComputerChoice == "Scissors":
        print("Computer Wins!!!\n\n")
    elif PlayerChoice == y and ComputerChoice == "Rock":
        print("Player Wins!!!\n\n")
    elif PlayerChoice == y and ComputerChoice == "Paper":
        print("ITS A DRAW!!!\n\n")

    if PlayerChoice == z and ComputerChoice == "Scissors":
        print("ITS A DRAW!!!\n\n")
    elif PlayerChoice == z and ComputerChoice == "Rock":
        print("Computer Wins!!!\n\n")
    elif PlayerChoice == z and ComputerChoice == "Paper":
        print("Player Wins!!!\n\n")

    KeepPlaying = input("Would you like to keep playing? ")
    
    if KeepPlaying == "Yes":
        print("\n")
        RunGame()
    else:
        print("\n\nThanks for playing!\n\n")
        quit()

#Main/Entry Point
a = input("Would you like to play Rock, Paper, and Scissors? ")
print("\n")

if a == "Yes": 
    RunGame()
else:
    print("\nMaybe Next Time!")
    quit()