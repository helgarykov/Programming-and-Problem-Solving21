module readNWrite

///<summary> Given a file name, returns the contents of the text file as a string option. If the file doesn'r exist, it returns None.</summary>
///<param name="filename">A string, file name.</param>
///<returns> A string option.</returns>

let filename = "README_8.txt"

let readFile (filename: string) : option<string> =
    try 
        Some (System.IO.File.ReadAllText filename) 
    with 
        | :? System.IO.FileNotFoundException -> None

///<summary> Given a list of file names, concotenates the files. If one of the files doesn't exist, returns None.</summary>
///<param name="filenames">A list of strings.</param>
///<returns> A string option.</returns>

let listOfFiles = ["Ex1.txt"; "Ex2.txt"; "Ex3.txt"]

let rec cat (filenames: list<string>) : option<string> =
    match filenames with
        | [] -> None     //if one of the files doesn't exist, returns None
        | head :: [] -> Some (readFile head).Value   // kører på lister bestående kun af et element.
        | head :: tail -> Some ((readFile head).Value + (cat tail).Value) // [Ex1.txt; Ex2.txt] både head and tail, we kører readFile på 1.fil (2 options kan ikke lægges sammen, så vi er nødt til at omdanne option til string vha. .Value, så vi kan lægge den anden streng til). Vi kører cat på tailen (sidste element), og når listen består af kun 1 element, dvs. head, så rammer vi 2.betingele head :: [] og returner an option (derfor Some foran), fordi det er det forventede output.
    
    
///<summary> Given a list of file names, reverses the order of each file in a line-by-line manner, reverses each line (opposite of cat) and concatenates the result. If one of the files doesn't exist, returns None.</summary>
///<param name="filenames">A list of strings.</param>
///<returns> A string option.</returns>  

(*let rec tac (filenames: list<string>) : option<string> =
    match filenames with
        | [] -> None
        | head :: [] -> Some ((List.rev (readFile head).Value)  // List.rev kan ikke anvendes på en string
        | head :: tail -> Some (List.rev ((readFile head).Value + (tac tail).Value))    //List.rev kan ikke bruges på en string *)

let rec tac (filenames: list<string>) : option<string> =

    let rec reverseText (file: string) : string =
        match file.Length with                       // 
            | 0 -> ""
            | 1 -> file 
            | _ -> (reverseText file.[1..file.Length]) + (string file.[0])

    match filenames with
        | [] -> None
        | head :: [] -> Some (reverseText (readFile head).Value)
        | head :: tail -> Some ((reverseText (readFile head).Value) + (tac tail).Value)
