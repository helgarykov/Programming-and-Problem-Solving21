let rotate (list: char list) =
    let rec r (index: int) (acc: char list) =
        match index with
        | i when i >= list.Length -> acc
        | i when i = 0 -> r (index + 1) ((list.Item (index + 3)) :: acc)
        | i when i = 1 -> r (index + 1) ((list.Item (index - 1)) :: acc )
        | i when i = 3 -> r (index + 1) ((list.Item (index + 1)) :: acc)
        | i when i = 4 -> r (index + 1) ((list.Item (index - 3)) :: acc)
        | _ -> r (index + 1) ((list.Item index) :: acc)
    
    List.rev (r 0 [])
   

let alphabet = ['a'; 'b'; 'c'; 'd'; 'e'; 'f']

printfn "Test: Rotate"
printfn "%A" (rotate alphabet)