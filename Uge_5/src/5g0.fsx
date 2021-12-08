/// <summary>
/// Given a list of lists of 'a, return true
/// if the outer list is not empty or
/// there's at least one element in one of the inner lists
/// or the number of elements in the inner lists does not differ.
/// Otherwise return false.
/// </summary>
/// <param name="llst">The list of lists.</param>
/// <return> True or false.</return>


let isTable (llst: 'a list list) =
    if llst.IsEmpty then                             //Brach 1: given a list of list, return false if it's empty
        false
    else
        let firstListLength = llst.Head.Length       //given a list of lists,
        let mutable isValid = true

        if firstListLength = 0 then                  //Branch 2a: given a list of lists,return false if the first list is empty
            isValid <- false

        for innerList in llst do
            if innerList.Length <> firstListLength then //Branch 2b: given a list of lists, return false if the number of elements
                isValid <- false                        //in the inner lists is different from the number of elements in the first inner list

        isValid                                     // Branch 2c: No invalid case was hit, so isValid is still true.

// VALID
let lst2da = [[1;2;3]; [4;5;6]]

// INVALID
let lst2db = [[]; [2]]                      // the array tests Branch 2a of isTable

// INVALID
let lst2dc = [[]; []]                       // the array tests Branch 1 of isTable

// VALID
let lst2dd = [[1; 5; 87]; [45; 8; 76]; [4; 6; 5]]

// INVALID
let lst2de = [[1; 5; 87]; [45; 8; 76; 765]; [4; 6; 5; 899]] // the array tests Branch 2b of isTable

printfn "White-box testing of isTable"
printfn "%5b: (Branch 2c %A) = %b" (isTable lst2da) lst2da true
printfn "%5b: (Brach 2a %A) = %b" (not (isTable lst2db)) lst2db false
printfn "%5b: (Branch 1 %A) = %b" (not (isTable lst2dc)) lst2dc false
printfn "%5b: (Branch 2c %A) = %b" (isTable lst2dd) lst2dd true
printfn "%5b: (Branch 2b %A) = %b" (not (isTable lst2de)) lst2de false



/// <summary>
/// Given a list of lists of 'a, return a new list
/// with first element.
/// </summary>
/// <param name="llst">The list of lists</param>
/// <return> A list of 'a.</return>


let firstColumn(llst: 'a list list) =
    let hasAnyElements (lst: 'a list) = not lst.IsEmpty             // given a list, return if it has any elements
    let listWithoutEmptyLists = (List.filter hasAnyElements llst)   // given a list, filter it, returning only those with elements

    List.map List.head listWithoutEmptyLists                       // map the lists to new list with first element


printfn "White-box testing of firstColumn"
printfn "%5b: (firstColumn %A) = %A" ((firstColumn lst2da) = [1; 4]) lst2da [1; 4]
printfn "%5b: (firstCOlumn %A) = %A" ((firstColumn lst2db) = [2]) lst2db [2]
printfn "%5b: (firstCOlumn %A) = %A" ((firstColumn lst2dc) = []) lst2dc []
printfn "%5b: (firstColumn %A) = %A" ((firstColumn lst2dd) = [1; 45; 4]) lst2dd [1; 45; 4]
printfn "%5b: (firstColumn %A) = %A" ((firstColumn lst2de) = [1; 45; 4]) lst2de [1; 45; 4]



/// <summary>
/// Given a list of lists of 'a, return a new list of lists
/// without first element.
/// </summary>
/// <param name="llst">The list of lists.</param>
/// <return> A list of lists of 'a.</return>


let dropFirstColumn(llst: 'a list list) =
    let hasAnyElements (lst: 'a list) = not lst.IsEmpty             // given a list, return if it has any elements
    let listWithoutEmptyLists = (List.filter hasAnyElements llst)   // given a list, filter it, returning only those with elements

    List.map List.tail listWithoutEmptyLists                        // map the lists to new lists without first element


printfn "White-box testing of dropFirstColumn"
printfn "%5b: (dropFirstColumn %A) = %A" ((dropFirstColumn lst2da) = [[2; 3]; [5; 6]]) lst2da [[2; 3]; [5; 6]]
printfn "%5b: (dropFirstColumn %A) = %A" ((dropFirstColumn lst2db) = [[]]) lst2db [[]]
printfn " %5b: (dropFirstColumn %A) = %A" ((dropFirstColumn lst2dc)= []) lst2dc []
printfn " %5b: (dropFirstColumn %A) = %A" ((dropFirstColumn lst2dd)= [[5; 87]; [8; 76]; [6;5]]) lst2dd [[5; 87]; [8; 76]; [6;5]]
printfn " %5b: (dropFirstColumn %A) = %A" ((dropFirstColumn lst2de)= [[5; 87]; [8; 76; 765]; [6;5; 899]]) lst2de [[5; 87]; [8; 76; 765]; [6;5; 899]]



/// <summary>
/// Given a list of lists of 'a, return a new list containing
/// the items at index in each of the inner lists.
/// </summary>
/// <param name="llst">The list of lists.</param>
/// <param name="index">The index to retrieve in each inner list.</param>
/// <return>A list of 'a.</return>

let lstColumn (llst: 'a list list) (index: int) =
    List.init llst.Length (fun x -> (llst.Item x).Item index)



/// <summary>
/// Given a list of lists of 'a, transpose the lists so rows become columns,
/// and columns become rows.
/// </summary>
/// <param name="llst">The list of lists.</param>

let transpose (llst: 'a list list) =
    List.init (llst.Item 0).Length (fun x -> lstColumn llst x)


printfn "White-box testing of transpose"
printfn "%5b: (transpose %A) = %A" ((transpose lst2da) = [[1; 4]; [2; 5]; [3; 6]]) lst2da [[1; 4]; [2; 5]; [3; 6]]
printfn "%5b: (transpose %A) = %A" ((transpose lst2db) = []) lst2db []
printfn "%5b: (transpose %A) = %A" ((transpose lst2dc) = []) lst2dc []
printfn "%5b: (transpose %A) = %A" ((transpose lst2dd) = [[1; 45; 4]; [5; 8; 6]; [87; 76; 5]]) lst2dd [[1; 45; 4]; [5; 8; 6]; [87; 76; 5]]
printfn "%5b: (transpose %A) = %A" ((transpose lst2de) = [[1; 45; 4]; [5; 8; 6]; [87; 76; 5]]) lst2de [[1; 45; 4]; [5; 8; 6]; [87; 76; 5]]
