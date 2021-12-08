open vec2d

let v = (1.3, -2.5)
printfn "Vector %A: (%f, %f)" v (vec2d.len v) (vec2d.ang v)

let w = (-0.1, 0.5)
printfn "Vector %A: (%f, %f)" w (vec2d.len w) (vec2d.ang w)

let s = vec2d.add v w
printfn "Vector %A: (%f, %f)" s (vec2d.len s) (vec2d.ang s)

let x = vec2d.round 2 8.76456366
printfn "%f" x
