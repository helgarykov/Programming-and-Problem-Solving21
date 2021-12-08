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
