# LudumDare53
https://ldjam.com/events/ludum-dare/53

## [Insert title]

### Elevator Pitch:
In "[Insert title]", you play as a military man trying to mail themselves. To do so, you must sneak into a post office disguised as a cardboard box. Guards and employees can see you when you move, but you perfectly fit in the box when you stand still. Get to the loading docks to get loaded into a truck without being caught!

### Controls:
- WASD or Arrow Keys: Move the character
- E or Enter: Interact with objects

### Camera:
- Top-down perspective
- Fixed camera, the camera moves to the next screen when you get to the goal of the current screen

### Character:
- The player is a military man disguised as a cardboard box
- Can move around, which lifts the carboard, allowing guards to see you

### AI:
- Guards and employees move along fixed paths
- The AI has a cone of vision, and the player can avoid being seen by staying out of it
- If they see the player, they will give chase and try to catch them. The game goes in "Alert Mode"
    - If the Guard touches the player, it's game over
    - If the player breaks line of sight, the guard will go to the last known position of the player
    - If the Guards can't see the player for N seconds, they go back to their patrolling  behavior
    - Hiding in the box doesn't work in Alert mode
- In Alert mode, all the guard know the location of the player
    - Guards move quicker in alert mode
- Other possible type of AIs: 
    - Employees that pick up the player(in the box), and place them somewhere else, 
    - Camera, scanner door, movement detector
        - All of those make the game go into alert mode if triggered.

### Game Loop:
- Start screen with options to start the game or exit
- Main game screen where the player must sneak through different levels of the post office, avoiding guards and employees to get to the loading docks
    - If you get caught, you restart in your current screen and the AI go back to their initial state
- Loading dock screen where the player must get into the truck without being caught
- End screen with a score based on how quickly the player completed the game and how many times they were caught.
