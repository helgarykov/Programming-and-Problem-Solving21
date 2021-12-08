
let newArray = array2D [[1;2]; [3;4]]

/// <summary> 
/// Given a two-dimentional array, transpose it so that rows become columns
/// and columns become rows.
/// </summary>
/// <param name="a"> Array of 'a.</param>
/// <return> An array of 'a.</return>

let transposeArr (a : 'a[,]) =
    Array2D.init (Array2D.length2 a) (Array2D.length1 a) (fun r c -> a.[c,r])


printfn "White-box test for transposeArr"
printfn "%5b:" ((transposeArr newArray) = array2D[[1;3]; [2;4]]) 

//(c) Comparing this implementation with Assignment 5g0d, what are the advantages and disadvantages
//of each of these implementations?

//To answer this question, I should have applied the imperative paradigm to
// the implementation in 5g0d. I believe, though, that had it been the case,
// one of its main disadvantages would have been a code of many more lines, containing
// additional mutable variables and while/for-loops, than a code written within
// the functional paradigm.
// That also implies that the code implementing the functional
// paradigm is much more concise and "clean", so to speak, than that of
// the imperative one. In other words, the functional approach is less prone to errors
// and is easier to maintain than the imperative one.


///(d) For the application of tables, which of lists and arrays are better programmed using the
//imperative paradigm and using the functional paradigm and why?

// Being two-dimensional lists, arrays organise data in the form of a table, with a 'row'-axis and
// a 'column'-axis.
// I believe that arrays are better programmed using the imperative paradigm for
// the application of tables, because they are mutable and randomly accessible via index.
// Lists, on the other hand, are immutable and are optimised to recursive functions —
// both are indicative of the functional paradigm.



