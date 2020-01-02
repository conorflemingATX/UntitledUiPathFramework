namespace TemplateActivity

open System.Activities
open System.ComponentModel

type Activity () =
    inherit CodeActivity ()

    [<Category("Input")>]
    [<RequiredArgument>]
    member val State = new InArgument<string> () with get, set

    [<Category("Input")>]
    [<RequiredArgument>]
    member val Message = new InArgument<string> () with get, set

    [<Category("Output")>]
    [<RequiredArgument>]
    member val OutState = new OutArgument<string> () with get, set

    [<Category("Output")>]
    [<RequiredArgument>]
    member val Command = new OutArgument<string> () with get, set

    override x.Execute (ctx: CodeActivityContext) =
        let state = x.State.Get(ctx)
        let hi = Lib.program <| state
        x.OutState.Set(ctx, hi)

