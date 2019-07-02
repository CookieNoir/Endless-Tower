# Endless Tower
An unusual realization of the tower defense game, in which you need to build floors of the tower to increase the length of the way enemies go and to add cannons to fight with them.

Each floor of the tower is a construction of 3 sections connected with the ladder.
Each section has 3 slots for cannons and a special slot for a building.

You start the game with only 1 floor of your tower, the null score and a little amount of coins.
You have 15 seconds to build your first cannons to protect the top of the tower from enemies.
Each enemy has its own movement speed and health points. If it dies you get coins and increase the score.
If only a single enemy reaches the top the game ends.

To prevent this situation you can

-upgrade your current towers

-or set new

-or you can build a new floor of the tower to make enemies go another round.

The last action costs some coins and increases the difficulty of the game which leads to appearing of new enemies and growing the health of already known. It also shifts up the limit of the number of enemies.

# How it works

The Game script is attached to the top of the tower

![picture](https://github.com/CookieNoir/Endless-Tower/blob/master/tower_top.png)

The Cam_Moving script is attached to the empty object with coordinates (0,0,z) which contains the camera child object inside with custom coordinates and location

![picture](https://github.com/CookieNoir/Endless-Tower/blob/master/cam_moving.png)

# Other links

Game prototype: https://www.youtube.com/watch?v=wW8IKsFuWiw

Game working demo: https://www.youtube.com/watch?v=LFAHmuc7DiY
