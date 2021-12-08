

1.I started out by creating a signature document - vec2dsmall.fsi, containing the description of :1) the summary, 2) functions, 3) parameters, 4) the type of input and the type of the  output (e.g. integers or floats).

2. I wrote the code/my functions in the implementation document - vec2dsmall.fs.

3. I compiled both the .fsi and .fs files and translated them into a library. I did that by running the command on the console:
fsharpc -a vec2dsmall.fsi vec2dsmall.fs

4. I created the application document - 4i1.fsx - containing my Black-box test for the task 4i1.

5. I linked the newly created library file - vec2dsmall.dll - to my application document - 4i1.fsx. I did that by running the command on the console:
fsharpc -r vec2dsmall.dll 4i1.fsx

6. I got the output on the console by running:
mono 4i1.exe

7. Consider my Black-box test and the comments on the erroneous output in the file 4i1.fsx.



Step     Line      Env    File       Bindings and evaluations
-------------------------------------------------------------------------------------------------------------------
1        1         E_0    4i2.fsx     v = 1.3, -2.5
2        2         E_0    4i2.fsx     printfn "Vector %A: (%f, %f)" v (vec2d.len v) = ? (vec2d.ang v) = ?
3        2         E_0    4i2.fsx     len v = ?
3        3,4       E_1    vec2d.fs    len = ((v), sqrt (x ** 2.0 + y ** 2.0), ())
4        4         E_1    vec2d.fs    return = 2.817
5        2         E_0    4i2.fsx     ang v = ?
6        7,8       E_2    vec2d.fs    ang = ((v), atan2 y x, ())
7        8         E_2    vec2d.fs    return = -1.091
8        3         E_0    4i2.fsx     w = -0.1, 0.5
9        4         E_0    4i2.fsx     printfn "Vector %A: (%f, %f)" w (vec2d.len w) = ? (vec2d.ang w) = ?
10       4         E_0    4i2.fsx     len w = ?
11       3,4       E_1    vec2d.fs    len = ((w),  sqrt (x ** 2.0 + y ** 2.0), ())
12       4         E_1    vec2d.fs    return = 0.509
13       4         E_0    4i2.fsx     ang w = ?
14       7,8       E_2    vec2d.fs    ang = ((w), atan2 y x, ())
15       8         E_2    vec2d.fs    return = 1.768
16       5         E_0    4i2.fsx     s = vec2d.add v w
17       6         E_0    4i2.fsx     printfn "Vector %A: (%f, %f)" s (vec2d.len s) = ? (vec2d.ang s) = ?
18       6         E_0    4i2.fsx     len s = ?
19       5         E_0    4i2.fsx     s = vec2d.add v w
20       5         E_0    4i2.fsx     add = ?
21       11        E_3    vec2d.fs    add = ((v), (w), v + w, ())
22       11        E_3    vec2d.fs    ((v = (1.3,-2.5), w =(-0.1, 0.5)), v + w, ())
23       5         E_0    4i2.fsx     return = 1.2, -2
24       3, 4      E_1    vec2d.fs    len = ((s), sqrt (x ** 2.0 + y ** 2.0), ())
25       4         E_1    vec2d.fs    return = 2.332
26       6         E_0    4i2.fsx     ang s = ?
27       7,8       E_2    vec2d.fs    ang = ((s), atan2 y x, ())
28       8         E_2    vec2d.fs    return = -1.03
