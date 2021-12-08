module color

let trunc v =
  if v < 0 then
    0
  else if v > 255 then
    255
  else
    v


let add (r1, g1, b1, r2, g2, b2) =
  let r = trunc (r1 + r2)
  let g = trunc (g1 + g2)
  let b = trunc (b1 + b2)
  (r, g, b)

let scale (x, y, z, c) =
  let kat = x * c
  let isvaffel = y * c
  let motorvej = z * c
  (trunc kat, trunc isvaffel, trunc motorvej)

let grey (grey1, grey2, grey3) =
  let greyconvert = (grey1 + grey2 + grey3)/3
  (trunc greyconvert, trunc greyconvert, trunc greyconvert)

val grey : int * int * int -> int * int * int
  

