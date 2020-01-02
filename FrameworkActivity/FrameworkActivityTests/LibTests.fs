module LibTests

open NUnit.Framework
open FsUnit

[<Test>]
let ``Should Test Successfully`` () =
    true |> should be True