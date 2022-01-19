open Robots

[<EntryPoint>]
let main (args: string array) : int =
    try
        let lst = args |> Array.toList
        let row = lst.[0] |> int
        let col = lst.[1] |> int
        let robots = lst.[2..]
        for r in robots do
            if (string r).Length <> 2 then raise (System.ArgumentException "Error: \"Robot names must have a length of two characters\"")
        let g = Game(row,col,robots)
        g.Play()
        0
    with
        | :? System.ArgumentException as ex -> printfn "%A" ex.Message; 0
        | _ -> 1
