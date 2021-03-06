# Untitled UiPath Framework

This is POC of a framework to utilise F# as the main engine for business logic within a UiPath process. 

The reason for trying to do it this way is that although the UiPath platform does a lot of things very well, I personally think that much of its power comes from compatibility with the larger .NET Framework.

In my personal opinion, the areas in with the UiPath really shines is in having the Orchestrator as a single consolidated platform for running and scheduling processes, managing versions and managing assets, and in terms of the UiPath studio experience, it really excels in easily designing IO actions for use across multiple environments. One thing it does less well is business logic. Aside from using activities which are useful but not necessarily comprehensive for all purposes, it forces the use of imperative, procedural code with limited capability to define types and classes, which would make many types of processes much easier to design. As a result, it also tends to promote reliance on data-structures such as Dictionaries and Datatables, which can lack type safety and be error prone. It also can suffer from extremely ambiguous errors at times which can make debugging somewhat difficult. This, combined with a fairly long feedback loop and somewhat awkward testing story can make certain aspects of development almost as slow and painful, as the IO design features make it easy and painless. In short, while most automation and IO activities are much easier to design, domain modelling and handling business logic in many cases can be made much more difficult; this is especially evident when compared to a language like F# which excels in domain modelling using the type system, and can express even complex logic in an extremely terse manner.

In addition, F# also provides access to useful features unique to the language such as access to type providers for accessing data from Databases or Excel in a convenient and type safe manner.

However, if we take the view that UiPath performs best as a type of IO Framework, it places us in a fairly unique position in that having control of program execution being placed in the hands of the IO framework is an inversion of the normal order of dependencies. 

The goal is to reinvert this order and have the F# business logic act as the engine for the application, and direct the IO operations across the boundary between UiPath process and F# program, even while maintaining access to certain UiPath specific APIs such as logging to orchestrator and fetching transactions, and while the UiPath process acts as the overall calling context for the code.

The way in which I am attempting to do this is by modelling a process after a mutually recursive function, in which the F# program essentially boils down to a function which takes a State and a Message, and Outputs a new State and a Command, while the UiPath component acts as a function which takes a State (although it doesn't mutate it,) and a Command and returns the same State, plus a Message to the Activity containing the F# program.

Messages act as instuctions to the F# program to update the state in some way, and then the output of a command can be achieved by a pure function which takes the new state and returns the appropriate command. While the state is consolidated in a single object and passed between boundaries, the actual functions themselves are essentially pure functions on both sides.

Commands represent instructions to the UiPath process to perform some side effect, they can be labelled so that they can be switched upon and following this the relevant modular workflow can be called and then the appropriate message returned based on the success or failure of the operation.

There are going to be some difficulties that I have to figure out around how to model DTOs across the UiPath / F# boundary, native .net objects would probably be best, and as close to the F# domain model as possible, but I'm still not 100% confident about F# to VB.NET/C# interop yet, I'm still figuring that out.

## Assorted Notes

- I am going to be using an exel type provider for reading the config; one issue there is that although most of the time configs in UiPath are written similarly to the one found for the ReFramework where there is an excel table with three column names: Name, Value, and description; from which a regular Dictionary of type \<String, Object\> is formed. So if I am already changing the format to that of a table with every field having its own column, is there any point in keeping it in excel?