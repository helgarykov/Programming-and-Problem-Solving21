open color

let red = (255, 0, 0)
let green = (0, 255, 0)
let arg = color.add red green
let factor = 1.25
let bright = color.scale factor arg

printfn "Bright grey is: %A" bright
