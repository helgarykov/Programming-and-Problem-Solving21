let isTable (llst: 'a list list) =
    if llst.IsEmpty then
        false
    else
        let firstListLength = llst.Head.Length
        let mutable isValid = true

        if firstListLength = 0 then
            isValid <- false

        for innerList in llst do
            if innerList.Length <> firstListLength then
                isValid <- false

        isValid

// VALID 
let lst2da = [[1;2;3]; [4;5;6]]

// INVALID
let lst2db = [[]; [2]]

// INVALID
let lst2dc = [[]; []]

// VALID
let lst2dd = [[1; 5; 87]; [45; 8; 76]; [4; 6; 5]]

// INVALID
let lst2de = [[1; 5; 87]; [45; 8; 76; 765]; [4; 6; 5; 899]]

printfn "White-box testing of isTable"
printfn "%5b: (isTable %A) = %b" (isTable lst2da) lst2da true
printfn "%5b: (isTable %A) = %b" (not (isTable lst2db)) lst2db false
printfn "%5b: (isTable %A) = %b" (not (isTable lst2dc)) lst2dc false
printfn "%5b: (isTable %A) = %b" (isTable lst2dd) lst2dd true
printfn "%5b: (isTable %A) = %b" (not (isTable lst2de)) lst2de false



let firstColumn(llst: 'a list list) =
    let firstElement (lst:'a list) = lst.Head
    List.map firstElement llst
  
 
let dropFirstColumn(llst: 'a list list) = 
    let nextElement (lst:'a list) = lst.Tail
    let newLst = (List.filter (fun (lst: 'a list) -> not lst.IsEmpty) llst)
    List.map nextElement newLst




printfn "White-box testing of dropFirstColumn"
printfn "%5b: (dropFirstColumn %A) = %A" ((dropFirstColumn lst2da) = [[2; 3]; [5; 6]]) lst2da [[2; 3]; [5; 6]]
printfn "%5b: (dropFirstColumn %A) = %A" ((dropFirstColumn lst2db) = [[]]) lst2db [[]]
printfn "%A" (dropFirstColumn lst2db)

printfn "White-box testing of firstColumn"
printfn "%5b: (firstColumn %A) = %A" ((firstColumn lst2dd) = [1; 45; 4]) lst2dd [1; 45; 4]
printfn "%5b: (firstColumn %A) = %A" ((firstColumn lst2da) = [1; 4]) lst2da [1; 4]






plet isTable (llst: 'a list list) =
    if llst.IsEmpty then
        false
    else
        let firstListLength = llst.Head.Length
        let mutable isValid = true

        if firstListLength = 0 then
            isValid <- false

        for innerList in llst do
            if innerList.Length <> firstListLength then
                isValid <- false

        isValid

// VALID 
let lst2da = [[1;2;3]; [4;5;6]]

// INVALID
let lst2db = [[]; [2]]

// INVALID
let lst2dc = [[]; []]

// VALID
let lst2dd = [[1; 5; 87]; [45; 8; 76]; [4; 6; 5]]

// INVALID
let lst2de = [[1; 5; 87]; [45; 8; 76; 765]; [4; 6; 5; 899]]

printfn "White-box testing of isTable"
printfn "%5b: (isTable %A) = %b" (isTable lst2da) lst2da true
printfn "%5b: (isTable %A) = %b" (not (isTable lst2db)) lst2db false
printfn "%5b: (isTable %A) = %b" (not (isTable lst2dc)) lst2dc false
printfn "%5b: (isTable %A) = %b" (isTable lst2dd) lst2dd true
printfn "%5b: (isTable %A) = %b" (not (isTable lst2de)) lst2de false



let firstColumn(llst: 'a list list) =
    let firstElement (lst:'a list) = lst.Head
    List.map firstElement llst
  

printfn "White-box testing of firstColumn"
printfn "%5b: (firstColumn %A) = %A" ((firstColumn lst2dd) = [1; 45; 4]) lst2dd [1; 45; 4]
printfn "%5b: (firstColumn %A) = %A" ((firstColumn lst2da) = [1; 4]) lst2da [1; 4]

let dropFirstColumn(llst: 'a list list) =
    let hasAnyElements (lst: 'a list) = not lst.IsEmpty             // given a list, return if it has any elements
    let listWithoutEmptyLists = (List.filter hasAnyElements llst)   // given a list, filter it, returning only those with elements

    // List.map ('a -> 'b) 'a list -> 'b list
    List.map List.tail listWithoutEmptyLists    // map the lists to new lists without first element


printfn "White-box testing of dropFirstColumn"
printfn "%5b: (dropFirstColumn %A) = %A" ((dropFirstColumn lst2da) = [[2; 3]; [5; 6]]) lst2da [[2; 3]; [5; 6]]
printfn "%5b: (dropFirstColumn %A) = %A" ((dropFirstColumn lst2db) = [[]]) lst2db [[]]
printfn " %5b: (dropFirstColumn %A) = %A" ((dropFirstColumn lst2dc)= []) lst2dc []


let table = [[1; 5; 87]; [45; 8; 76]; [4; 6; 5]]
let table2 = [[1; 5; 87; 13; 8]; [45; 8; 76; 18; 99]; [4; 6; 5; 99; 101]]


/// <summary>
/// Given a list of lists of 'a, return a new list containing
/// the items at index in each of the inner lists.
/// </summary>
/// <param name="llst">The list of lists</param>
/// <param name="index">The index to retrieve in each inner list</param>
/// <return>A list of 'a</return>
let lstColumn (llst: 'a list list) (index: int) =
    List.init llst.Length (fun x -> (llst.Item x).Item index)

/// <summary> Given a list of lists of 'a, transpose the lists so rows become columns,
/// and columns become rows.</summary>
/// <param name="llst">The list of lists</param>

let transpose (llst: 'a list list) =
    List.init (llst.Item 0).Length (fun x -> lstColumn llst x)

printfn "%A" (transpose table2)
