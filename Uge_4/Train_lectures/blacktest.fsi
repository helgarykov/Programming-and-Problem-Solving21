module transform

///<summary> Convert a non-negative integer into its binary form.
///E.g., dec2bin 3 = "0b11".</summary>
///<example>The call <c>dec2bin 3</c> returns <c>"0b11"</c>.</example>
///<param name="n">a non-negative integer.</param>
///<returns>The binary representation of n as a string on Fsharp form </returns>
val dec2bin : n:int -> string