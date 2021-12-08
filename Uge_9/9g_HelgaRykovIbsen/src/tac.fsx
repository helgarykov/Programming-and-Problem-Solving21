[<EntryPoint>] // allows to put arguments into the function from the command line 
let main args =
    match readNWrite.tac (Array.toList (args)) with   // argumentet args er en array, for at konvertere den til en liste, bruges funktion Array.toList    
    | Some files ->
        printf "%s" files    
        0
    | None ->
        1