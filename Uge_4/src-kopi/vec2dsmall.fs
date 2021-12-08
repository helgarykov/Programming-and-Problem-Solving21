module vec2d

let len (x, y) =
  sqrt (x ** 2.0 + y ** 2.0)


let ang (x, y) =
  atan2 y x


let add (x1:float, y1:float) (x2:float, y2:float) =
  let vectorX = x1 + x2
  let vectorY = y1 + y2
  (vectorX, vectorY)


//Given the desired number of decimals and a float, round
// the float off to the target number of decimals.
let round (decimals:int) (x:float) =
    System.Math.Round(x, decimals)
