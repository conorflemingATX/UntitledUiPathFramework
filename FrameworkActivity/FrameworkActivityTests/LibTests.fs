module TemplateActivityTests

open NUnit.Framework
open FsUnit

[<TestFixture>]
type ``hello`` () =
    [<Test>]
    member x.``hello "Conor" should equal "Hello Conor!"`` () =
        Lib.hello "Conor" |> should equal "Hello Conor!"
