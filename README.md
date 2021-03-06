This project consists of a generic [genetic algorithm](http://en.wikipedia.org/wiki/Genetic_algorithm) library ([GeneticAlgorithm](https://github.com/rodrigoq/geneticassigner/tree/master/GeneticAlgorithm)) and an implementation of it with a front-end ([GeneticAssigner](https://github.com/rodrigoq/geneticassigner/tree/master/GeneticAssigner)) for assigning students to class courses.

The library is made in such way that it can be used for other things besides timetabling or scheduling, although it requires some coding for achieving this.

The program takes simple [comma separated files](https://github.com/rodrigoq/geneticassigner/wiki) with the available places for the different courses and the students lists of options for the courses and allocates them  into the courses with the best distribution it can find, trying it best to assign every student with their options priorities.

This program is already being used successfully for assigning psychology students of the University of Buenos Aires to different hospitals and mental health centers for doing practices.

You can run it on every platform that supports the .Net Framework or [Mono](http://www.mono-project.com/)

For more information read the [wiki](https://github.com/rodrigoq/geneticassigner/wiki)
