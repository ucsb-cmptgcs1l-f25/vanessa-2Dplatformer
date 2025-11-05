# Testing Library Used
Unity Testing Framework 
# Approaches
# Unit Testing
EditMode in Unity: EditMode is able to run the small logic-based code that tests particular functions. You aren't supposed to create Gameobjects, but as it is only to test if a function produces the right outcome. I ran a unit test that checked my timer was working and counting down correctly. My logic was done in a script named Countdown, which counts down from 60 seconds. My test passed, showing that my Countdown script works correctly.
# Component Testing
Playmode in Unity: Playmode is able to simulate when I hit run in my game. I am able to create game objects and build my scripts with them, leading to the "component testing" portion. I have created a test where it checks the player respawn component. Using my PlayerRespawn script, I wrote a unity test where the player is automatically placed in the same position as my fall detector collider, which should respawn my player back to the original starting point if working correctly. I am essentially checking what the player's final position is after the collider is at the same position as my player. My test passes, showing my PlayerRespawn script works correctly.
