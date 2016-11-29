# PhysicsA2

Carlo Albino, 
Rohelio Lopez, 
Wahid Shafique, 

Final Assignment rubric


Main Player/Avatar (5%)

The main player must be driven by physics, either setting a velocity or applying a force.The player should be able to move on the xz plane and have the ability to jump.You must detect when the player has failed i.e. fallen to their death, you can do this by checking a certain velocity on the y axisand/or having a death y value or however other way you see fitting


Level Layout

Projectile Intro (5%)

your avatar must be launched into the start of your level, so the level opens with a representation of launcher, when the user presses space to start the game the avatar is launched and lands at the beginning portion of your level.upon landing the player should be able to move and there should be a particle effect, or an audio cue as feedback to notify the player the game has started

.
Moving Platforms


Level 1 (5%)

Have a physics driven(ie set velocity and do not manually translate object) platform.this platform will move side to side, when the player jumps on the platform, an upward force should be applied to move the platform to a connecting plane or object that the player can move onto.

Level 2 (10%)

The platforms are stationary but when the player jumps on one, they sink at whatever pace you want them to before eventually succumbing to gravity and just falling at the acceleration of gravity.


Friction surfaces


Level 1 (2.5%)

Have a surface that simulates ice, when the player gets on this surface, they should continue moving in whatever direction they were moving in even if they let go of the axis input.

Level 2 (2.5%)

Have a glue like surface which reduces the players speed.


Spring Joints


Level 1 (10%)

create a rope like spring joint that is over a pit.This rope will allow the player to jump and attach to it, and then detach when it's safe to land on the other side.The player can detach whenever they like, but they should have the ability to cross the pit if their timing is correct, in other words make sure the player has a chance to succeed.


Level 2 (15%)

over the glue like surface, have a spring joint the player can jump and attach to.when the player is attached the spring joint should compress(elevating the player slightly) and then move to the end of the glue like surface at which point it should decompress to lower the player.The player should be able to release themself from the spring at any point with whatever designated input you choose.

Boosts


Level 1 (10%)

An impulse that sends you flying in the air a random height that you determine, this should be a one time use.If the player returns to it, it should not work.

Level 2 (20%)

A ramp boost. Once your player reaches the ramp, the force/impulse it applies is additive. The ramp if approached at a very slow speed should be able to get the player from the ramp to the next platform. 
Torque 


Level 1 (10%)

Have a kinematic object that rotates about its origin using torque or angular velocity.The player will be able to jump on this object, stick on to the object and then jump off whenever they desire

Projectile Outro (5%)


Have the player adjust a cannon at the end of the level.As the player is adjusting the cannon you should visually represent where the player will landThere is a platform they must land on to complete the level.So when the player makes contact with the cannon, they shouldn't be able to move anymore, the only input they should have is up and down axis to control the angle of the cannon and whatever input you want to launch them.Once they land on the platform the level is complete.
From that point iy would be ideal if the next level started right away, but i will also accept you closing the editor and openinga new scene that represents your second level.
Do what you can, tackle parts that you feel more confident with first.Feel free to add any additional features, feedbacks etc.Level 3 you can freestyle and make whatever obstacles you want.
