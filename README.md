# Critters

Playing around with an artificial life simulation

## TODO
Pretty much everything
  
* Finite Cell Automata as backdrop and plant life
  * Growth rules
  * impassible terrain
  * multiple types of plantlife?
  2d array of tuples of objects (updated/current) calculations to be based on current value with a 2nd pass to fixup
  dictionaries? allowing multiple things to exist there. Or pocos and they just know how to interact with others... hm.
  
* Basic configuration
  * file reading
  * snoop on file changes
* UI
  * consider: https://www.i-programmer.info/programming/wpf-workings/620-using-the-wpf-net-40-datagrid-.html
  * mvvm-ify it. What fits with what I use for FCA?
  * view main field
  * reset, step, autorun, speed...
* Critters
  * plant eating critter.
  * getting hungry
  * being idle when not hungry
  * reproduction
    * genomes?
      * sex?
  * critter eating critters.
