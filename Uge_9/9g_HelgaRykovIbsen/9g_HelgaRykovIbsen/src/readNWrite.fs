module readNWrite

///<summary> Given a file name, returns the contents of the text file as a string option. If the file doesn'r exist, it returns None.</summary>
///<param name="filename">A string, file name.</param>
///<returns> A string option.</returns>
let readFile (filename: string) : option<string> =
    try 
        Some (System.IO.File.ReadAllText filename) 
    with 
        | :? System.IO.FileNotFoundException -> None



/// Discriminated union
/// Wrapper type
/// Sum type
(*type Option<'s> =
    | Some value of 's
    | None

    member Value =
        let (Some v) = this
        v *)


/// THIS IS A VERY CLEVER FIX


///<summary> Given a list of file names, concotenates the files. If one of the files doesn't exist, returns None.</summary>
///<param name="filenames">A list of strings.</param>
///<returns> A string option.</returns>
let rec cat (filenames: list<string>) : option<string> =
    match filenames with
        | [] -> None     
        | filename :: [] -> 
            match (readFile filename) with
            | Some content -> Some content
            | None -> None
        | filename :: restOfList -> 
            match (readFile filename) with
            | Some content -> 
                match (cat restOfList) with
                | Some innercontent -> Some (content + innercontent)
                | None -> None
            | None -> None    
    
///<summary> Given a list of file names, reverses the order of each file in a line-by-line manner, reverses each line (opposite of cat) and concatenates the result. If one of the files doesn't exist, returns None.</summary>
///<param name="filenames">A list of strings.</param>
///<returns> A string option.</returns>  
let rec tac (filenames: list<string>) : option<string> =

    let rec reverseText (file: string) : string =
        match file.Length with                       
            | 0 -> ""
            | 1 -> file 
            | _ -> (reverseText file.[1..file.Length]) + (string file.[0])

    match filenames with
        | [] -> None
        | head :: [] -> Some (reverseText (readFile head).Value)
        | head :: tail -> Some ((reverseText (readFile head).Value) + (tac tail).Value)
