type point = int * int
type color = ImgUtil.color

//A figure - either a circle or a rectangle
type figure =
  | Circle of point * int * color
  | Rectangle of point * point * color
  | Mix of figure * figure

///<summary> Finds color of figure at point.</summary>
let rec colorAt (x, y) figure =
    match figure with
    | Circle ((cx, cy), r, col) ->
        if (x-cx)*(x-cx)+(y-cy)*(y-cy) <= r*r
        then Some col else None
    | Rectangle ((x0, y0), (x1, y1), col) ->
        if x0 <= x && x <= x1 && y0 <= y && y <= y1
        then Some col else None
    | Mix (f1, f2) ->
        match (colorAt (x, y) f1, colorAt (x, y) f2) with
        | (None, c) -> c
        | (c, None) -> c
        | (Some c1, Some c2) ->
          let (a1, r1, g1, b1) = ImgUtil.fromColor c1
          let (a2, r2, g2, b2) = ImgUtil.fromColor c2
          Some (ImgUtil.fromArgb (((a1+a2)/2), ((r1+r2)/2), ((g1+g2)/2), ((b1+b2)/2)))

let circle = Circle ((50, 50), 45, (ImgUtil.red))
let rectangle = Rectangle ((40, 40), (90, 110), (ImgUtil.blue))
let figTest = Mix (circle, rectangle)
let wCircle = Circle ((50, 50), -45, (ImgUtil.red))
let wRectangle = Rectangle ((40, 40), (25, 25), (ImgUtil.blue))

let makePicture (filnavn:string) (figur:figure) (b:int) (h:int) =
    let canv = ImgUtil.mk b h
    for y=0 to (h-1) do
        for x=0 to (b-1) do
            match (colorAt (x, y) figur) with
            | None -> ImgUtil.setPixel (ImgUtil.fromRgb (128, 128, 128)) (x, y) canv
            | Some col -> ImgUtil.setPixel col (x, y) canv
    ImgUtil.toPngFile filnavn canv

let rec checkFigure (figur:figure) : bool =
    match figur with
    | Circle ((cx, cy), r, col) -> match r with
                                   | _ when r<0 -> false
                                   | _ -> true
    | Rectangle ((x0, y0), (x1, y1), col) -> match ((x1-x0), (y1-y0)) with
                                             | (x, y) when (x<0) || (y<0) -> false
                                             | _ -> true
    | Mix (f1, f2) -> match (checkFigure f1, checkFigure f2) with
                      | (true, true) -> true
                      | _ -> false

let rec move (figur:figure) (x:int, y:int) : figure =
    match figur with
    | Circle ((cx, cy), r, col) -> Circle (((cx+x), (cy+y)), r, col)
    | Rectangle ((x0, y0), (x1, y1), col) -> Rectangle (((x0+x), (y0+y)), ((x1+x), (y1+y)), col)
    | Mix (f1, f2) -> Mix ((move f1 (x, y)), (move f2 (x, y)))

let rec boundingBox (figur:figure) : point * point =
    match figur with
    | Circle ((cx, cy), r, col) -> (((cx-r), (cy-r)), ((cx+r), (cy+r)))
    | Rectangle ((x0, y0), (x1, y1), col) -> ((x0, y0), (x1, y1))
    | Mix (f1, f2) -> let ((minXf1, minYf1), (maxXf1, maxYf1)) = (boundingBox f1)
                      let ((minXf2, minYf2), (maxXf2, maxYf2)) = (boundingBox f2)
                      (((if (minXf1 < minXf2) then minXf1 else minXf2), (if (minYf1 < minYf2) then minYf1 else minYf2)),
                       ((if (maxXf1 > maxXf2) then maxXf1 else maxXf2), (if (maxYf1 > maxYf2) then maxYf1 else maxYf2)))

printfn "%A" (boundingBox figTest)
printfn "Check for figTest figure: %b" (checkFigure figTest)
printfn "Check for wrong circle: %b" (checkFigure wCircle)
printfn "Check for wrong rectangle: %b" (checkFigure wRectangle)
makePicture "test.png" figTest 100 150
makePicture "moveTest.png" (move figTest (-20,20)) 100 150
