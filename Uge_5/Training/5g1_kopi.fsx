let a = Array2D.init 2 3 (fun r c -> (c,r))


let transposeArr (a : 'a[,]) =
    Array2D.init (Array2D.length2 a) (Array2D.length1 a) (fun r c -> a.[c,r])

let newArray = array2D [[1;2]; [3;4]]

let transpose arr =
    Array2D.init (Array2D.length2 arr) (Array2D.length1 arr) (fun r c -> arr.[c,r])

printfn "%A" (newArray)
printfn "%A" (transpose newArray)



printfn "%5b:" ((transposeArr newArray) = array2D[[1;3]; [2;4]]) 
