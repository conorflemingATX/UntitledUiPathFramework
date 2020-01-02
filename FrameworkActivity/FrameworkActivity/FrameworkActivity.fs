namespace TemplateActivity

open System.Activities
open System.ComponentModel

type Activity () =
    inherit CodeActivity ()

    [<Category("Input")>]
    [<RequiredArgument>]
    member val Name = new InArgument<string> () with get, set

    [<Category("Output")>]
    member val Out = new OutArgument<string> () with get, set

    override x.Execute (ctx: CodeActivityContext) =
        let name = x.Name.Get(ctx)
        let hi = Lib.hello name
        x.Out.Set(ctx, hi)

