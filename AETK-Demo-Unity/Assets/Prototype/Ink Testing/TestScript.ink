VAR drinked_the_stuff = false

-> Beginning

== Beginning ==
The hills are aliivveeee... With the sound of crappah~~

Our hearts are all filled... with the sounds of craaaaaapaaahhh~~~

Well... What do you do now?
    * Stink it.
        You let a massive stinker rip through the thin morning air. 
        All of the townspeople about are now running for their lives.
        
        What do?
            -> Choice
        
    * Drink it.
        ~ drinked_the_stuff = true
        You drink up, and wipe the Biosalud from your lips.
        
        'MMMMMMM', you mutter. So yum-bubbly. Very tasty.
        
        What's on the agenda now?
            -> Choice
            
== Choice ==
** Cry
    Lol, you cried your dumb little eyes out, bozo. Now you're looking like a big ole baby.
    Good luck coming back from that blunder. LOL HEHE LMAO.
        -> END
** Die
    -> Die

== Die ==
You dieded. It was sadd. :( Rip

* {drinked_the_stuff} Resurrect. 
    -> Resurrection
* [Accept deth.]
    Goodbye. -> END
    
== Resurrection ==
Your body starts to hover in the air as the thick Biosalud flows through your veins, repairing all of your ailments.

You land back on the ground after having done a sick backflip mid-air. You're alive again, and everything is as it should be.

That is, filled with Biosalud.

THE END.
    -> END