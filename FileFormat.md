To use the application you should give it some data to process, so it can make the assignments of the students to the courses.
You need to create two files, one for the courses and one for the students. Both are simple comma separated files. The file name can be anything you want, you have to set up the file names in the [configuration file](Configuration.md) before starting the program.


# Courses file description #

The courses file needs three columns of data, all mandatory.

```
id;description;places
```

  * **id:** the unique identifier of that course, must be a number.
  * **description:** the text description or name of the course.
  * **places:** the capacity of students for that course, if you set it to five it means only five students can get into the course.

## Example ##
```
1;English Friday 2 PM;10
2;English Monday;5
...
89;Math Special class;2
...
```

An example file can be found in the code at http://code.google.com/p/geneticassigner/source/browse/trunk/GeneticAssigner/courses.txt


# Students file description #

The students file needs at least three columns of data, you can have until seven columns, the first two are for identification and description and the rest are the course number selected. The different options must be unique for the same student

```
id;description;option1[;option2;option3;option4;option5]
```

  * **id:** the unique identifier of that student, must be a number.
  * **description:** the text description, usually the name of the student.
  * **option1:** the course id which is the first priority option for that student, this one is mandatory.
  * **option2-5:** the optional less priority course ids that the program might choose if there is not more places on the first option course.

## Example ##
```
1;Adam Andrew;10
2;George O.;5;4;2
3;Pedro P.;2;4;1;89;3
...
123;Anne S.;2;1;89;3
```


An example file can be found in the code at http://code.google.com/p/geneticassigner/source/browse/trunk/GeneticAssigner/students.txt